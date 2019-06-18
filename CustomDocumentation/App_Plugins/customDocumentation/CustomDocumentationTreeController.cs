using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.ModelBinding;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;

namespace CustomDocumentation.App_Plugins.customDocumentation
{
    [Tree("customDocumentation", "customDocumentationTree", "Documentation")]
    [PluginController("customDocumentation")]
    public class CustomDocumentationTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            return new TreeNodeCollection();
        }

        protected override MenuItemCollection GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }
    }
}