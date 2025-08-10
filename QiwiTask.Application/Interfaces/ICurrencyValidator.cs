
using QiwiTask.Application.Dto;
using QiwiTask.Domain.Entities;

namespace QiwiTask.Application.Interfaces
{
    public interface ICurrencyValidator
    {
        Task ValidateAsync(Payment dto);
    }
}
