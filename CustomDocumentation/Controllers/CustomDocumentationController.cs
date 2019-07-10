using Markdig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Controllers
{
    public class CustomDocumentationController: UmbracoAuthorizedApiController
    {
        public JsonResult<string> GetHtmlForRoute(string filePath)
        {
            var path = CreateReadablePath(filePath);
            var fileContent = File.ReadAllText(path);
            using(var reader = new StreamReader(path))
            {
                fileContent = reader.ReadToEnd();
            }
            var result = Markdown.ToHtml(fileContent);

            return Json(result);
        }

        private string CreateReadablePath(string filePath)
        {
            var pathParts = filePath.Split('-');
            var relativePath = string.Join("/", pathParts);
            return HttpContext.Current.Server.MapPath("/Documentation/"+relativePath);
        }
    }
}