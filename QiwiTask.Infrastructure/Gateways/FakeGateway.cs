
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Enums;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Infrastructure.Gateways
{
    public class FakeGateway : IPaymentGateway
    {
        public string Name => "FakeGateway";

        public bool IsAvailable() => true;

        public bool DoesSupportCurrency(Currency currency) => true;

        public decimal GetCommissionPercentage(Currency currency) => 0.02m;

        public Task<bool> ProcessAsync(Payment payment)
        {
            Console.WriteLine($"[FakeGateway] Processing payment {payment.Amount} {payment.Currency}");
            return Task.FromResult(true);
        }
    }
}
