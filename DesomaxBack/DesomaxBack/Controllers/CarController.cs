using Azure.Core;
using DesomaxBack.Common;
using DesomaxBack.Context;
using DesomaxBack.Interfaces;
using DesomaxBack.Models;
using DesomaxBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesomaxBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly DesomaxContext _context;

        public CarController(DesomaxContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("InsertCar")]
        public async Task<ActionResult<List<Car>>> InsertCar(InsertCarViewModel insertCarViewModel)
        {
            try
            {
                _context.Cars.Add(new Car()
                {
                    Id = Guid.NewGuid(),
                    Name = insertCarViewModel.Name ?? "",
                    Description = insertCarViewModel.Description ?? "",
                    Excluded = false,
                    InclusionDate = DateTime.Now,
                    ChangeDate = DateTime.Now,
                });

                _context.SaveChanges();

                return Ok("Carro inserido com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllCars")]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            try
            { 
                var cars = await _context.Cars.ToListAsync();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
