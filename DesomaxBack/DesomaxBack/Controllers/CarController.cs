﻿using Azure.Core;
using DesomaxBack.Common;
using DesomaxBack.Context;
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

        [HttpGet]
        [Route("GetCarById")]
        public async Task<ActionResult<Car>> GetCarById(string cardId)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id.ToString() == cardId);

            if (car == null)
            {
                return NotFound("Carro não encontrado");
            }

            return Ok(car);
        }

        [HttpPut]
        [Route("UpdateCar")]
        public async Task<ActionResult<List<Car>>> UpdateCar(CarDetailsViewModel carDetailsViewModel)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(x => x.Id.ToString() == carDetailsViewModel.Id);

                if (car == null)
                {
                    return NotFound("Carro não encontrado");
                }

                car.Name = carDetailsViewModel.Name;
                car.Description = carDetailsViewModel.Description;
                car.ChangeDate = DateTime.Now;

                _context.SaveChanges();

                return Ok("Carro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCar")]
        public async Task<ActionResult<Car>> DeleteCar(string cardId)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id.ToString() == cardId);

            if (car == null)
            {
                return NotFound("Carro não encontrado");
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return Ok("Carro removido com sucesso!");
        }
    }
}
