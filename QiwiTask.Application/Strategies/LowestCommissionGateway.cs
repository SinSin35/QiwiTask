using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Application.Strategies
{
    public class LowestCommissionGateway : IGatewaySelectionStrategy
    {
        private readonly ICommissionProvider _commissionProvider;
        public LowestCommissionGateway(ICommissionProvider commissionProvider)
        {
            _commissionProvider = commissionProvider;
        }

        public async Task<IPaymentGateway> SelectAsync(Payment payment, IEnumerable<IPaymentGateway> gateways)
        {
            var availableGateways = gateways
                .Where(g => g.IsAvailable() && g.DoesSupportCurrency(payment.Currency))
                .ToList();

            if (!availableGateways.Any())
                throw new InvalidOperationException("No gateway available for currency: " + payment.Currency);

            var commissionTasks = availableGateways
                .Select(async g => new
                {
                    Gateway = g,
                    Commission = await _commissionProvider.GetCommissionAsync(g.Name, payment.Currency)
                })
                .ToList();

            var gatewayCommissions = await Task.WhenAll(commissionTasks);

            return gatewayCommissions
                .OrderBy(x => x.Commission)
                .First()
                .Gateway;

        }
    }
}
