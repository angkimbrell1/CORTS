public static class HtmlHelperExtensions
{
    public static string Image(this HtmlHelper helper,
                                string url,
                                string altText,
                                object htmlAttributes)
    {
        TagBuilder builder = new TagBuilder("img");
        builder.Attributes.Add("src", url);
        builder.Attributes.Add("alt", altText);
        builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        return builder.ToString(TagRenderMode.SelfClosing);
    }
}