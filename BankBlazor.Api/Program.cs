
using BankBlazor.Api.Contexts;
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

            //---> Registrerar BankBlazorContext i Program.cs, och talar om för applikationen att när någon
            //behöver en instans av BankBlazorContext (t.ex. en controller), ska den få en instans av
            //den som är korrekt konfigurerad med SQL Server-anslutning och connection string.
            builder.Services.AddDbContext<BankBlazorContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
