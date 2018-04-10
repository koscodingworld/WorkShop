using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkShop.Models
{
    public class Orders
    {
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }

        [DisplayName("客戶名稱")]
        public int CustomID { get; set; }

        [DisplayName("負責員工名稱")]
        public int EmployeeID { get; set; }

        [DisplayName("訂單日期")]
        public DateTime OrderDate { get; set; }

        [DisplayName("需要日期")]
        public DateTime RequireDate { get; set; }

        [DisplayName("出貨日期")]
        public DateTime ShippedDate { get; set; }

        [DisplayName("出貨公司名稱")]
        public int ShipperID { get; set; }

        [DisplayName("運費")]
        public Decimal Freight { get; set; }

        [DisplayName("出貨地址")]
        public String ShipAddress { get; set; }

        [DisplayName("出貨城市")]
        public String ShipCity { get; set; }

        [DisplayName("出貨地區")]
        public String ShipRegion { get; set; }

        [DisplayName("郵遞區號")]
        public String ShipPostalCode { get; set; }

        [DisplayName("出貨國家")]
        public String ShipCountry { get; set; }


        public List<Orders> Initialize()
        {
            var OrderList = new List<Orders>() {
                new Orders{
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