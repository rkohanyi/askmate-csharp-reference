using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AskMateWebApp.Helpers
{
    [HtmlTargetElement("pluralize")]
    public class PluralizeTagHelper : TagHelper
    {
        public string Singular { get; set; }
        public string Plural { get; set; }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            int count = int.Parse(content.GetContent());
            output.TagName = null;
            output.Content.SetContent(count + " " + (count == 0 || count == 1 ? Singular : Plural));
        }
    }
}
