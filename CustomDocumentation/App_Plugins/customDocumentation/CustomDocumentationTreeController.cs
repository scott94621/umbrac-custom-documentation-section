using System;
using System.IO;
using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace CustomDocumentation.App_Plugins.customDocumentation
{
    [Tree("customDocumentation", "customDocumentationTree", "Documentation")]
    [PluginController("customDocumentation")]
    public class CustomDocumentationTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            ReadFolder();
            return CreateTreeNodeCollection(id, queryStrings);
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }

        private TreeNodeCollection CreateTreeNodeCollection(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            if (id == "-1")
            {
                return TopLevel(id, queryStrings);
            }
            return nodes;
        }

        private TreeNodeCollection TopLevel(string id, FormDataCollection queryStrings)
        {
            var topNodes = new TreeNodeCollection();
            topNodes.Add(CreateTreeNode("Top", id, queryStrings, "Documentation", "icon-folder", true));
            return topNodes;
        }

        //private TreeNodeCollection GetChildren(string id, FormDataCollection queryStrings)
        //{
        //    var 
        //    return new TreeNodeCollection();
        //}

        private void ReadFolder()
        {
            var path = "~/Documentation";
            var directoryName = Path.GetFileName(path);
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var res = AppDomain.CurrentDomain.BaseDirectory;
            var directories = Directory.GetDirectories(currentDirectoryPath+ @"\Documentation");
        }
    }
}