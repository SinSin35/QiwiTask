using QiwiTask.Domain.Enums;

namespace QiwiTask.Application.Dto
{
    public class PaymentRequesstDto
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        public string IdempotencyKey { get; set; } = Guid.NewGuid().ToString();
    }
}
