using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkShop.Models
{
    public class Employees
    {
        public int EmployeeID { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public List<Employees> Initialize()
        {
            var EmployeesList = new List<Employees>() {
                new Employees{
                    EmployeeID = 1,
                    LastName = "員工",
                    FirstName = "陳"
                },
                new Employees{
                    EmployeeID = 2,
                    LastName = "員工",
                    FirstName = "洪" }
                };
            return EmployeesList;
        }
    }
}