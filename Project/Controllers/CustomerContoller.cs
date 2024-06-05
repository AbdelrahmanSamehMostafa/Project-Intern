using AutoMapper;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace MyApp.Namespace
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerContoller : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IMapper _mapper;

        public CustomerContoller(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            var customerDTOs = customers.Select(customer => _mapper.Map<CustomerDTO>(customer)).ToList();
            return Ok(customerDTOs);
        }

        [HttpGet("{customerId}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDtoToReturn = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDtoToReturn);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerForCreationDTO customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            await _customerRepository.CreateCustomerAsync(customerEntity);

            var customerToReturn = _mapper.Map<CustomerDTO>(customer);
            return CreatedAtRoute("GetCustomerById", new { customerId = customerToReturn.CustomerId }, customerToReturn);
        }

        // [HttpPut("{customerId}")]
        // public async Task<IActionResult> UpdateCustomer(int customerId, CustomerDTO customerDTO)
        // {
        //     if (customerId != customerDTO.CustomerId)
        //     {
        //         return BadRequest();
        //     }
        //     var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
        //     var customerToUpdate = _mapper.Map(customerDTO, existingCustomer);
        //     await _customerRepository.UpdateCustomerAsync(customerToUpdate);
        //     return NoContent();
        // }


        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerDTO customerDTO)
        {
            if (customerId != customerDTO.CustomerId)
            {
                return BadRequest();
            }

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);

            // Map properties from customerDTO to existingCustomer
            _mapper.Map(customerDTO, existingCustomer);

            // Update the customer in the repository
            await _customerRepository.UpdateCustomerAsync(existingCustomer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
            return NoContent();
        }
    }
}
