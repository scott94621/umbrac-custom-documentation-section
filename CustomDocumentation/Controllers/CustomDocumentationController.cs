using CustomDocumentation.App_Plugins.customDocumentation;
using Markdig;
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
            var fileContent = File.ReadAllText(path);
            using (var reader = new StreamReader(path))
            {
                fileContent = reader.ReadToEnd();
            }
            var result = fileContent;
            if (HasFileMdExtension(editedFilePath))
                result = Markdown.ToHtml(fileContent);
            return Json(result);
        }

        private string CheckExtensions(string filePath)
        {
            if (!HasFileMdExtension(filePath) && !HasFileHtmlExtension(filePath) && !HasFileTxtExtension(filePath))
            {
                if (IsFileParentFolder(filePath))
                    filePath = filePath.Replace(Constants.MAIN_FOLDER_NAME, "");

                filePath = $"{filePath}-{Constants.README}";
            }
            return filePath;
        }

        private string CreateReadablePath(string filePath)
        {
            var pathParts = filePath.Split('-');
            var relativePath = string.Join("/", pathParts);
            return HttpContext.Current.Server.MapPath("/Documentation/" + relativePath);
        }

        private bool HasFileMdExtension(string filePath)
        {
            return filePath.Contains(Constants.MD_EXTENSION);
        }
        private bool HasFileHtmlExtension(string filePath)
        {
            return filePath.Contains(Constants.HTML_EXTENSION);
        }
        private bool HasFileTxtExtension(string filePath)
        {
            return filePath.Contains(Constants.TXT_EXTENSION);
        }

        private bool IsFileParentFolder(string filePath)
        {
            return filePath.Contains(Constants.MAIN_FOLDER_NAME);
        }
    }
}