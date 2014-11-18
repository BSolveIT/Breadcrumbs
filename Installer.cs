using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using SitefinityWebApp.Widgets.Breadcrumb;

namespace Breadcrumbs
{
    public class Installer
    {
        public static void PreApplicationStart()
        {
            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }

        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName != "RegisterRoutes" || !Bootstrapper.IsDataInitialized)
            {
                return;
            }

            InstallVirtualPaths();
            InstallWidget(); 
        }

        private static void InstallVirtualPaths()
        {
            SiteInitializer initializer = SiteInitializer.GetInitializer();
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            var EventsCalendarViewConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = SitefinityWebApp.Widgets.Breadcrumb.Breadcrumbs.breadcrumbWidgetVirtualPath + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = typeof(SitefinityWebApp.Widgets.Breadcrumb.Breadcrumbs).Assembly.GetName().Name
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey(SitefinityWebApp.Widgets.Breadcrumb.Breadcrumbs.breadcrumbWidgetVirtualPath + "*"))
            {
                virtualPathConfig.VirtualPaths.Add(EventsCalendarViewConfig);
                Config.GetManager().SaveSection(virtualPathConfig);
            }
        }

        private static void InstallWidget()
        {

            var configManager = ConfigManager.GetManager();
            var config = configManager.GetSection<ToolboxesConfig>();

            var controls = config.Toolboxes["PageControls"];
            var sectionName = "BSolveIT";
            var section = controls.Sections.Where<ToolboxSection>(e => e.Name == sectionName).FirstOrDefault();

            if (section == null)
            {
                section = new ToolboxSection(controls.Sections)
                {
                    Name = sectionName,
                    Title = sectionName,
                    Description = sectionName,
                    ResourceClassId = typeof(PageResources).Name
                };
                controls.Sections.Add(section);
            }

            var controlName = "Breadcrumbs";
            var controlType = typeof(SitefinityWebApp.Widgets.Breadcrumb.Breadcrumbs);
            if (!section.Tools.Any<ToolboxItem>(e => e.Name == controlName))
            {
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = controlName,
                    Title = controlName,
                    Description = controlName,
                    ControlType = controlType.AssemblyQualifiedName
                };
                section.Tools.Add(tool);
            }

            configManager.SaveSection(config);

        }

    }
}
