using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using System.Text.RegularExpressions;

namespace QiwiTask.Application.Validation.Implementations
{
    public class RubValidator : ICurrencyValidator
    {
        public Task ValidateAsync(Payment request)
        {
            string accountPattern = @"^RUB\d{10}$";

            if (!Regex.IsMatch(request.SourceAccount, accountPattern))
                throw new ArgumentException("Invalid RUB source account format");

            if (!Regex.IsMatch(request.DestinationAccount, accountPattern))
                throw new ArgumentException("Invalid RUB destination account format");

            if (request.Amount <= 0 || request.Amount > 500000)
                throw new ArgumentException("RUB amount limit exceeded");

            return Task.CompletedTask;
        }
    }
}
