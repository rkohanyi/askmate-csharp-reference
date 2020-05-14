using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AskMate.Web.Helpers
{
    [HtmlTargetElement(Attributes = "sort")]
    public class SortTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var sort = ViewContext.HttpContext.Request.Query["sort"];
            var ascending = ViewContext.HttpContext.Request.Query["ascending"];

            string modelSort = sort.Count == 0 ? "SubmissionTime" : sort[0];
            string modelAscending = ascending.Count == 0 ? "false" : ascending[0];

            string routeSort = context.AllAttributes["asp-route-sort"].Value.ToString();
            string routeAscending = context.AllAttributes["asp-route-ascending"].Value.ToString();

            output.Attributes.RemoveAll("sort");
            if (routeSort.Equals(modelSort) && routeAscending.Equals(modelAscending))
            {
                output.Attributes.SetAttribute("class", "active");
            }
        }
    }
}
