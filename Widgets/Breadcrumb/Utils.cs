using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Widgets.Breadcrumb
{
    public class Utils
    {
        /// <summary>
        /// Check to see if the style sheet has already been added to the header
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cssRelativeUrl"> </param>
        /// <returns></returns>
        public static bool HasStyleSheetLoaded(Page page, string cssRelativeUrl)
        {
            return
                page.Header.Controls.OfType<Literal>().Select(control => (control).Text).Where(text => text != null).Any
                    (text => text.IndexOf(cssRelativeUrl, StringComparison.Ordinal) != -1);
        }
    }
}