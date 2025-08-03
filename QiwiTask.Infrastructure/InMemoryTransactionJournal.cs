using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Infrastructure
{
    public class InMemoryTransactionJournal : ITransactionJournal
    {
        private readonly Dictionary<string, Payment> _store = new();

        public Task<bool> ExistsAsync(string idempotencyKey)
            => Task.FromResult(_store.ContainsKey(idempotencyKey));

        public Task<Payment?> GetAsync(string idempotencyKey)
        {
            _store.TryGetValue(idempotencyKey, out var payment);
            return Task.FromResult(payment);
        }

        public Task AddAsync(string idempotencyKey, Payment payment)
        {
            _store[idempotencyKey] = payment;
            return Task.CompletedTask;
        }
    }
}
