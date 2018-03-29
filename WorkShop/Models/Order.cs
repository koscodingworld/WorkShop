using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkShop.Models
{
    public class Order
    {
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }
        
        public int CustomID { get; set; }
        
        public int EmployeeID { get; set; }

        [DisplayName("訂購日期")]
        public DateTime OrderDate { get; set; }

        [DisplayName("需要日期")]
        public DateTime RequireDate { get; set; }

        [DisplayName("出貨日期")]
        public DateTime ShippedDate { get; set; }

        public int ShipperID { get; set; }

        public Decimal Freight { get; set; }

        public String ShipAddress { get; set; }

        public String ShipCity { get; set; }

        public String ShipRegion { get; set; }

        public String ShipPostalCode { get; set; }

        public String ShipCountry { get; set; }


        public List<Order> Initialize()
        {
            var OrderList = new List<Order>() {
                new Order{
                    OrderID = 1,
                    CustomID = 1,
                    EmployeeID = 1,
                    OrderDate = Convert.ToDateTime("2018-03-29"),
                    RequireDate = Convert.ToDateTime("2018-03-29"),
                    ShippedDate = Convert.ToDateTime("2018-03-29"),
                    ShipperID = 1,
                    Freight = 2000,
                    ShipAddress = "中山路1號",
                    ShipCity = "台北市",
                    ShipRegion = "中山區",
                    ShipPostalCode = "12345",
                    ShipCountry = "台灣"
                }
            };
            return OrderList;
        }
    }
}