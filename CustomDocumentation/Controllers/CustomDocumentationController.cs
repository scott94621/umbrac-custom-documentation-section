using CustomDocumentation.App_Plugins.customDocumentation;
using Markdig;
using System;
using System.IO;
using System.Web;
using System.Web.Http.Results;
using Umbraco.Web.WebApi;

namespace Controllers
{
    public class CustomDocumentationController : UmbracoAuthorizedApiController
    {
        public JsonResult<string> GetHtmlForRoute(string filePath)
        {
            var editedFilePath = CheckExtensions(filePath);
            var path = CreateReadablePath(editedFilePath);
            try
            {
                var fileContent = File.ReadAllText(path);
                using (var reader = new StreamReader(path))
                {
                    fileContent = reader.ReadToEnd();
                }
                var result = fileContent;
                if (ExtensionHelper.HasFileMdExtension(editedFilePath))
                    result = Markdown.ToHtml(fileContent);
                return Json(result);
            }
            catch (FileNotFoundException e)
            {
                Logger.Error(typeof(CustomDocumentationController), $"The file at path {path} was not found", e);
                return Json("File was not found");
            }
            catch(Exception e)
            {
                Logger.Error(typeof(CustomDocumentationController), "", e);
                return Json("Something went wrong");
            }
        }

        private string CheckExtensions(string filePath)
        {
            if (!ExtensionHelper.HasFileAllowedExtension(filePath))
            {
                if (IsFileParentFolder(filePath))
                    filePath = filePath.Replace(Constants.MAIN_FOLDER_NAME, "");

                filePath = $"{filePath}/{Constants.README}";
            }
            return filePath;
        }

        private string CreateReadablePath(string filePath)
        {
            return HttpContext.Current.Server.MapPath("/Documentation/" + filePath);
        }

        private bool IsFileParentFolder(string filePath)
        {
            return filePath.Contains(Constants.MAIN_FOLDER_NAME);
        }
    }
}