using Dapper;
using DotNetCoreWithGrid.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DotNetCoreWithGrid.Repository
{
  public class ProductRepository : IProductRepository
  {
    public int GetProductsCount(string search)
    {
      using (SqlConnection con = new SqlConnection(ShareConnectionString.Value))
      {
        var para = new DynamicParameters();
        para.Add("@Search",search);
        var data = con.Query<int>("GetProductCount_Search", para, commandType: CommandType.StoredProcedure).FirstOrDefault();
        return data;
      }
      //throw new System.NotImplementedException();
    }

    public List<Products> ProductPagination(string search, string orderBy, int? pageNumber, int pageSize)
    {
      using (SqlConnection con = new SqlConnection(ShareConnectionString.Value))
      {
        var para = new DynamicParameters();
        para.Add("@OrderBy", orderBy);
        para.Add("@Search", search);
        para.Add("@PageNumber", pageNumber);
        para.Add("@PageSize", pageSize);
        var data = con.Query<Products>("ProductPagination_Search", para, commandType: CommandType.StoredProcedure).ToList();
        return data;
      }
      //throw new System.NotImplementedException();
    }
  }
}
