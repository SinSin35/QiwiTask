using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Enums;

namespace QiwiTask.Infrastructure.Services
{
    public class FakeBalanceVerificationService : IBalanceVerificationService
    {
        public async Task<bool> HasSufficientBalanceAsync(PaymentRequesst request)
        {
            await Task.Delay(150);

            return true;
        }
    }
}
