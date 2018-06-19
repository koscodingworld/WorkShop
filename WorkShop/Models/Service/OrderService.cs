using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;


namespace WorkShop.Models.Service
{
    public class OrderService
    {
        public List<Orders> getAllData() {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [Sales].Orders";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Orders");
            List<Orders> orderList = dataSet.Tables[0].AsEnumerable().Select(
               dataRow => new Orders
               {
                   OrderID = dataRow.Field<int>("OrderID"),
                   EmployeeID = dataRow.Field<int>("EmployeeID"),
                   OrderDate = dataRow.Field<DateTime?>("OrderDate"),
                   RequiredDate = dataRow.Field<DateTime?>("RequiredDate"),
                   ShippedDate = dataRow.Field<DateTime?>("ShippedDate"),
                   ShipperID = dataRow.Field<int>("ShipperID"),
                   Freight = dataRow.Field<Decimal>("Freight"),
                   ShipAddress = dataRow.Field<String>("ShipAddress"),
                   ShipCity = dataRow.Field<String>("ShipCity"),
                   ShipRegion = dataRow.Field<String>("ShipRegion"),
                   ShipPostalCode = dataRow.Field<String>("ShipPostalCode"),
                   ShipCountry = dataRow.Field<String>("ShipCountry"),
               }).ToList();
            return orderList;

        }
    }
}