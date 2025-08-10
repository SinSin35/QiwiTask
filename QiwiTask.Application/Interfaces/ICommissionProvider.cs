
namespace QiwiTask.Application.Interfaces
{
    public interface ICommissionProvider
    {
        Task<decimal> GetCommissionAsync(string paymentGatewayName, string currencyCode);
    }
}
