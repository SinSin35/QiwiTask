using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using System.Text.RegularExpressions;

namespace QiwiTask.Application.Validation.Implementations
{
    public class UsdValidator : ICurrencyValidator
    {
        public Task ValidateAsync(Payment request)
        {
            string accountPattern = @"^USD\d{10}$";

            if (!Regex.IsMatch(request.SourceAccount, accountPattern))
                throw new ArgumentException("Invalid USD source account format");

            if (!Regex.IsMatch(request.DestinationAccount, accountPattern))
                throw new ArgumentException("Invalid USD destination account format");

            if (request.Amount <= 0 || request.Amount > 10000)
                throw new ArgumentException("USD amount limit exceeded");

            return Task.CompletedTask;
        }
    }
}
