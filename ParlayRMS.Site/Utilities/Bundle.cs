namespace ParlayRMS.Site.Utilities;

using NUglify.Css;
using WebOptimizer;
using WebOptimizer.Processors;

public static class Bundle
{
    public static void AddLayoutBundle(this IAssetPipeline pipeline, CssSettings cssSetting, JsSettings jsSettings)
    {
        pipeline.AddCssBundle("/Content/jquery-ui.css",
            cssSetting,
            "Content/Libs/jquery-ui/themes/base/jquery.ui.accordion.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.autocomplete.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.button.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.core.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.datepicker.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.dialog.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.menu.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.progressbar.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.resizable.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.selectable.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.slider.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.spinner.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.tabs.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.theme.css",
            "Content/Libs/jquery-ui/themes/base/jquery.ui.tooltip.css");
        pipeline.AddCssBundle("/Content/lib.css",
            cssSetting,
            "Content/Libs/jquery-ui/jquery-ui.more.css",
            "Content/Libs/contextmenu/jquery.contextmenu.css",
            "Scripts/Libs/multiselect/jquery.multiselect-customized.css",
            "Scripts/Libs/multiselect/jquery.multiselect.filter.css",
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.css",
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.filter.css",
            "Content/Libs/simplepagination/simplePagination.css");
        pipeline.AddCssBundle("/Content.css",
            cssSetting,
            "Content/google-abel-font.css",
            "Content/BetList.css",
            "Content/Components.css",
            "Content/confirm.css",
            "Content/Icon.css",
            "Content/modalPopup.css",
            "Content/Paging.css",
            "Content/Site.css",
            "Content/Validators.css");

        pipeline.AddCssBundle("/Content/Menu.css", 
            cssSetting,
            "Content/Menu.css");

        pipeline.AddCssBundle("/Content/jquery-ui-multiselect.css",
            cssSetting,
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.css",
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.filter.css");

        pipeline.AddJavaScriptBundle("/Scripts/lib.js", 
            jsSettings,
            "Scripts/jquery-1.9.0.js",
            "Scripts/Libs/bettertooltip/jquery.betterTooltip.js",
            "Scripts/Libs/contextmenu/jquery.contextMenu.js",
            "Scripts/Libs/cookie/jquery.cookie.js",
            "Scripts/Libs/jquery-ui/jquery-ui.custom.js",
            "Scripts/Libs/jquery-ui/jquery-ui.more.js",
            "Scripts/autoNumeric/autoNumeric.js",
            "Scripts/Libs/moment/moment.js",
            "Scripts/Libs/multiselect/jquery.multiselect-customized.js",
            "Scripts/Libs/multiselect/jquery.multiselect.filter.js",
            
            "Scripts/Libs/numeral/numeral.js",
            "Scripts/Libs/simplemodal/jquery.simplemodal.js",
            "Scripts/Libs/sortElements/jquery.sortElements.js",
            "Scripts/Libs/soundManager/soundmanager2-nodebug.js");
        pipeline.AddJavaScriptBundle("/Scripts/core.js",
            jsSettings, 
            "Scripts/Core.js",
            "Scripts/Components.js",
            "Scripts/Confirm.js",
            "Scripts/Popup.js",
            "Scripts/Menu.js",
            "Scripts/Validators.js",
            "Scripts/UpdateLevel.js"
        );

        pipeline.AddJavaScriptBundle("/Scripts/Menu.LayoutAngular.js",
            jsSettings,
            "Scripts/jquery-1.9.0.js",
            "Scripts/Menu.LayoutAngular.js");

        pipeline.AddJavaScriptBundle("/Scripts/jquery-ui-multiselect.js",
            jsSettings,
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.js",
            "Scripts/Libs/multiselect/jquery-ui-multiselect-widget-3.0.0/jquery.multiselect.filter.js");

    }
}
