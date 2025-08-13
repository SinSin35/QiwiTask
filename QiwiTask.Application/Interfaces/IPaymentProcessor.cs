using QiwiTask.Application.Dto;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiwiTask.Application.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<bool> ProcessWithRetryAsync(IPaymentGateway gateway, Payment request);
    }
}
