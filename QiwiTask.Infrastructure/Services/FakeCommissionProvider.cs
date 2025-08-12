using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Enums;

namespace QiwiTask.Infrastructure.Services
{
    public class FakeCommissionProvider : ICommissionProvider
    {
        public async Task<decimal> GetCommissionAsync(string gatewayName, Currency currency)
        {
            await Task.Delay(100);

            var rnd = new Random();
            return Math.Round((decimal)(rnd.NextDouble() * 0.02 + 0.01), 4);
        }
    }
}
