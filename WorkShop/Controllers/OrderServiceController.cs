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
        // GET: OrderSearch
        public ActionResult OrderSearch()
        {
            Order Order = new Order();
            IList<Order> OrderList = Order.Initialize();

            Employees Employees = new Employees();
            IList<Employees> EmployeesList = Employees.Initialize();

            Shippers Shippers = new Shippers();
            IList<Shippers> ShippersList = Shippers.Initialize();

            OrderSearchArgs OrderSearchArgs = new OrderSearchArgs();
            
            //list.Where(o => o.CustomName == "XXX").ToList(); => CustomID
            List<SelectListItem> EmployeeListItem = new List<SelectListItem>();
            foreach (Employees EmployeesData in EmployeesList) {
                EmployeeListItem.Add(new SelectListItem() {
                    Text = EmployeesData.FirstName + EmployeesData.LastName,
                    Value = EmployeesData.EmployeeID.ToString()
                });
            }
            ViewBag.EmployeeListItem = EmployeeListItem;

            List<SelectListItem> ShipperListItem = new List<SelectListItem>();
            foreach (Shippers ShippersData in ShippersList)
            {
                ShipperListItem.Add(new SelectListItem()
                {
                    Text = ShippersData.CompanyName,
                    Value = ShippersData.ShipperID.ToString()
                });
            }
            ViewBag.ShipperListItem = ShipperListItem;

            return View(OrderSearchArgs);
        }
        [HttpPost()]
        public ActionResult OrderSearchResult(OrderSearchArgs OrderSearchArgs)
        {
            Order Order = new Order();
            List<Order> OrdersList = Order.Initialize();

            return View(OrdersList);
        }
    }
}