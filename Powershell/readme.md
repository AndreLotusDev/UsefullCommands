## Setup Instructions

1. **Environment Variables**: Set up the following environment variables with your sensitive information.
   - `MY_SECURE_PASSWORD`: Your secure password.
   - `MY_TENANT_ID`: Your tenant ID.
   - `MY_SITE_URL`: Your SharePoint site URL.
   - `MY_AZURE_DNS_ID`: Your Azure DNS ID.
   - `MY_CERT_PATH`: Path to your certificate.

2. **Script Usage**: Modify the script to use these environment variables:
    ```powershell
    $pwd = ConvertTo-SecureString -String (Get-Content env:MY_SECURE_PASSWORD) -Force -AsPlainText
    $tenant = (Get-Content env:MY_TENANT_ID)
    $siteURL = (Get-Content env:MY_SITE_URL)
    $dnsNameAzureId = (Get-Content env:MY_AZURE_DNS_ID)
    $pfxPath = (Get-Content env:MY_CERT_PATH)
    ```

3. **Run the Script**: Execute the script as needed, ensuring that your environment variables are properly configured.
