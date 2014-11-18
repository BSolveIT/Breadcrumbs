using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;

namespace SitefinityWebApp.Widgets.Breadcrumb.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.Widgets.Breadcrumb.Breadcrumbs"/> widget
    /// </summary>
    public class BreadcrumbsDesigner : ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return BreadcrumbsDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the ShowHomePage property
        /// </summary>
        protected virtual Control ShowHomePage
        {
            get
            {
                return this.Container.GetControl<Control>("ShowHomePage", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the IncludeVirtualNodes property
        /// </summary>
        protected virtual Control IncludeVirtualNodes
        {
            get
            {
                return this.Container.GetControl<Control>("IncludeVirtualNodes", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the CssFile property
        /// </summary>
        protected virtual Control CssFile
        {
            get
            {
                return this.Container.GetControl<Control>("CssFile", true);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("showHomePage", this.ShowHomePage.ClientID);
            descriptor.AddElementProperty("includeVirtualNodes", this.IncludeVirtualNodes.ClientID);
            descriptor.AddElementProperty("cssFile", this.CssFile.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(BreadcrumbsDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Widgets/Breadcrumb/Designer/BreadcrumbsDesigner.ascx";
        public const string scriptReference = "~/Widgets/Breadcrumb/Designer/BreadcrumbsDesigner.js";
        #endregion
    }
}
 
