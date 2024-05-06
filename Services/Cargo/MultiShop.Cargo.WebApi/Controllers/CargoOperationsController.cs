using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BuesinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }

        [HttpGet]
        public IActionResult cargoOperationList()
        {
            var values = _cargoOperationService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetcargoOperationById(int id)
        {
            var value = _cargoOperationService.TGetById(id);
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CreatecargoOperation(CreateCargoOperationDto createcargoOperationDto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
               Barcode= createcargoOperationDto.Barcode,
               Description= createcargoOperationDto.Description,
               OperationDate=createcargoOperationDto.OperationDate
            };

            _cargoOperationService.TInsert(cargoOperation);
            return Ok("Kargo işlemi başarıyla eklendi");
        }

        [HttpPut]
        public IActionResult UpdatecargoOperation(UpdateCargoOperationDto updatecargoOperationDto)
        {
            CargoOperation updatedcargoOperation = new CargoOperation()
            {
                Barcode = updatecargoOperationDto.Barcode,
                CargoOperationId = updatecargoOperationDto.CargoOperationId,
                OperationDate= updatecargoOperationDto.OperationDate
            };
            _cargoOperationService.TUpdate(updatedcargoOperation);
            return Ok("Kargo işlemi Güncelleme işlemi başarılı");
        }
        [HttpDelete]
        public IActionResult RemovecargoOperation(int id)
        {
            _cargoOperationService.TDelete(id);
            return Ok("kargo işlemi başarıyla silindi");
        }
    }
}
