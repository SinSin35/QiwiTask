using Microsoft.AspNetCore.Mvc;
using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Interfaces;

namespace QiwiTask.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ITransactionJournal _journal;

        public PaymentController(IPaymentService paymentService, ITransactionJournal journal)
        {
            _paymentService = paymentService;
            _journal = journal;
        }

        [HttpPost]
        public async Task<IActionResult> Process([FromBody] PaymentRequesstDto dto)
        {
            if (await _journal.ExistsAsync(dto.IdempotencyKey))
            {
                var existing = await _journal.GetAsync(dto.IdempotencyKey);
                return Ok(existing);
            }

            var result = await _paymentService.ProcessAsync(dto);
            await _journal.AddAsync(dto.IdempotencyKey, result);

            return Ok(result);
        }
    }
}
