
using QiwiTask.Application.Interfaces;
using QiwiTask.Application.Services;
using QiwiTask.Application.Strategies;
using QiwiTask.Application.Validation;
using QiwiTask.Application.Validation.Implementations;
using QiwiTask.Domain.Enums;
using QiwiTask.Infrastructure.Services;
using System.Text.Json.Serialization;

namespace QiwiTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }); //?

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IBalanceVerificationService, FakeBalanceVerificationService>();
            builder.Services.AddScoped<IGatewaySelectionStrategy, LowestCommissionGateway>();
            builder.Services.AddScoped<ICommissionProvider, FakeCommissionProvider>();

            builder.Services.AddSingleton<UsdValidator>();
            builder.Services.AddSingleton<EurValidator>();
            builder.Services.AddSingleton<RubValidator>();

            builder.Services.AddSingleton<IPaymentValidator>(sp =>
            {
                var validators = new Dictionary<Currency, ICurrencyValidator>
                {
                    { Currency.USD, sp.GetRequiredService<UsdValidator>() },
                    { Currency.EUR, sp.GetRequiredService<EurValidator>() },
                    { Currency.RUB, sp.GetRequiredService<RubValidator>() }
                };
                return new PaymentValidator(validators);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
