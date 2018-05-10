using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WorkShop.Models.Service;
using System.Data;
using WorkShop.Models;

namespace WorkShop.Controllers
{

    public class OrderController : Controller
    {
        public ActionResult Index()
        {

            OrderService orderService = new OrderService();
            DataSet orderDS = orderService.getAllData();
            List<Orders> orderList = new List<Orders>();
            orderList = orderDS.Tables[0].AsEnumerable().Select(
                dataRow => new Orders()
                {
                    OrderID = dataRow.Field<int>("OrderID"),
                    OrderDate = dataRow.Field<DateTime?>("OrderDate"),
                    RequiredDate = dataRow.Field<DateTime?>("RequiredDate"),
                    ShippedDate = dataRow.Field<DateTime?>("ShippedDate")
                }).ToList();
            /*foreach (DataRow rows in orderDS.Tables[0].Rows) {
                DateTime? date;
                if (rows.ItemArray[3].ToString() != "")
                {
                    date = Convert.ToDateTime(rows.ItemArray[3]);
                }
                else
                {
                    date = null;
                }
                orderList.Add(new Orders() { 
                    OrderID = (int)rows.ItemArray[0],
                    OrderDate = Convert.ToDateTime(rows.ItemArray[1]),
                    RequiredDate = Convert.ToDateTime(rows.ItemArray[2]),
                    ShippedDate = date
                });
            }*/
            return View(orderList);
        }

        [HttpGet]
        public ActionResult OrderInsert()
        {
            CustomerService customerService = new CustomerService();
            DataSet customerDS = customerService.getAllData();
            List<SelectListItem> customersSelectItemList = new List<SelectListItem>();
            customersSelectItemList = customerDS.Tables[0].AsEnumerable().Select(
                dataRow => new SelectListItem()
                {
                    Text = dataRow.Field<String>("CompanyName"),
                    Value = dataRow.Field<int>("CustomerID").ToString()
                }).ToList();
            ViewBag.customersSelectItemList = customersSelectItemList;

            EmployeeService employeeService = new EmployeeService();
            DataSet employeeDS = employeeService.getAllData();
            List<SelectListItem> employeesSelectItemList = new List<SelectListItem>();
            employeesSelectItemList = employeeDS.Tables[0].AsEnumerable().Select(
                dataRow => new SelectListItem()
                {
                    Text = dataRow.Field<String>("FirstName") + dataRow.Field<String>("LastName"),
                    Value = dataRow.Field<int>("EmployeeID").ToString()
                }).ToList();
            ViewBag.employeesSelectItemList = employeesSelectItemList;

            ShipperService shipperService = new ShipperService();
            DataSet shipperDS = shipperService.getAllData();
            List<SelectListItem> shippersSelectItemList = new List<SelectListItem>();
            shippersSelectItemList = shipperDS.Tables[0].AsEnumerable().Select(
                dataRow => new SelectListItem()
                {
                    Text = dataRow.Field<String>("CompanyName"),
                    Value = dataRow.Field<int>("ShipperID").ToString()
                }).ToList();
            ViewBag.shippersSelectItemList = shippersSelectItemList;

            return View(new Orders());
        }
        [HttpPost]
        public ActionResult OrderInsert(Orders orderInsertArgs)
        {
            if (ModelState.IsValid) {

            }
            return RedirectToAction("Index");
        }
    }
}