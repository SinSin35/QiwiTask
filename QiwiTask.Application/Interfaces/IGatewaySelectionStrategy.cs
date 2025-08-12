using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Application.Interfaces
{
    public interface IGatewaySelectionStrategy
    {
        Task<IPaymentGateway> SelectAsync(Payment payment, IEnumerable<IPaymentGateway> gateways);
    }
}
