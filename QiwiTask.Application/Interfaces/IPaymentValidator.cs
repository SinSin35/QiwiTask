
using QiwiTask.Application.Dto;
using QiwiTask.Domain.Entities;

namespace QiwiTask.Application.Interfaces
{
    public interface IPaymentValidator
    {
        Task ValidateAsync(Payment request);
    }
}
