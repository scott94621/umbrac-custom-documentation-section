namespace CustomDocumentation_V8.App_Plugins.customDocumentation
{
    public static class ExtensionHelper
    {
        public static bool HasFileMdExtension(string filePath)
        {
            return filePath.Contains(Constants.MD_EXTENSION);
        }
        public static bool HasFileHtmlExtension(string filePath)
        {
            return filePath.Contains(Constants.HTML_EXTENSION);
        }
        public static bool HasFileTxtExtension(string filePath)
        {
            return filePath.Contains(Constants.TXT_EXTENSION);
        }

        public static bool HasFileAllowedExtension(string filePath)
        {
            return HasFileMdExtension(filePath) || HasFileHtmlExtension(filePath) || HasFileTxtExtension(filePath);
        }
    }
}