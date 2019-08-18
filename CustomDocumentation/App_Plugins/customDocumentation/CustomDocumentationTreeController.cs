using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using umbraco.BusinessLogic.Actions;
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
            return CreateTreeNodeCollection(id, queryStrings);
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menuNodes = new MenuItemCollection();
            menuNodes.Items.Add<RefreshNode, ActionRefresh>("Reload");
            return menuNodes;
        }

        private TreeNodeCollection CreateTreeNodeCollection(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            if (id == "-1")
            {
                return TopLevel(id, queryStrings);
            }
            nodes = GetChildren(id, queryStrings);
            return nodes;
        }

        private TreeNodeCollection TopLevel(string id, FormDataCollection queryStrings)
        {
            var topNodes = new TreeNodeCollection();
            topNodes.Add(CreateTreeNode(Constants.MAIN_FOLDER_NAME, id, queryStrings, "Documentation", "icon-folder", true));
            return topNodes;
        }

        private TreeNodeCollection GetChildren(string parentId, FormDataCollection queryStrings)
        {
            var folderChildren = GetFolderChildren(parentId);
            var directories = folderChildren.Item1;
            var files = folderChildren.Item2;
            files = files.ToList().Where(file => !file.Contains("README.md"));

            var folderNodes = GetTreeFolders(parentId, queryStrings, directories);
            var fileNodes = GetTreeFiles(parentId, queryStrings, files);
            folderNodes.AddRange(fileNodes);
            return folderNodes;
        }

        private TreeNodeCollection GetTreeFolders(string parentId, FormDataCollection queryStrings, IEnumerable<string> folders)
        {
            var treeFolders = new TreeNodeCollection();
            foreach (var folder in folders)
            {
                var folderParts = folder.Split(new string[] { Constants.MAIN_FOLDER_NAME }, StringSplitOptions.None);
                var partAfterMainFolder = folderParts[folderParts.Length - 1];
                var distinctFolderNames = partAfterMainFolder.Split('\\');
                var name = distinctFolderNames[distinctFolderNames.Length - 1];
                var hasChildren = Directory.GetFiles(folder).Length != 0;
                treeFolders.Add(CreateTreeNode($"{name}", parentId, queryStrings, name, "icon-folder", hasChildren));
            }
            return treeFolders;
        }

        private TreeNodeCollection GetTreeFiles(string parentId, FormDataCollection queryStrings, IEnumerable<string> files)
        {
            var treeFiles = new TreeNodeCollection();
            foreach (var file in files)
            {
                var parts = file.Split(new string[] { Constants.MAIN_FOLDER_NAME }, StringSplitOptions.None);
                var partAfterMain = parts[parts.Length - 1];
                var distinctNames = partAfterMain.Split('\\');
                var name = distinctNames[distinctNames.Length - 1];
                treeFiles.Add(CreateTreeNode($"{parentId}&{name}", parentId, queryStrings, name, "icon-file", false));
            }
            return treeFiles;
        }

        private Tuple<IEnumerable<string>, IEnumerable<string>> ReadFolder(string folderName)
        {
            var path = HttpContext.Current.Server.MapPath($"~\\{folderName}");
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            return new Tuple<IEnumerable<string>, IEnumerable<string>>(directories, files);
        }

        private Tuple<IEnumerable<string>, IEnumerable<string>> GetFolderChildren(string parentId)
        {
            Tuple<IEnumerable<string>, IEnumerable<string>> folderChildren;
            if (parentId == Constants.MAIN_FOLDER_NAME)
                folderChildren = ReadFolder(Constants.MAIN_FOLDER_NAME);
            else
            {
                var folderPath = parentId.Replace('&', '/');
                folderChildren = ReadFolder(Constants.MAIN_FOLDER_NAME + "/" + parentId);
            }
            return folderChildren;
        }
    }
}