using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService; // Ska skapa en Mapp (Interfaces) Och sätta in ICustomerService där
        public CustomerController(ICustomerService customerService) // Jag skapar en konstruktor som tar emot ICustomerService
        {
            _customerService = customerService;
        }
        // GET: api/CustomerController
        [HttpGet] // Detta är en GET metod som hämtar alla kunder genom att anropa GetAll metoden i ICustomerService
        public async Task<ActionResult<List<CustomerDTO>>> GetAll() // Ska skapa CustomerDto i en mapp (DTOs)
                                                                    // Metoden returnerar en lista av CustomerDto i Json format.
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(int id) // Detta är en GET metod som hämtar en kund 
                                                                     // Den tar emot en parameter id av typ int 
                                                                     // och hämtar en specifik kund.
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

    }
}
