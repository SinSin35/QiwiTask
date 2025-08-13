using QiwiTask.Application.Dto;
using QiwiTask.Application.Interfaces;
using QiwiTask.Domain.Entities;
using QiwiTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiwiTask.Application.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private const int MaxRetries = 3;
        private const int BaseDelayMilliseconds = 200;
        public async Task<bool> ProcessWithRetryAsync(IPaymentGateway gateway, Payment request)
        {
            int attempt = 0;
            int delay = BaseDelayMilliseconds;

            while (attempt < MaxRetries)
            {
                try
                {
                    attempt++;
                    if (await gateway.ProcessAsync(request))
                        return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Attempt {attempt}] Error: {ex.Message}");
                }

                if (attempt < MaxRetries)
                {
                    await Task.Delay(delay);
                    delay *= 2; 
                }
            }

            return false;
        }
    }
}
