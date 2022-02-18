using DotNetCoreWithGrid.Models;
using System.Collections.Generic;

namespace DotNetCoreWithGrid.Repository
{
  public interface IProductRepository
  {
    int GetProductsCount(string search);
    List<Products> ProductPagination(string search, string orderBy, int? pageNumber, int pageSize);
  }
}
