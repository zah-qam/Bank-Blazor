using BankBlazor.Api.Data;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id); // frågetecken för nullable
    }
}
