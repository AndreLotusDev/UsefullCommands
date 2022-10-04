using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Helpers
{

    public class RazorViewRendererService
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<ResourceFile> _resourceFiles;
        public RazorViewRendererService(

            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _resourceFiles = GetImagesBase64();
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = GetActionContext();
            var view = FindView(actionContext, viewName);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },

                    new TempDataDictionary(
                        actionContext.HttpContext,
                        _tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                var htmlTemplate = output.ToString();

                _resourceFiles.ForEach(f => htmlTemplate = htmlTemplate.Replace(f.FileName, f.ImageBase64));

                return htmlTemplate;
            }
        }
        public string GetTemplatePath(string templateName)
        {
            return Path.Combine("Views", "Emails", "Account", $"{templateName}.cshtml");
        }

        public string GetTemplateStagePath(string templateName)
        {
            return Path.Combine("Views", "Emails", "Stage", $"{templateName}.cshtml");
        }

        public string GetTemplateDocumentPath(string templateName)
        {
            return Path.Combine("Views", "Emails", "Document", $"{templateName}.cshtml");
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);

            if (getViewResult.Success) return getViewResult.View;

            var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);

            if (findViewResult.Success) return findViewResult.View;

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);

            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new InvalidOperationException(errorMessage);
        }
        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = _serviceProvider;
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
        private List<ResourceFile> GetImagesBase64()
        {
            var images = new List<ResourceFile>();
            var assembly = Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames();

            if (resources.Length > 0)
            {
                foreach (var r in resources)
                {
                    using (var reader = new StreamReader(assembly.GetManifestResourceStream(r)))
                    {
                        byte[] bytes;

                        using (var ms = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        string base64 = Convert.ToBase64String(bytes);
                        var resourceFile = new ResourceFile
                        {
                            FileName = string.Join('.', r.Split('.').TakeLast(2)),
                            ImageBase64 = $"data:image/{ r.Split('.').Last() };base64, {base64}"
                        };

                        images.Add(resourceFile);
                    }
                }
            }
            return images;
        }
    }

    class ResourceFile
    {
        public string FileName { get; set; }
        public string ImageBase64 { get; set; }
    }
}
