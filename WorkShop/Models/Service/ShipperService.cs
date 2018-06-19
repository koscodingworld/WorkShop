using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;

namespace WorkShop.Models.Service
{
    public class ShipperService
    {
        public List<Shippers> getAllData()
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [Sales].Shippers";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Shippers");
            List<Shippers> ShippersList = dataSet.Tables[0].AsEnumerable().Select(
                dataRow => new Shippers
                {
                    ShipperID = dataRow.Field<int>("ShipperID"),
                    CompanyName = dataRow.Field<String>("CompanyName"),
                    Phone = dataRow.Field<String>("Phone")
                }).ToList();
            return ShippersList;
        }
        
        public string GetCompanyName(int shipperID)
        {
            List<Shippers> customers = this.getAllData();
            Shippers shipper = customers.SingleOrDefault(m => m.ShipperID == shipperID);

            return (shipper != null) ? shipper.CompanyName : null;
        }
    }
}