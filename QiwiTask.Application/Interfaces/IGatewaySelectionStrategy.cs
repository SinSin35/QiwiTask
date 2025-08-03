using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Application.Interfaces
{
    public interface IGatewaySelectionStrategy
    {
        IPaymentGateway Select(Payment payment, IEnumerable<IPaymentGateway> gateways);
    }
}
