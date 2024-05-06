using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BuesinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        [HttpGet]
        public IActionResult CargoCustomerList()
        {
            var values = _cargoCustomerService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var value = _cargoCustomerService.TGetById(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDto cargoCustomerDto)
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                Address = cargoCustomerDto.Address,
                City = cargoCustomerDto.City,
                Disctrict = cargoCustomerDto.Disctrict,
                Email = cargoCustomerDto.Email,
                Name = cargoCustomerDto.Name,
                Surname = cargoCustomerDto.Surname,
                Phone = cargoCustomerDto.Phone,
            };
            _cargoCustomerService.TInsert(cargoCustomer);
            return Ok("cargo müşterisi Başarıyla Eklendi");
        }

        [HttpDelete]
        public IActionResult RemoveCargoCustomer(int id)
        {
            _cargoCustomerService.TDelete(id);
            return Ok("Kargo müşterisi başarıyla silindi");
        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            CargoCustomer updatedCargoCustomer = new CargoCustomer()
            { 
                CargoCustomerId = updateCargoCustomerDto.CargoCustomerId,
                Address = updateCargoCustomerDto.Address, 
                City = updateCargoCustomerDto.City, 
                Disctrict=updateCargoCustomerDto.Disctrict,
                Email = updateCargoCustomerDto.Email,
                Name = updateCargoCustomerDto.Name,
                Phone = updateCargoCustomerDto.Phone,
                Surname=updateCargoCustomerDto.Surname
            };
            _cargoCustomerService.TUpdate(updatedCargoCustomer);
            return Ok("Cargo Müşterisi başarıyla güncellendi");
        }
    }
}
