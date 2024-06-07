// using AutoMapper;
// using HotelBookingSystem.Data.Models;
// using HotelBookingSystem.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace HotelBookingSystem.Controllers
// {
//     [Route("api/Addresses")]
//     [ApiController]
//     public class AddressController : ControllerBase
//     {
//         private readonly IAddressRepository _addressRepository;
//         private readonly IMapper _mapper;
        
        
//         public AddressController(IAddressRepository addressRepository, IMapper mapper)
//         {
//             _addressRepository = addressRepository;
//             _mapper = mapper;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAllAddresses()
//         {
//             var addresses = await _addressRepository.GetAllAddressesAsync();
//             // if (addresses == null)
//             // {
//             //     return NotFound();
//             // }
//             return Ok(_mapper.Map<IEnumerable<AddressWithHotelNameDTO>>(addresses));
//         }

//         [HttpGet]
//         [Route("{addressId}", Name = "GetAddressById")]
//         public async Task<IActionResult> GetAddressById(int addressId)
//         {
//             var address = await _addressRepository.GetAddressByIdAsync(addressId);
//             if (address == null)
//             {
//                 return NotFound();
//             }
//             return Ok(_mapper.Map<AddressWithHotelNameDTO>(address));
//         }

//         [HttpPost]
//         [Route("{HotelId}")]
//         public async Task<IActionResult> CreateAddress(int HotelId, [FromBody] AddressForCreationDTO addressdto)
//         {


//             var addressEntity = _mapper.Map<Address>(addressdto);
//             //addressEntity.HotelId = HotelId;

//             await _addressRepository.CreateAddressAsync(addressEntity);

//             var addressToReturn = _mapper.Map<AddressWithIdDTO>(addressEntity);
//             return CreatedAtRoute("GetAddressById", new { addressId = addressToReturn.Id }, addressToReturn);
//         }

//         [HttpPut]
//         [Route("{addressId}")]
//         public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressWithIdDTO addressWithId)
//         {
//             var address = await _addressRepository.GetAddressByIdAsync(addressId);
//             if (address == null)
//             {
//                 return NotFound();
//             }

//             if(addressId != addressWithId.Id)
//                 return BadRequest();

//             _mapper.Map(addressWithId, address);

//             await _addressRepository.UpdateAddressAsync(address);

//             return NoContent();
//         }

//         [HttpDelete]
//         [Route("{addressId}")]
//         public async Task<IActionResult> DeleteAddress(int addressId)
//         {
//             if (!await _addressRepository.AddressExistsAsync(addressId))
//             {
//                 return NotFound();
//             }

//             await _addressRepository.DeleteAddressAsync(addressId);
//             return NoContent();
//         }


//     }

// }