    using BankBlazor.Api.Data;
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
        public async Task<Customer?> GetCustomerById(int id)
        {
            var customer = await _dbContext.Customers 
                .Include(c => c.Dispositions) // Ska jag ha med Depositions?
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
                throw new Exception("Customer not found");
            return customer;
        }
        public async Task<ResponseCode> CreateCustomer(Customer customer)
        {
            try
            {
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
                return ResponseCode.Created;
            }
            catch (DbUpdateException)
            {
                return ResponseCode.BadRequest;
            }
        }
        public async Task<ResponseCode> UpdateCustomer(Customer customer)
        {
            try
            {
                _dbContext.Customers.Update(customer);
                await _dbContext.SaveChangesAsync();
                return ResponseCode.Accepted;
            }
            catch (DbUpdateConcurrencyException)
            {
                return ResponseCode.NotFound;
            }
            catch (DbUpdateException)
            {
                return ResponseCode.BadRequest;
            }
        }
        public async Task<ResponseCode> DeleteCustomer(int id)
        {
            try
            {
                var customer = await GetCustomerById(id);
                if (customer == null) return ResponseCode.NotFound;
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return ResponseCode.Success;
            }
            catch (DbUpdateException)
            {
                return ResponseCode.BadRequest;
            }
        }
    }
}
