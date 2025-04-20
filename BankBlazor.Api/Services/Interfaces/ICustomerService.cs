using BankBlazor.Api.Data;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id); // frågetecken för nullable
        Task<ResponseCode> CreateCustomer(Customer customer);
        Task<ResponseCode> UpdateCustomer(Customer customer);
        Task<ResponseCode> DeleteCustomer(int id);
    }
}
