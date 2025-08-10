using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Enums;

namespace QiwiTask.Application.Validation
{
    public class PaymentValidator : IPaymentValidator
    {
        private readonly Dictionary<Currency, ICurrencyValidator> _validators;

        public PaymentValidator(Dictionary<Currency, ICurrencyValidator> validators)
        {
            _validators = validators;
        }

        public Task ValidateAsync(Payment request)
        {
            if (!_validators.TryGetValue(request.Currency, out var validator))
                throw new ArgumentException($"No validator found for currency {request.Currency}");

            return validator.ValidateAsync(request);
        }
    }
}
