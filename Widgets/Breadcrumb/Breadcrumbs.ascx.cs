using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using SitefinityWebApp.Widgets.Breadcrumb;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Widgets.Breadcrumb
{

    internal class NodeInfoX
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public bool IsCurrent { get; set; }
    }

    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner))]
    public partial class Breadcrumbs : System.Web.UI.UserControl
    {

        public Boolean ShowHomePage { get; set; }
        public string CssFile { get; set; }
        public Boolean IncludeVirtualNodes { get; set; }

        private List<NodeInfoX> _nodes = new List<NodeInfoX>();

        public Breadcrumbs()
        {
            ShowHomePage = true;
            CssFile = "~/Widgets/Breadcrumb/Styles/breadcrumbs.css";
            IncludeVirtualNodes = true;
        }


        protected override void OnPreRender(EventArgs e)
        {

            string cssFileUrl = !string.IsNullOrEmpty(CssFile) ? VirtualPathUtility.ToAbsolute(CssFile) : string.Empty;

            if (!string.IsNullOrEmpty(CssFile) && !Utils.HasStyleSheetLoaded(Page, cssFileUrl))
            {
                var css = new Literal { Text = "<link href=\"" + cssFileUrl + "\" rel=\"Stylesheet\" type=\"text/css\" />\n" };
                Page.Header.Controls.Add(css);
            }

            base.OnPreRender(e);
        }

        protected void Page_PreRender()
        {

            this.Page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);

            _nodes.Clear();
            if (SiteMap.RootNode.HasChildNodes)
            {

                SiteMapNode node = SiteMap.CurrentNode;
                while (node != SiteMap.RootNode)
                {
                    SiteMapNode parentNode = node.ParentNode;
                    if (node != SiteMap.RootNode)
                    {
                        NodeInfoX NodeInfoX = ProcessNode(node);
                        if (NodeInfoX != null)
                        {
                            _nodes.Add(NodeInfoX);
                        }

                        if (node.Title == "news" && Request.RawUrl.Contains("article"))
                        {
                            _nodes.Last().IsCurrent = true;
                        }
                    }
                    node = parentNode;
                }
                _nodes.Reverse();
            }
        }

        private NodeInfoX ProcessNode(SiteMapNode node)
        {
            if ((node is PageSiteNode) && (node as PageSiteNode).ShowInNavigation)
            {
                bool nodeCurrent = false;
                if (SiteMap.CurrentNode != null)
                {
                    if (node.Title == SiteMap.CurrentNode.Title)
                    {
                        nodeCurrent = true;
                    }
                    if (nodeCurrent || node.ParentNode == SiteMap.RootNode)
                    {
                        NodeInfoX result = new NodeInfoX()
                        {
                            Title = node.Title,
                            Url = ResolveClientUrl(node.Url),
                            IsCurrent = nodeCurrent
                        };

                        return result;
                    }
                }
            }
            return null;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<div class=\"BreadcrumbsOuterWrapper\"><div class=\"breadcrumbs flat\">");
            string aFormat = "<a href=\"{0}\" class=\"{1}\">{2}</a>";

            for (int i = 0; i < _nodes.Count; i++)
            {
                if (i == _nodes.Count-1)
                {
                    writer.Write(string.Format(aFormat, "javascript:void(0);", "active", _nodes[i].Title));
                }
                else
                {
                    writer.Write(string.Format(aFormat, _nodes[i].Url, "breadcrumblink", _nodes[i].Title));
                }
            }

            writer.Write("</ul></div></div>");
        }

 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (IncludeVirtualNodes)
            {
                var extender = this.Page.GetBreadcrumbExtender();
                if (extender != null)
                {
                    var virtualNodes = extender.GetVirtualNodes(SitefinitySiteMap.GetCurrentProvider());
                    foreach (var node in virtualNodes)
                    {
                        var processedNode = new NodeInfoX()
                        {
                            Title = node.Title,
                            Url = ResolveClientUrl(node.Url),
                            IsCurrent = true
                        };
                        _nodes.Add(processedNode);
                    }
                }
            }
            SiteMapNode hnode = SiteMap.RootNode.ChildNodes[0];
            NodeInfoX HomeNode = ProcessNode(hnode);
            if (HomeNode != null && ShowHomePage)
            {
                _nodes.Insert(0, HomeNode);
            }
        }

        public static readonly string breadcrumbWidgetVirtualPath = "~/BreadcrumbWidget/";
        private readonly string breadcrumbWidgetTempaltePath = breadcrumbWidgetVirtualPath + "Breadcrumbs.Widgets.Breadcrumbs.breadcrumbs.ascx";
    }
}