Type.registerNamespace("SitefinityWebApp.Widgets.Breadcrumb.Designer");

SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner = function (element) {
    /* Initialize ShowHomePage fields */
    this._showHomePage = null;
    
    /* Initialize IncludeVirtualNodes fields */
    this._includeVirtualNodes = null;
    
    /* Initialize CssFile fields */
    this._cssFile = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI ShowHomePage */
        jQuery(this.get_showHomePage()).attr("checked", controlData.ShowHomePage);

        /* RefreshUI IncludeVirtualNodes */
        jQuery(this.get_includeVirtualNodes()).attr("checked", controlData.IncludeVirtualNodes);

        /* RefreshUI CssFile */
        jQuery(this.get_cssFile()).val(controlData.CssFile);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges ShowHomePage */
        controlData.ShowHomePage = jQuery(this.get_showHomePage()).is(":checked");

        /* ApplyChanges IncludeVirtualNodes */
        controlData.IncludeVirtualNodes = jQuery(this.get_includeVirtualNodes()).is(":checked");

        /* ApplyChanges CssFile */
        controlData.CssFile = jQuery(this.get_cssFile()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* ShowHomePage properties */
    get_showHomePage: function () { return this._showHomePage; }, 
    set_showHomePage: function (value) { this._showHomePage = value; },

    /* IncludeVirtualNodes properties */
    get_includeVirtualNodes: function () { return this._includeVirtualNodes; }, 
    set_includeVirtualNodes: function (value) { this._includeVirtualNodes = value; },

    /* CssFile properties */
    get_cssFile: function () { return this._cssFile; }, 
    set_cssFile: function (value) { this._cssFile = value; }
}

SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner.registerClass('SitefinityWebApp.Widgets.Breadcrumb.Designer.BreadcrumbsDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
