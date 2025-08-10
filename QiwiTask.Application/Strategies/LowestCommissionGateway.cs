using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Application.Strategies
{
    public class LowestCommissionGateway : IGatewaySelectionStrategy
    {
        public IPaymentGateway Select(Payment payment, IEnumerable<IPaymentGateway> gateways)
        {
            return gateways
                .Where(g => g.IsAvailable() && g.DoesSupportCurrency(payment.Currency))
                .OrderBy(g => g.GetCommissionPercentage(payment.Currency))
                .FirstOrDefault()
                ?? throw new InvalidOperationException("No gateway available for currency: " + payment.Currency);
        }
    }
}
