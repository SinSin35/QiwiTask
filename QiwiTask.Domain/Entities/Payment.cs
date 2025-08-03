using QiwiTask.Domain.Enums;

namespace QiwiTask.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }

    }
}
