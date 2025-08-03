
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Enums;

namespace QiwiTask.Domain.Interfaces
{
    public interface IPaymentGateway
    {
        string Name { get; }
        bool IsAvailable();
        bool DoesSupportCurrency(Currency currency);
        decimal GetCommissionPercentage(Currency currency);
        Task<bool> ProcessAsync(Payment payment);
    }
}
