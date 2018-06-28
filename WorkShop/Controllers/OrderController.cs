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
using System.Web.Script.Serialization;

namespace WorkShop.Controllers
{

    public class OrderController : Controller
    {
        //查詢頁面
        public ActionResult Index()
        {
            EmployeeService employeeService = new EmployeeService();
            List<Employees> employeesList = employeeService.getAllData();
            List<SelectListItem> employeesSelectItemList = employeesList.Select(
                m => new SelectListItem()
                {
                    Text = m.FirstName + m.LastName,
                    Value = m.FirstName + m.LastName
                }).ToList();
            ViewBag.employeesSelectItemList = employeesSelectItemList;

            ShipperService shipperService = new ShipperService();
            List<Shippers> shippersList = shipperService.getAllData();
            List<SelectListItem> shippersSelectItemList = shippersList.Select(
                m => new SelectListItem()
                {
                    Text = m.CompanyName,
                    Value = m.CompanyName
                }).ToList();
            ViewBag.shippersSelectItemList = shippersSelectItemList;
            return View();
        }

        //新增頁面
        [HttpGet]
        public ActionResult OrderInsert()
        {
            CustomerService customerService = new CustomerService();
            List<Customers> customersList = customerService.getAllData();
            List <SelectListItem> customersSelectItemList = customersList.Select(
                m => new SelectListItem()
                {
                    Text = m.CompanyName,
                    Value = "" + m.CustomerID
                }).ToList();
            ViewBag.customersSelectItemList = customersSelectItemList;

            EmployeeService employeeService = new EmployeeService();
            List<Employees> employeesList = employeeService.getAllData();
            List<SelectListItem> employeesSelectItemList = employeesList.Select(
                m => new SelectListItem()
                {
                    Text = m.FirstName + m.LastName,
                    Value = "" + m.EmployeeID
                }).ToList();
            ViewBag.employeesSelectItemList = employeesSelectItemList;

            ShipperService shipperService = new ShipperService();
            List<Shippers> shippersList = shipperService.getAllData();
            List<SelectListItem> shippersSelectItemList = shippersList.Select(
                m => new SelectListItem()
                {
                    Text = m.CompanyName,
                    Value = "" + m.ShipperID
                }).ToList();
            ViewBag.shippersSelectItemList = employeesSelectItemList;

            return View(new Orders());
        }
        [HttpPost]
        public JsonResult OrderInsert(Orders orderInsertArgs)
        {
            int OrderId = 0 ;
            if (ModelState.IsValid) {
                OrderService orderService = new OrderService();
                OrderId = orderService.InsertOrderReturnNewOrderId(orderInsertArgs);
            }
            return Json(OrderId,JsonRequestBehavior.AllowGet) ;
        }

        //修改頁面
        [HttpGet]
        public ActionResult OrderUpdate(int OrderID)
        {
            CustomerService customerService = new CustomerService();
            List<Customers> customersList = customerService.getAllData();
            List<SelectListItem> customersSelectItemList = customersList.Select(
                m => new SelectListItem()
                {
                    Text = m.CompanyName,
                    Value = "" + m.CustomerID
                }).ToList();
            ViewBag.customersSelectItemList = customersSelectItemList;

            EmployeeService employeeService = new EmployeeService();
            List<Employees> employeesList = employeeService.getAllData();
            List<SelectListItem> employeesSelectItemList = employeesList.Select(
                m => new SelectListItem()
                {
                    Text = m.FirstName + m.LastName,
                    Value = "" + m.EmployeeID
                }).ToList();
            ViewBag.employeesSelectItemList = employeesSelectItemList;

            ShipperService shipperService = new ShipperService();
            List<Shippers> shippersList = shipperService.getAllData();
            List<SelectListItem> shippersSelectItemList = shippersList.Select(
                m => new SelectListItem()
                {
                    Text = m.CompanyName,
                    Value = "" + m.ShipperID
                }).ToList();
            ViewBag.shippersSelectItemList = shippersSelectItemList;
            OrderService orderService = new OrderService();
            Orders orderData = orderService.getOrderById(OrderID);
            return View(orderData);
        }
        [HttpPost]
        public JsonResult OrderUpdate(Orders oldOrder) {
            int OrderId = 0;
            if (ModelState.IsValid)
            {
                OrderService orderService = new OrderService();
                OrderId = orderService.UpdateOrder(oldOrder);
            }
            return Json(OrderId, JsonRequestBehavior.AllowGet);
            
        }
        //刪除頁面
        [HttpPost]
        public JsonResult OrderDelete(int OrderID)
        {
            int OrderId = 0;
            if (ModelState.IsValid)
            {
                OrderService orderService = new OrderService();
                OrderId = orderService.DeleteOrder(OrderID);
            }
            return Json(OrderId, JsonRequestBehavior.AllowGet);

        }
        //JsonResult
        //查詢頁面
        [HttpPost]
        public JsonResult OrderSearch(OrderSearchArgs orderSearchArgs) {
            OrderService orderService = new OrderService();
            List<Orders> orderList = orderService.getAllData();
            /**/
            IEnumerable<Orders> orderResult = orderList;
            // 訂單編號
            if (orderSearchArgs.OrderID.HasValue)
            {
                orderResult = orderResult.Where(m => m.OrderID == orderSearchArgs.OrderID.Value);
            }
            // 負責員工
            if (!string.IsNullOrWhiteSpace(orderSearchArgs.EmployeeName))
            {
                EmployeeService employeeService = new EmployeeService();
                orderResult =
                    orderResult.Where(
                        m => employeeService.GetEmployeeName(m.EmployeeID).Contains(orderSearchArgs.EmployeeName)
                    );
            }
            // 訂購日期
            if (orderSearchArgs.OrderDate.HasValue)
            {
                orderResult = orderResult.Where(m => m.OrderDate == orderSearchArgs.OrderDate.Value);
            }
            // 需要日期
            if (orderSearchArgs.RequiredDate.HasValue)
            {
                orderResult = orderResult.Where(m => m.RequiredDate == orderSearchArgs.RequiredDate.Value);
            }
            // 出貨日期
            if (orderSearchArgs.ShippedDate.HasValue)
            {
                orderResult = orderResult.Where(m => m.ShippedDate == orderSearchArgs.ShippedDate.Value);
            }
            // 運輸公司
            if (!string.IsNullOrWhiteSpace(orderSearchArgs.CompanyName))
            {
                ShipperService shipperService = new ShipperService();
                orderResult =
                    orderResult.Where(
                        m => shipperService.GetCompanyName(m.ShipperID).Contains(orderSearchArgs.CompanyName)
                    );
            }
            EmployeeService employeesService = new EmployeeService();
            List<Employees> employeeList = employeesService.getAllData();
            ShipperService shippersService = new ShipperService();
            List<Shippers> ShipperList = shippersService.getAllData();
            List<OrderSearchArgs> orderSearchArgsList = orderResult.Select(
                m => new OrderSearchArgs{
                    OrderID = m.OrderID,
                    EmployeeName = employeeList.Single(empM => empM.EmployeeID == m.EmployeeID).FirstName + employeeList.Single(empM => empM.EmployeeID == m.EmployeeID).LastName,
                    OrderDate = m.OrderDate,
                    RequiredDate = m.RequiredDate,
                    ShippedDate = m.ShippedDate,
                    CompanyName = ShipperList.Single(shipperM => shipperM.ShipperID == m.ShipperID).CompanyName
                }).ToList();
            return Json(orderSearchArgsList, JsonRequestBehavior.AllowGet);
        }
        //取得商品資訊
        [HttpPost]
        public JsonResult GetProductData() {
            ProductsService productsService = new ProductsService();
            List<Products> productsList = productsService.getAllData();
            return Json(productsList, JsonRequestBehavior.AllowGet);
        }
    }
}