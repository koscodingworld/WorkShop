using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;

namespace WorkShop.Models.Service
{
    public class CustomerService
    {
        public List<Customers> getAllData() {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [Sales].Customers";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Customers");
            List<Customers> customersList = dataSet.Tables[0].AsEnumerable().Select(
                dataRow => new Customers
                {
                    CustomerID = dataRow.Field<int>("CustomerID"),
                    CompanyName = dataRow.Field<String>("CompanyName"),
                    ContactName = dataRow.Field<String>("ContactName"),
                    Address = dataRow.Field<String>("Address")
                }).ToList();

            return customersList;
        }
    }
}