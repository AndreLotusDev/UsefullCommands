Install-Module -Name PnP.PowerShell -Scope CurrentUser
Install-Module PnP.PowerShell
Install-Module -Name SharePointPnPPowerShellOnline -Force

$pwd = ""
$tenant = ""
$siteURL = ""
$pfxPath = ""
$subFolder = ""
$dnsNameAzureId = ""
$folderSiteRelativeUrl = "h"
$cert = New-SelfSignedCertificate -DnsName $dnsNameAzureId -CertStoreLocation $pfxPath
$pwd = ConvertTo-SecureString -String "" -Force -AsPlainText
$folderId = ""

Connect-PnPOnline -ClientId $dnsNameAzureId -CertificatePath $pfxPath -CertificatePassword $pwd -Url $siteURL -Tenant $tenant

#################################

# Iterate through a folder

$folderUrl = ""

function Get-AllItems($folderUrl) {
    $folders = Get-PnPFolderItem -FolderSiteRelativeUrl $folderUrl -ItemType Folder
    foreach ($subFolder in $folders) {
        Write-Output ("Folder: " + $subFolder.Name)
        Get-AllItems -folderUrl $subFolder.ServerRelativeUrl
    }

    $files = Get-PnPFolderItem -FolderSiteRelativeUrl $folderUrl -ItemType File
    foreach ($file in $files) {
        Write-Output ("File: " + $file.Name)
    }
}

Get-AllItems -folderUrl $folderUrl

#################################

# Get all list information
$lists = Get-PnPList

$lists | ForEach-Object {
    $listName = $_.Title
    $itemCount = $_.ItemCount
    Write-Output "$listName : $itemCount items"
}


#################################

# Get by specificified documents

$listName = "Documents"

$lists = Get-PnPList

$selectedList = $lists | Where-Object { $_.Title -eq $listName }

if ($selectedList) {

    $siteUrl = ""

    $relativeListUrl = $selectedList.RootFolder.ServerRelativeUrl

    $listUrl = "$siteUrl$relativeListUrl"

    $listId = $selectedList.Id

    $itemCount = (Get-PnPListItem -List $listName).Count
    Write-Output "$listName : $itemCount items"
    Write-Output "Full url: '$listName' é: $listUrl"
    Write-Output "unique id:'$listName' é: $listId"
} else {
    Write-Output "Not found"
}

#################################


$folderId = ""

# Obter a pasta inicial usando o GUID
$folder = Get-PnPListItem -List "Documents" -UniqueId $folderId

# Verificar se a pasta foi encontrada
if ($folder) {
    # Obter a URL relativa da pasta
    $folderUrl = $folder.FieldValues["FileRef"]

    # Função para listar todas as subpastas a partir de uma URL
    function Get-SubFolders($folderUrl) {
        $query = "<View Scope='RecursiveAll'><Query><Where><Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>1</Value></Eq></Where></Query></View>"
        $folders = Get-PnPListItem -List "Documents" -FolderServerRelativeUrl $folderUrl -Query $query -PageSize 100

        foreach ($subFolder in $folders) {
            Write-Output "Pasta: $($subFolder.FieldValues['FileLeafRef'])"
            Write-Output "URL Relativa: $($subFolder.FieldValues['FileRef'])"
            # Chamada recursiva para obter subpastas
            Get-SubFolders -folderUrl $subFolder.FieldValues['FileRef']
        }
    }

    # Chamar a função para listar todas as subpastas a partir da pasta inicial
    Get-SubFolders -folderUrl $folderUrl
} else {
    Write-Output "Folder by uguid not found"
}

#################################

# Quick info retrieve 

$ListItems = Get-PnPFolderItem -FolderSiteRelativeUrl ""

foreach ($item in $ListItems) {
    $itemId = $item.UniqueId
    $itemName = $item.Name
    
    if ($item.PSObject.Properties["ItemCount"]) {
        $itemQtd = $item.ItemCount
    } else {
        $itemQtd = "N/A"
    }
    
    Write-Output "Item ID: $itemId"
    Write-Output "Item Name: $itemName"
    Write-Output "Item total: $itemQtd"
    Write-Output "---------------------"
}


#################################

# Define the list title and folder URL
$listTitle = "Documents"
$folderUrl = ""
$batchSize = 100

# Initialize variables
$listItems = @()
$position = $null
$counter = 0

# Function to get items from a folder with pagination
function Get-PagedFolderItems {
    param (
        [string]$listTitle,
        [string]$folderUrl,
        [int]$batchSize = 100
    )

    $allItems = @()
    $position = $null

    do {
        $viewXml = @"
        <View Scope='RecursiveAll'>
            <Query>
                <Where>
                    <Eq>
                        <FieldRef Name='FileDirRef'/><Value Type='Text'>$folderUrl</Value>
                    </Eq>
                </Where>
                <OrderBy>
                    <FieldRef Name='ID' Ascending='TRUE'/>
                </OrderBy>
                <RowLimit Paged='TRUE'>$batchSize</RowLimit>
            </Query>
        </View>
"@

        $query = New-Object Microsoft.SharePoint.Client.CamlQuery
        $query.ViewXml = $viewXml
        $query.ListItemCollectionPosition = $position

        try {
            $items = Get-PnPListItem -List $listTitle -Query $query
        } catch {
            Write-Error "Error retrieving items: $_"
            break
        }

        if ($items -ne $null) {
            $allItems += $items
            $position = $items.ListItemCollectionPosition
        }

    } while ($position -ne $null)

    return $allItems
}

# Get items from the specified folder with pagination
$ListItems = Get-PagedFolderItems -listTitle $listTitle -folderUrl $folderUrl -batchSize $batchSize

foreach ($item in $ListItems) {
    $itemId = $item["UniqueId"]
    $itemName = $item["FileLeafRef"]
    $itemQtd = if ($item.PSObject.Properties["ItemChildCount"]) { $item["ItemChildCount"] } else { "N/A" }
    Write-Output "Item ID: $itemId"
    Write-Output "Item Name: $itemName"
    Write-Output "Item total: $itemQtd"
    Write-Output "---------------------"
    
    $counter++
    Write-Progress -PercentComplete ($counter / ($ListItems.Count) * 100) -Activity "Processing Items $counter"
}
