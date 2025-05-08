using BankBlazor.Api.Data;
using BankBlazor.Api.Services.Interfaces;
using BankBlazor.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //---> Registrerar BankBlazorContext i Program.cs, och talar om f�r applikationen att n�r n�gon
            //beh�ver en instans av BankBlazorContext (t.ex. en controller), ska den f� en instans av
            //den som �r korrekt konfigurerad med SQL Server-anslutning och connection string.
            builder.Services.AddDbContext<BankBlazorContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

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
