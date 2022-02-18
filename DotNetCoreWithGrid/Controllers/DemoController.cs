using DotNetCoreWithGrid.Models;
using DotNetCoreWithGrid.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using X.PagedList;

namespace DotNetCoreWithGrid.Controllers
{
  public class DemoController : Controller
  {
    private readonly IProductRepository _productRepository;

    public DemoController(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    public IActionResult Index(string search, int sortBy, bool isAsc = true, int? page = 1)
    {
      string searchvalue = string.Empty;
      if (!string.IsNullOrEmpty(search))
      {
        searchvalue = Regex.Replace(search, @"[^a-zA-Z0-9\s]", string.Empty);
      }
      if (page < 0)
      {
        page = 1;
      }
      ProductPagingInfo productPagingInfo = new ProductPagingInfo();
      var pageIndex = (page ?? 1) - 1;
      var pageSize = 5;
      string sortColumn = string.Empty;

      #region sortingColumn
        switch(sortBy)
      {
        case 1:
          if(isAsc)          
            sortColumn = "ProductId";
          else
            sortColumn = "ProductId Desc";
          break;
        case 2:
          if (isAsc)
            sortColumn = "Name";
          else
            sortColumn = "Name Desc";
          break;
        case 3:
          if (isAsc)
            sortColumn = "Price";
          else
            sortColumn = "Price Desc";
          break;
        default:
          break;
      }

      #endregion

      int totalProductsCount = _productRepository.GetProductsCount(searchvalue);
      var products = _productRepository.ProductPagination(searchvalue, sortColumn, page, pageSize).ToList();
      var productPagedList = new StaticPagedList<Products>(products, pageIndex + 1, pageSize, totalProductsCount);
      productPagingInfo.Products = productPagedList;
      productPagingInfo.PageSize = pageSize;
      productPagingInfo.SortBy = sortBy;
      productPagingInfo.IsAsc = isAsc;
      productPagingInfo.Search= searchvalue;
      return View(productPagingInfo); 

    }

    [HttpPost]
    public IActionResult Index(ProductPagingInfo model)
    {
      return View(model);
    }


    }
}
