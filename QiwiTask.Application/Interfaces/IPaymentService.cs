
using QiwiTask.Application.Dto;
using QiwiTask.Domain.Entities;

namespace QiwiTask.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> ProcessAsync (PaymentRequesstDto payment);   
    }
}
