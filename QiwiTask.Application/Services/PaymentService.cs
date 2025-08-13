using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Enums;
using QiwiTask.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QiwiTask.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IEnumerable<IPaymentGateway> _gateways;
        private readonly IGatewaySelectionStrategy _gatewayStrategy;
        private readonly IPaymentValidator _paymentValidator;
        private readonly IBalanceVerificationService _balanceVerificationService;
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentService(
            IEnumerable<IPaymentGateway> gateways, 
            IGatewaySelectionStrategy gatewayStrategy,
            IPaymentValidator paymentValidator,
            IBalanceVerificationService balanceVerificationService,
            IPaymentProcessor paymentProcessor)
        {
            _gateways = gateways ?? throw new ArgumentNullException(nameof(gateways));
            _gatewayStrategy = gatewayStrategy ?? throw new ArgumentNullException(nameof(gatewayStrategy));
            _paymentValidator = paymentValidator ?? throw new ArgumentNullException(nameof(paymentValidator));
            _balanceVerificationService = balanceVerificationService;
            _paymentProcessor = paymentProcessor;
        }

        public async Task<Payment> ProcessAsync(PaymentRequesst dto)
        {
            await _paymentValidator.ValidateAsync(dto);

            if (!await _balanceVerificationService.HasSufficientBalanceAsync(dto))
                throw new InvalidOperationException("Insufficient balance for transaction");

            var payment = new Payment
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                SourceAccount = dto.SourceAccount,
                DestinationAccount = dto.DestinationAccount,
                Metadata = dto.Metadata
            };

            var gateway = await _gatewayStrategy.SelectAsync(payment, _gateways);

            var success = await _paymentProcessor.ProcessWithRetryAsync(gateway, payment);

            payment.Status = success ? PaymentStatus.Processed : PaymentStatus.Failed;
            payment.ProcessedAt = DateTime.UtcNow;

            return payment;
        }
    }
}
