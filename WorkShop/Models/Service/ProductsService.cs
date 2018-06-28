using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;

namespace WorkShop.Models.Service
{
    public class ProductsService
    {
        public List<Products> getAllData()
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [Production].[Products]";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Products");
            List<Products> productsList = dataSet.Tables[0].AsEnumerable().Select(
                dataRow => new Products
                {
                    ProductID = dataRow.Field<int>("ProductID"),
                    ProductName = dataRow.Field<String>("ProductName"),
                    UnitPrice = dataRow.Field<decimal>("UnitPrice")
                }).ToList();

            return productsList;
        }
    }
}