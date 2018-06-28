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
        public List<OrderDetail> Details { get; set; }
        
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }
        
        [DisplayName("客戶名稱")]
        public int CustomerID { get; set; }

        [Required()]
        [DisplayName("負責員工")]
        public int EmployeeID { get; set; }

        [Required()]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("訂單日期")]
        public DateTime OrderDate { get; set; }

        [Required()]
        [DisplayName("需要日期")]
        public DateTime RequiredDate { get; set; }

        [DisplayName("出貨日期")]
        public DateTime? ShippedDate { get; set; }

        [Required()]
        [DisplayName("出貨公司")]
        public int ShipperID { get; set; }

        [Required()]
        [DisplayName("運費")]
        public Decimal Freight { get; set; }

        [Required()]
        [DisplayName("出貨地址")]
        public String ShipAddress { get; set; }

        [Required()]
        [DisplayName("出貨城市")]
        public String ShipCity { get; set; }

        [DisplayName("出貨地區")]
        public String ShipRegion { get; set; }

        [DisplayName("郵遞區號")]
        public String ShipPostalCode { get; set; }

        [Required()]
        [DisplayName("出貨國家")]
        public String ShipCountry { get; set; }
        
    }
}