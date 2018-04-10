using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WorkShop.Models;

namespace WorkShop.Controllers
{
    
    public class OrderServiceController : Controller
    {
        public static IList<Customers> CustomersList;
        public static IList<Employees> EmployeesList;
        public static IList<Orders> OrdersList;
        public static IList<Shippers> ShippersList;

        public ActionResult Index() {
            Customers Customers = new Customers();
            CustomersList = Customers.Initialize();

            Employees Employees = new Employees();
            EmployeesList = Employees.Initialize();

            Orders Order = new Orders();
            OrdersList = Order.Initialize();

            Shippers Shippers = new Shippers();
            ShippersList = Shippers.Initialize();
            
            return View(OrdersList);
        }
        public ActionResult OrderInsert()
        {

            return View();
        }
            // GET: OrderSearch
            /*public ActionResult OrderSearch()
            {

            }*/
            /*[HttpPost()]
            public ActionResult OrderSearchResult(OrderSearchArgs OrderSearchArgs)
            {
                Orders Order = new Orders();
                List<Orders> OrdersList = Order.Initialize();

                return View(OrdersList);
            }*/
        }
}