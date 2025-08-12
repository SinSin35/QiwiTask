
using QiwiTask.Application.Dto;

namespace QiwiTask.Application.Interfaces
{
    public interface IBalanceVerificationService
    {
        Task<bool> HasSufficientBalanceAsync(PaymentRequesst request);
    }
}
