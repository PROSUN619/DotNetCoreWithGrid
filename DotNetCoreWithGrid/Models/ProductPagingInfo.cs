using X.PagedList;

namespace DotNetCoreWithGrid.Models
{
  public class ProductPagingInfo
  {
    public int? PageSize { get; set; }
    public int SortBy { get; set;}
    public string Search { get; set; }
    public bool IsAsc { get; set; }
    public StaticPagedList<Products> Products { get; set; }
  }
}
