 public class CurrentUserInfo 
    {
        public CurrentUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            var isANormaUserFromIdentity = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value != null;
            var isUserFromAzure = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value != null;

            if (isANormaUserFromIdentity)
            {
                UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
                ExternalUser = false;
                FirstName = httpContextAccessor.HttpContext?.User?.FindFirst("first_name")?.Value;
                LastName = httpContextAccessor.HttpContext?.User?.FindFirst("last_name")?.Value;
                UserEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
                UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (isUserFromAzure)
            {
                UserId = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                ExternalUser = true;
                FirstName = httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value.Split(" ")[0];
                LastName = httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value.Split(" ").Skip(1).FirstOrDefault();
                UserEmail = httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value;
                UserName = httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value;
            }
            

            Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
        }
        public string UserId { get; }
        public bool ExternalUser { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string UserEmail { get; }
        public IReadOnlyList<string> Roles { get; }
    }