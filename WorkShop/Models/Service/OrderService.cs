using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkShop.Models.Dao;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WorkShop.Models.Service
{
    public class OrderService
    {
        public List<Orders> getAllData() {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            String sql = "SELECT * FROM [Sales].Orders";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Orders");
            List<Orders> orderList = dataSet.Tables[0].AsEnumerable().Select(
               dataRow => new Orders
               {
                   OrderID = dataRow.Field<int>("OrderID"),
                   EmployeeID = dataRow.Field<int>("EmployeeID"),
                   OrderDate = dataRow.Field<DateTime>("OrderDate"),
                   RequiredDate = dataRow.Field<DateTime>("RequiredDate"),
                   ShippedDate = dataRow.Field<DateTime?>("ShippedDate"),
                   ShipperID = dataRow.Field<int>("ShipperID"),
                   Freight = dataRow.Field<Decimal>("Freight"),
                   ShipAddress = dataRow.Field<String>("ShipAddress"),
                   ShipCity = dataRow.Field<String>("ShipCity"),
                   ShipRegion = dataRow.Field<String>("ShipRegion"),
                   ShipPostalCode = dataRow.Field<String>("ShipPostalCode"),
                   ShipCountry = dataRow.Field<String>("ShipCountry"),
               }).ToList();
            return orderList;
        }

        public Orders getOrderById(int OrderID)
        {
            List <Orders> orderAllData= this.getAllData();
            Orders orderData = orderAllData.Single(m => m.OrderID == OrderID);
            
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            string sqlDetail = "select * from Sales.OrderDetails where OrderID = @Id";
            SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
            cmdDetail.Parameters.Add(new SqlParameter("@Id", OrderID));

            SqlDataAdapter adapterDetail = new SqlDataAdapter(cmdDetail);
            DataSet dsDetail = new DataSet();
            adapterDetail.Fill(dsDetail);
            orderData.Details = new List<OrderDetail>();
            foreach (DataRow detailRow in dsDetail.Tables[0].Rows)
            {
                orderData.Details.Add(new OrderDetail
                {
                    OrderID = int.Parse(detailRow["OrderID"].ToString()),
                    ProductID = int.Parse(detailRow["ProductID"].ToString()),
                    UnitPrice = decimal.Parse(detailRow["UnitPrice"].ToString()),
                    Qty = int.Parse(detailRow["Qty"].ToString()),
                });
            }

            return orderData;
        }


        public int InsertOrderReturnNewOrderId(Orders newOrder)
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            string sql = @"
                INSERT INTO [Sales].[Orders]
                            ([CustomerID]
                            ,[EmployeeID]
                            ,[OrderDate]
                            ,[RequiredDate]
                            ,[ShippedDate]
                            ,[ShipperID]
                            ,[Freight]
                            ,[ShipName]
                            ,[ShipAddress]
                            ,[ShipCity]
                            ,[ShipRegion]
                            ,[ShipPostalCode]
                            ,[ShipCountry])
                        VALUES
                            (@CustomerID
                            ,@EmployeeID
                            ,@OrderDate
                            ,@RequiredDate
                            ,@ShippedDate
                            ,@ShipperID
                            ,@Freight
                            ,@ShipName
                            ,@ShipAddress
                            ,@ShipCity
                            ,@ShipRegion
                            ,@ShipPostalCode
                            ,@ShipCountry)
                    SELECT SCOPE_IDENTITY()
                ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID", newOrder.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", newOrder.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", newOrder.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", newOrder.RequiredDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", newOrder.ShippedDate.HasValue ? newOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", newOrder.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@Freight", newOrder.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", newOrder.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", newOrder.ShipCity));
            cmd.Parameters.Add(new SqlParameter("@ShipRegion", string.IsNullOrWhiteSpace(newOrder.ShipRegion) ? " " : newOrder.ShipRegion));
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", string.IsNullOrWhiteSpace(newOrder.ShipPostalCode) ? " " : newOrder.ShipPostalCode));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", newOrder.ShipCountry));

            int orderId;
            conn.Open();
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                foreach (var detail in newOrder.Details)
                {
                    SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                    cmdDetail.Transaction = transaction;
                    cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                    cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                    cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                    cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                    cmdDetail.ExecuteNonQuery();
                }
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
            return orderId;
        }

        public int UpdateOrder(Orders oldOrder) {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();
            string sql = @"
                    UPDATE [Sales].[Orders]
                    SET 
                        [CustomerID] = @CustomerID
                        ,[EmployeeID] = @EmployeeID
                        ,[OrderDate] = @OrderDate
                        ,[RequiredDate] = @RequiredDate
                        ,[ShippedDate] = @ShippedDate
                        ,[ShipperID] = @ShipperID
                        ,[Freight] = @Freight
                        ,[ShipName] = @ShipName
                        ,[ShipAddress] = @ShipAddress
                        ,[ShipCity] = @ShipCity
                        ,[ShipRegion] = @ShipRegion
                        ,[ShipPostalCode] = @ShipPostalCode
                        ,[ShipCountry] = @ShipCountry
                    WHERE [OrderID] = @OrderID

                    DELETE FROM Sales.OrderDetails WHERE OrderID = @OrderID
            ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", oldOrder.OrderID));
            cmd.Parameters.Add(new SqlParameter("@CustomerID", oldOrder.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", oldOrder.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", oldOrder.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", oldOrder.RequiredDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", oldOrder.ShippedDate.HasValue ? oldOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", oldOrder.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@Freight", oldOrder.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", oldOrder.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", oldOrder.ShipCity));
            cmd.Parameters.Add(new SqlParameter("@ShipRegion", string.IsNullOrWhiteSpace(oldOrder.ShipRegion) ? oldOrder.ShipRegion : " "));
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", string.IsNullOrWhiteSpace(oldOrder.ShipPostalCode) ? oldOrder.ShipPostalCode : " "));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", oldOrder.ShipCountry));

            conn.Open();
            cmd.ExecuteNonQuery();
            int orderId;
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                orderId = Convert.ToInt32(oldOrder.OrderID);
                if (oldOrder.Details != null)
                {
                    string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                    foreach (var detail in oldOrder.Details)
                    {
                        SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                        cmdDetail.Transaction = transaction;
                        cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                        cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                        cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                        cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                        cmdDetail.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
            return orderId;
        }
        public int DeleteOrder(int orderId)
        {
            DaoConnect daoConnect = new DaoConnect();
            SqlConnection conn = daoConnect.SqlConnect();

            string sql = @"
                    DELETE FROM  [Sales].[OrderDetails]
                    WHERE OrderID = @OrderID
                    DELETE FROM  [Sales].[Orders]
                    WHERE OrderID = @OrderID
                ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", orderId));

            conn.Open();
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.ExecuteNonQuery();
                // 全部動作做完後執行Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                // 有出問題則將此交易內的所有更動的資料Rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
            return orderId;
        }
    }
}
