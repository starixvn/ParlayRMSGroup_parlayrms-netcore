namespace ParlayRMS.Site.Utilities;

using NUglify.Css;
using NUglify.JavaScript;
using WebOptimizer;

public static class Bundle
{
    public static void AddLayoutBundle(this IAssetPipeline pipeline, CssSettings cssSetting, CodeSettings scriptSetting)
    {
        pipeline.AddCssBundle("/content/jquery-ui.css",
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
        pipeline.AddCssBundle("/content/lib.css",
            cssSetting,
            "Content/Libs/jquery-ui/jquery-ui.more.css",
            "Content/Libs/contextmenu/jquery.contextmenu.css",
            "Scripts/Libs/multiselect/jquery.multiselect-customized.css",
            "Scripts/Libs/multiselect/jquery.multiselect.filter.css");
        pipeline.AddCssBundle("/content.css",
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
        
        pipeline.AddJavaScriptBundle("/scripts/lib.js", 
            scriptSetting,
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
        pipeline.AddJavaScriptBundle("/scripts/core.js",
            scriptSetting, 
            "Scripts/Core.js",
            "Scripts/Components.js",
            "Scripts/Confirm.js",
            "Scripts/Popup.js",
            "Scripts/Menu.js",
            "Scripts/Validators.js",
            "Scripts/UpdateLevel.js"
        );
    }
}
