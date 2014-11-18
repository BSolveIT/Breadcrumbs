<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto; ">
<ol>        
    <li class="sfFormCtrl">
    <asp:CheckBox runat="server" ID="ShowHomePage" Text="Show Home Page Link" CssClass="sfCheckBox"/>        
    <div class="sfExample"></div>
    </li>
    
    <li class="sfFormCtrl">
    <asp:CheckBox runat="server" ID="IncludeVirtualNodes" Text="Include Virtual Pages" CssClass="sfCheckBox"/>        
    <div class="sfExample"></div>
    </li>
    
    <li class="sfFormCtrl">
    <asp:Label runat="server" AssociatedControlID="CssFile" CssClass="sfTxtLbl">Css File</asp:Label>
    <asp:TextBox ID="CssFile" runat="server" CssClass="sfTxt" />
    <div class="sfExample">e.g.  ~/Widgets/Breadcrumb/Styles/breadcrumb.css</div>
    </li>
    
</ol>
</div>
