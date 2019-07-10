using CustomDocumentation.App_Plugins.customDocumentation;
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
        private const string MD_EXTENSION = ".md";
        private const string TXT_EXTENSION = ".txt";
        private const string HTML_EXTENSION = ".html";
        private const string README = "README.md";

        public JsonResult<string> GetHtmlForRoute(string filePath)
        {
            if (!HasFileMdExtension(filePath))
            {
                if (IsFileParentFolder(filePath))
                    filePath = filePath.Replace(Constants.MAIN_FOLDER_NAME, "");    
                
                filePath = $"{filePath}-{README}";
            }
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

        private bool HasFileMdExtension(string filePath)
        {
            return filePath.Contains(MD_EXTENSION);
        }

        private bool IsFileParentFolder(string filePath)
        {
            return filePath.Contains(Constants.MAIN_FOLDER_NAME);
        }
    }
}