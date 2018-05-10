using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;

namespace WorkShop.Models.Service
{
    public class EmployeeService
    {
        public DataSet getAllData()
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [HR].Employees";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Employees");
            return dataSet;
        }
    }
}