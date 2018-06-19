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
        public List<Employees> getAllData()
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [HR].Employees";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Employees");
            List<Employees> employeesList = dataSet.Tables[0].AsEnumerable().Select(
                dataRow => new Employees
                {
                    EmployeeID = dataRow.Field<int>("EmployeeID"),
                    LastName = dataRow.Field<String>("LastName"),
                    FirstName = dataRow.Field<String>("FirstName")
                }).ToList();
            return employeesList;
        }
        public string GetEmployeeName(int employeeID)
        {
            List<Employees> employeeList = this.getAllData();
            Employees employee = employeeList.SingleOrDefault(m => m.EmployeeID == employeeID);

            return (employee != null) ? employee.FirstName + employee.LastName : null;
        }
    }
}