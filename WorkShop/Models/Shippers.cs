using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkShop.Models
{
    public class Shippers
    {
        public int ShipperID { get; set; }

        public String CompanyName { get; set; }

        public String Phone { get; set; }

        public List<Shippers> Initialize()
        {
            var ShippersList = new List<Shippers>() {
                new Shippers{
                    ShipperID = 1,
                    CompanyName = "運送公司1",
                    Phone = "0912345678"
                },
                new Shippers{
                    ShipperID = 2,
                    CompanyName = "運送公司2",
                    Phone = "0912345678"
                }
            };
            return ShippersList;
        }
    }
}