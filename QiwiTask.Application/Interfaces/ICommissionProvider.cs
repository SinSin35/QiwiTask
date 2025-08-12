
using QiwiTask.Domain.Enums;

namespace QiwiTask.Application.Interfaces
{
    public interface ICommissionProvider
    {
        Task<decimal> GetCommissionAsync(string gatewayName, Currency currency);
    }
}
