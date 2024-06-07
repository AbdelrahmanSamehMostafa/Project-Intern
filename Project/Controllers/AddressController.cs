using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _addressRepository.GetAllAddressesAsync();
            return Ok(_mapper.Map<IEnumerable<AddressBaseDTO>>(addresses));
        }

        [HttpGet("{addressId}", Name = "GetAddressById")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AddressBaseDTO>(address));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(AddressBaseDTO addressdto)
        {
            var addressEntity = _mapper.Map<Address>(addressdto);
            await _addressRepository.CreateAddressAsync(addressEntity);

            return StatusCode(201);
        }


        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, AddressBaseDTO addressBaseDTO)
        {
            var address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                return NotFound();
            }
            _mapper.Map(addressBaseDTO, address);
            await _addressRepository.UpdateAddressAsync(address);
            return NoContent();
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            if (!await _addressRepository.AddressExistsAsync(addressId))
            {
                return NotFound();
            }

            await _addressRepository.DeleteAddressAsync(addressId);
            return NoContent();
        }
    }
}
