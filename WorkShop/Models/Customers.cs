using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkShop.Models
{
    public class Customers
    {
        public int CustomerID { get; set; }

        public String CompanyName { get; set; }

        public String ContactName { get; set; }

        public String Address { get; set; }

        public List<Customers> Initialize()
        {
            var CustomersList = new List<Customers>() {
                new Customers{
                    CustomerID = 1,
                    CompanyName = "水龍頭",
                    ContactName = "陳先生",
                    Address = "台北市中山區中山路1號"
                },
                new Customers{
                    CustomerID = 2,
                    CompanyName = "餅乾",
                    ContactName = "乾先生",
                    Address = "台北市中山區中山路2號"
                }
            };
            return CustomersList;
        }
    }
}