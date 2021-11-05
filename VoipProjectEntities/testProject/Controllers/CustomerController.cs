using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testProject.Models;
using testProject.Repositories;

namespace testProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo repo;
        const string CustomerId = "";
        public CustomerController(ICustomerRepo _repo)
        {
            repo = _repo;
        }

        #region "Get All Customers / Get All Existing Users"
        public IActionResult Index()
        {
            List<CustomerModel> CustomerList = repo.GetAllCustomers();
            ViewBag.ShowAlert = false;

            if (CustomerList.Count > 0)
            {
                return View(CustomerList);
            }
            else
            {
                return ViewBag.ShowAlert = true;
            }
        }
        #endregion

        #region "Sign Up"
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.ShowAlert = false;
            ViewBag.ShowAlertEmail = false;
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(CustomerModel customer)
        {
            List<CustomerModel> CustomerList = repo.ValidateEmail(customer.Email);

            if (CustomerList.Count > 0)
            {
                ViewBag.ShowAlertEmail = true;
                return View();
            }
            else
            {
                CustomerModel Cust = repo.CreateCustomer(customer);

                if (Cust != null)
                {
                    MenuAccessModel menu = new MenuAccessModel();
                    menu.CustomerID = Cust.CustomerId;
                    menu.MenuLink = MenuLink.ManageCallHistory;

                    if (repo.CreateMenuAccess(menu))
                    {
                        return RedirectToAction("Customer", "Index");
                    }
                    else
                    {
                        repo.DeleteCustomer(Cust.Email);
                        ViewBag.ShowAlert = true;
                        return View();
                    }
                }
                else
                {
                    ViewBag.ShowAlert = true;
                    return View();
                }
            }
        }
        #endregion

        #region "Login"
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ShowAlert = "";
            return View();
        }

        [HttpPost]
        public IActionResult Login(CustomerModel customer)
        {
            //int custTypeid = repo.GetEnumValue(Convert.ToString(customer.CustomerTypeID));

            List<CustomerModel> CustomerList = repo.ValidateLogin(customer);

            if (CustomerList.Count > 0)
            {
                if(CustomerList[0].CustomerTypeID == customer.CustomerTypeID)
                {
                    CustomerModel Customer = repo.GetCustomerById(CustomerList[0].CustomerId);
                    HttpContext.Session.SetString(CustomerId, Customer.CustomerId);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ShowAlert = "customertype_error";
                    return View();
                }
            }
            else
            {
                ViewBag.ShowAlert = "login_error";
                return View();
            }

        }
        #endregion

        #region "Forgot Password"
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewBag.ShowAlert = false;
            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public ActionResult ForgotPasswordPost()
        {
            string email = Request.Form["Email"];

            List<CustomerModel> CustomerList = repo.ValidateEmail(email);

            if (CustomerList.Count > 0)
            {
                return RedirectToAction("Home", "Index");
            }
            else
            {
                ViewBag.ShowAlert = true;
                return View();
            }
        }
        #endregion

    }
}
