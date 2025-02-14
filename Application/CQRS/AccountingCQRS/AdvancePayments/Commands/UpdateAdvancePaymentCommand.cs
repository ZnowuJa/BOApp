using Application.CQRS.AccountingCQRS.BusinessTravels.Commands;
using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Commands
{
    public class UpdateAdvancePaymentCommand(AdvancePaymentFormVm item) : IRequest<int>
    {
        public AdvancePaymentFormVm Item { get; set; } = item;
    }
    
}