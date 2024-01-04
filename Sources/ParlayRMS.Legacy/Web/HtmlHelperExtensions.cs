using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Utilities;
namespace ParlayRMS.Legacy.Web;

public static class HtmlHelperExtensions
{
    public static IHtmlContent DropDownListWithMultipleSelected(this IHtmlHelper helper, string id, List<SelectListItem> items)
    {
        var result = $"<select id='{id}' multiple='multiple'>";
        foreach (var item in items)
        {
            result +=
                $"<option value='{item.Value}'{(item.Selected ? " selected='selected'" : string.Empty)}>{item.Text}</option>";
        }

        result += "</select>";
        return new HtmlString(result);
    }

    public static IHtmlContent RadioButtonList(this IHtmlHelper helper, string name, IEnumerable<SelectListItem> selectList)
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in selectList)
        {
            var id = name + item.Value;
            var isChecked = item.Selected;
            stringBuilder.Append(helper.RadioButton(name, item.Value, isChecked, new { id }));
            stringBuilder.Append(helper.Label(id, item.Text));
        }

        return new HtmlString(stringBuilder.ToString());
    }

    public static IHtmlContent RenderNumberCell(this IHtmlHelper helper, decimal value)
    {
        if (value >= 0)
        {
            return new HtmlString($"<td class='numeric'>{Formatter.FormatNumber2(value)}</td>");

        }
        else
        {
            return new HtmlString($"<td class='numeric negative'>{Formatter.FormatNumber2(value)}</td>");
        }
    }
    public static IHtmlContent RenderPercentCell(this IHtmlHelper helper, decimal value)
    {
        if (value >= 0)
        {
            return new HtmlString($"<td class='center numeric'>{Formatter.FormatNumber2(value)}</td>");

        }
        else
        {
            return new HtmlString($"<td class='center numeric negative'>{Formatter.FormatNumber2(value)}</td>");
        }
    }
}