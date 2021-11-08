﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        string Baseurl = "https://localhost:44330/";

        #region "Get Customer Type Enum Value"
        //public int GetEnumValue(string Type)
        //{
        //    int enumInt = (int)Enum.Parse(typeof(CustomerType), Type);
        //    return enumInt;
        //}
        #endregion

        #region "Get All Customers"
        public List<CustomerModel> GetAllCustomers()
        {
            string api = "api/Customer";
            List<CustomerModel> CustomerList = GetCustomerList(api);

            return CustomerList;
        }
        #endregion

        #region "Validate Login"
        public List<CustomerModel> ValidateLogin(CustomerModel customer)
        {
            string api = "api/Customer/ValidateLogin/" + customer.CustomerName + "/" + customer.Password;
            List<CustomerModel> CustomerList = GetCustomerList(api);

            return CustomerList;
        }
        #endregion

        #region "Validate Email"
        public List<CustomerModel> ValidateEmail(string Email)
        {
            string api = "api/Customer/ValidateEmail/" + Email;
            List<CustomerModel> CustomerList = GetCustomerList(api);       

            return CustomerList;
        }
        #endregion

        #region "Create Customer"
        public CustomerModel CreateCustomer(CustomerModel customer)
        {
            customer.CustomerTypeID = CustomerType.Demo;
            customer.ISMigrated = false;
            customer.ISTrialBalanceOpted = true;
            customer.CreatedAt = DateTime.Now;
            customer.UpdatedAt = DateTime.Now;

            try
            {
                CustomerModel CM = new CustomerModel();
                HttpClient HC = new HttpClient();
                Root result = new Root();

                var insertedRecord = HC.PostAsJsonAsync(Baseurl + "api/Customer", customer);
                insertedRecord.Wait();

                var results = insertedRecord.Result;

                if (results.IsSuccessStatusCode)
                {
                    var UserResponse = results.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Root>(UserResponse);
                    CM = result.data;
                }

                HC.Dispose();
                return CM;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Create Menu Access"
        public bool CreateMenuAccess(MenuAccessModel menu)
        {
            bool FunctionReturnValue = false;
            menu.CreatedAt = DateTime.Now;
            menu.UpdatedAt = DateTime.Now;
            menu.IsAccess = true;

            try
            {
                HttpClient HC = new HttpClient();
                HC.BaseAddress = new Uri(Baseurl);

                var insertedRecord = HC.PostAsJsonAsync("api/Menu", menu);
                insertedRecord.Wait();

                var result = insertedRecord.Result;

                if (result.IsSuccessStatusCode)
                {
                    FunctionReturnValue = true;
                }

                HC.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return FunctionReturnValue;
        }
        #endregion

        #region "Delete Customer"
        public void DeleteCustomer(string email)
        {
            List<CustomerModel> CustomerList = ValidateEmail(email);

            if(CustomerList.Count > 0)
            {
                try
                {
                    HttpClient HC = new HttpClient();
                    HC.BaseAddress = new Uri(Baseurl);

                    var insertedRecord = HC.DeleteAsync("api/Customer/" + CustomerList[0].CustomerId);
                    insertedRecord.Wait();

                    HC.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region "Get Customer By Id"
        public CustomerModel GetCustomerById(string CustomerId)
        {
            try
            {
                CustomerModel customer = new CustomerModel();

                HttpClient HC = new HttpClient();
                Root result = new Root();

                var insertedRecord = HC.GetAsync(Baseurl + "api/Customer/" + CustomerId);

                insertedRecord.Wait();

                var results = insertedRecord.Result;

                if (results.IsSuccessStatusCode)
                {
                    var UserResponse = results.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Root>(UserResponse);
                    customer = result.data;
                }

                HC.Dispose();
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Common Method - Get Customer List"
        public List<CustomerModel> GetCustomerList(string api)
        {
            try
            {
                List<CustomerModel> CustomerList = new List<CustomerModel>();

                HttpClient HC = new HttpClient();
                RootObject result = new RootObject();


                var insertedRecord =  HC.GetAsync(Baseurl + api);
                insertedRecord.Wait();

                var results = insertedRecord.Result;

                if (results.IsSuccessStatusCode)
                {
                    var UserResponse = results.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                    CustomerList = result.data.ToList();
                }

                HC.Dispose();
                return CustomerList;
            }
            catch (Exception)
            {
                throw;
            }
        }        
        #endregion

        #region "Root Object"
        public class RootObject
        {
            public string status { get; set; }
            public CustomerModel[] data { get; set; }
        }

        public class Root
        {
            public string status { get; set; }
            public CustomerModel data { get; set; }
        }
        #endregion
    }
}
