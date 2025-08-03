using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Enums;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IEnumerable<IPaymentGateway> _gateways;
        private readonly IGatewaySelectionStrategy _gatewayStrategy;

        public PaymentService(IEnumerable<IPaymentGateway> gateways, IGatewaySelectionStrategy gatewayStrategy)
        {
            _gateways = gateways ?? throw new ArgumentNullException(nameof(gateways));
            _gatewayStrategy = gatewayStrategy ?? throw new ArgumentNullException(nameof(gatewayStrategy));
        }

        public async Task<Payment> ProcessAsync(PaymentRequesstDto dto)
        {
            var payment = new Payment
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                SourceAccount = dto.SourceAccount,
                DestinationAccount = dto.DestinationAccount,
                Metadata = dto.Metadata
            };

            var gateway = _gatewayStrategy.Select(payment, _gateways);

            var success = await gateway.ProcessAsync(payment);

            payment.Status = success ? PaymentStatus.Processed : PaymentStatus.Failed;
            payment.ProcessedAt = DateTime.UtcNow;

            return payment;
        }
    }
}
