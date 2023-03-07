using Complete.DAL_Services;
using Complete.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;

namespace Complete.Controllers
{
    public class JqueryDatatableController : Controller
    { private readonly ICustomer _customer;
        public JqueryDatatableController(ICustomer customer)
        {
            _customer = customer;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowGrid()
        {
            return View();
        }
     
        public IActionResult GetAllEmployee()
        {
            var data = _customer.GetCustomer();
            return new JsonResult(data);

        }
        [HttpPost]
        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // getting all Customer data  
                //var customerData = _customer.GetCustomer();
                // //var customerData = (from tempcustomer in customerlist
                // //                    select tempcustomer);
                // if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                // {
                //     customerData = customerData.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection).ToList();
                // }
                // if (!string.IsNullOrEmpty(searchValue))
                // {
                //     customerData = customerData.Where(m => m.Name == searchValue);
                // }
                // Create a new SqlConnection object and open the connection
                using (MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
                {
                    connection.Open();

                    // Create a new SqlCommand object to retrieve the customer data
                    MySqlCommand command = new MySqlCommand("SELECT * FROM jquerygridtable", connection);

                    // Sort the results if a sort column and direction were specified
                    if (!(string.IsNullOrEmpty(sortColumn) || string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        command.CommandText += " ORDER BY " + sortColumn + " " + sortColumnDirection;
                    }

                    // Filter the results if a search value was specified
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        command.CommandText += " WHERE name = @searchValue";
                        command.Parameters.AddWithValue("@searchValue", searchValue);
                    }

                    // Execute the command and retrieve the results
                    MySqlDataReader reader = command.ExecuteReader();
                    List<Customer_model> customers = new List<Customer_model>();
                    while (reader.Read())
                    {
                        Customer_model customer = new Customer_model();
                        customer.CustomerID = (int)reader["customerid"];
                        customer.Name = reader["name"].ToString();
                        customer.Address = reader["address"].ToString();
                        customer.Country = reader["country"].ToString();
                        customer.City = reader["city"].ToString();
                        customer.PhoneNo = reader["phoneno"].ToString();
                        customers.Add(customer);
                    }
                    connection.Close();
                    //total number of rows counts   
                    recordsTotal = customers.Count();
                    //Paging   
                    var data = customers.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult Delete(int? ID)
        {   
           bool result= _customer.Delete(ID);
            if (ID == null)
            {
                return Json(new { data = "Not deleleted" });
            }
            if (result == true)
            {
                return Json(new { data = "Deleted" });
            }
            else
            {
                return Json(new { data = "No entries deleted" });
            }
        }
    }
}
