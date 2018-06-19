using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace WorkShop.Models
{
    public class OrderSearchArgs
    {
        [DisplayName("訂單編號")]
        public int? OrderID { get; set; }

        [DisplayName("負責員工")]
        public String EmployeeName { get; set; }

        [DisplayName("出貨公司")]
        public String CompanyName { get; set; }

        [DisplayName("訂購日期")]
        public DateTime? OrderDate { get; set; }

        [DisplayName("出貨日期")]
        public DateTime? ShippedDate { get; set; }

        [DisplayName("需要日期")]
        public DateTime? RequiredDate { get; set; }

    }
}