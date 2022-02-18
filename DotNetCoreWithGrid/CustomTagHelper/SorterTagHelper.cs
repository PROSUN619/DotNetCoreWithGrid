using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWithGrid.CustomTagHelper
{
	[HtmlTargetElement("tr",Attributes = "isAsc,sortby,pageSize,search")]
	public class SorterTagHelper:TagHelper
	{
		private const int v = 0;
		private IUrlHelperFactory _helperFactory;

    public SorterTagHelper(IUrlHelperFactory urlHelperFactory)
    {
			_helperFactory = urlHelperFactory;

		}

    #region Input Attribute
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext  viewContext { get; set; }

		[HtmlAttributeName("isAsc")]
		public bool IsAsc { get; set; }

		[HtmlAttributeName("sortby")]
		public int SortBy { get; set; }

		[HtmlAttributeName("pageSize")]
		public int? PageSize { get; set; }

		[HtmlAttributeName("search")]
		public string Search { get; set; }
    #endregion

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
			IUrlHelper urlHelper = _helperFactory.GetUrlHelper(viewContext);
      List<string> li = new List<string>
      {
        "ID","Name","Price"
      };
      TagBuilder tr = new TagBuilder("tr");
			int headerid = v;
      for (int i = 1; i <= li.Count; i++)
      {
				TagBuilder th = new TagBuilder("th");
				TagBuilder tag = new TagBuilder("a");
				tag.InnerHtml.Append(li[i - 1].ToString());
				//TagBuilder tagText = new TagBuilder(li[row-1].ToString());
				var tooglesort = (i == SortBy ? (IsAsc).ToString() : "true");
				tag.Attributes["href"] = urlHelper.Action("Index", "Demo",
					new { page = PageSize, SortBy = i, IsAsc = tooglesort, Search = Search });
				
				if (SortBy != 0)
        {
					if (i == SortBy)
          {
						TagBuilder tagSpan = new TagBuilder("span");
						tagSpan.AddCssClass($"arrow{(IsAsc ? "true" : "false")}");
						th.InnerHtml.AppendHtml(tagSpan);
					}
        }
				th.InnerHtml.AppendHtml(tag);
				tr.InnerHtml.AppendHtml(th);
				headerid += 1;
      }
			output.Content.AppendHtml(tr.InnerHtml);
      //return base.ProcessAsync(context, output);
    }
  }
}
