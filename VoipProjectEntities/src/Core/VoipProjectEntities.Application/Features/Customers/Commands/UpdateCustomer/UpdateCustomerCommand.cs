﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VoipProjectEntities.Application.Responses;

namespace VoipProjectEntities.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Response<Guid>>
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ISMigrated { get; set; }
        public int CustomerTypeID { get; set; } //enum
        public bool ISTrialBalanceOpted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
