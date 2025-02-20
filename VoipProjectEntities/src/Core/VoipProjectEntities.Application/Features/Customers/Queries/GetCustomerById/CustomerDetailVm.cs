﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VoipProjectEntities.Application.Features.Customers.Queries.GetCustomerById
{
    public class CustomerDetailVm
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ISMigrated { get; set; }
        public int CustomerTypeID { get; set; } //enum
        public bool ISTrialBalanceOpted { get; set; }
    }
}
