    using BankBlazor.Api.Data;
using BankBlazor.Shared.DTOs;
using BankBlazor.Api.Enums;
    using BankBlazor.Api.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;


namespace BankBlazor.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankBlazorContext _dbContext;
        public CustomerService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync(); //Fråga: Ska jag ha med Depositions? och andra relaterade tabeller?
                                                                      // isåfall sla lägga till .Include(c => c.Dispositions)
            return customers;
        }
        public async Task<CustomerReadDTO> GetCustomerById(int id)
        {
            var customer = await _dbContext.Customers 
                //.Include(c => c.Dispositions) // Ska jag ha med Depositions?
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Kund med id {id} hittades inte.");
            }
            return new CustomerReadDTO {
                City = customer.City ?? string.Empty,
                Country = customer.Country ?? string.Empty,
                CustomerId = customer.CustomerId,
                Emailaddress = customer.Emailaddress ?? string.Empty,
                Givenname = customer.Givenname ?? string.Empty,
                Streetaddress = customer.Streetaddress ?? string.Empty,
                Surname = customer.Surname ?? string.Empty,
                Telephonenumber = customer.Telephonenumber,
                Zipcode = customer.Zipcode ?? string.Empty
            };
        }
       
       
        
    }
}
