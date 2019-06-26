using Markdig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Controllers
{
    public class CustomDocumentationController: UmbracoAuthorizedApiController
    {
        public string GetHtmlForRoute()
        {
            var result = Markdown.ToHtml("This is a text with some *emphasis*");
            return result;
        }

    }
}