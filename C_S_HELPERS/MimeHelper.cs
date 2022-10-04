using Microsoft.AspNetCore.StaticFiles;

namespace Helpers
{
    public class MimeHelper 
    {
        public string GetContentType(string path) {
            var found = new FileExtensionContentTypeProvider().TryGetContentType(path, out var contentType);
            return contentType;
        }
    }
}
