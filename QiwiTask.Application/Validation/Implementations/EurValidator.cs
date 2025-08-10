using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using System.Text.RegularExpressions;

namespace QiwiTask.Application.Validation.Implementations
{
    public class EurValidator : ICurrencyValidator
    {
        public Task ValidateAsync(Payment request)
        {
            string accountPattern = @"^EUR\d{10}$";

            if (!Regex.IsMatch(request.SourceAccount, accountPattern))
                throw new ArgumentException("Invalid EUR source account format");

            if (!Regex.IsMatch(request.DestinationAccount, accountPattern))
                throw new ArgumentException("Invalid EUR destination account format");

            if (request.Amount <= 0 || request.Amount > 8000)
                throw new ArgumentException("EUR amount limit exceeded");

            return Task.CompletedTask;
        }
    }
}
