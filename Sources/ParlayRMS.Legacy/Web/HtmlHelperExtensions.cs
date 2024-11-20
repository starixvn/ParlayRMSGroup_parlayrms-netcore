using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Utilities;
namespace ParlayRMS.Legacy.Web;

public static class HtmlHelperExtensions
{
    public static IHtmlContent DropDownListWithMultipleSelected(this IHtmlHelper helper, string id, List<SelectListItem> items)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"<select id='{id}' multiple='multiple'>");
        foreach (var item in items)
        {
            stringBuilder.AppendLine($"<option value='{item.Value}'{(item.Selected ? " selected='selected'" : string.Empty)}>{item.Text}</option>");
        }
        stringBuilder.AppendLine("</select>");
        return new HtmlString(stringBuilder.ToString());
    }

    public static IHtmlContent RadioButtonList(this IHtmlHelper helper, string name, IEnumerable<SelectListItem> selectList)
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in selectList)
        {
            var id = name + item.Value;
            var isChecked = item.Selected;
            var radio = helper.RadioButton(name, item.Value, isChecked, new { id });
            var label = helper.Label(id, item.Text);
            stringBuilder.Append(GetString(radio));
            stringBuilder.Append(GetString(label));
        }

        return new HtmlString(stringBuilder.ToString());
    }

    public static IHtmlContent RenderNumberCell(this IHtmlHelper helper, decimal value)
    {
        var html = value >= 0
        ? $"<td class='numeric'>{Formatter.FormatNumber2(value)}</td>"
        : $"<td class='numeric negative'>{Formatter.FormatNumber2(value)}</td>";

        return new HtmlString(html);
    }
    public static IHtmlContent RenderPercentCell(this IHtmlHelper helper, decimal value)
    {
        var html = value >= 0
            ? $"<td class='center numeric'>{Formatter.FormatNumber2(value)}</td>"
            : $"<td class='center numeric negative'>{Formatter.FormatNumber2(value)}</td>";

        return new HtmlString(html);
    }

    public static string GetString(IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}