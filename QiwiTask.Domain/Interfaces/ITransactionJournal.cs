using QiwiTask.Domain.Entities;

namespace QiwiTask.Domain.Interfaces
{
    public interface ITransactionJournal
    {
        Task<bool> ExistsAsync(string idempotencyKey);
        Task<Payment?> GetAsync(string idempotencyKey);
        Task AddAsync(string idempotencyKey, Payment payment);
    }
}
