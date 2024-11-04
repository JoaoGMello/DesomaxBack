using Azure.Core;
using DesomaxBack.Common;
using DesomaxBack.Context;
using DesomaxBack.Models;
using DesomaxBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

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
                    Image = insertCarViewModel.Image ?? "",
                    Price = insertCarViewModel.Price,
                    Brand = insertCarViewModel.Brand ?? "",
                    Model = insertCarViewModel.Model ?? "",
                    Year = insertCarViewModel.Year ?? "",
                    Color = insertCarViewModel.Color ?? "",
                    Km = insertCarViewModel.Km,
                    UserId = Guid.Parse(insertCarViewModel.UserId),
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
        public async Task<ActionResult<List<CarDetailsViewModel>>> GetAllCars()
        {
            try
            {
                var cars = (from c in _context.Cars.Where(x => x.Excluded == false)
                               join u in _context.Users on c.UserId equals u.Id

                               select new CarDetailsViewModel
                               {
                                   Id = c.Id.ToString(),
                                   Model = c.Model ?? "",
                                   Brand = c.Brand ?? "",
                                   Year = c.Year ?? "",
                                   Price = c.Price,
                                   Description= c.Description ?? "",
                                   Image = c.Image ?? "",
                                   Km = c.Km,
                                   City = u.City ?? "",
                                   State = u.State ?? "",
                                   UserId = c.UserId.ToString() ?? "",
                                   Like =  c.Like,
                               }).AsEnumerable();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetCarsByUserId")]
        public async Task<ActionResult<List<CarDetailsViewModel>>> GetCarsByUserId(UserIdViewModel userIdViewModel)
        {
            try
            {
                var cars = (from c in _context.Cars.Where(x => x.Excluded == false && x.UserId.ToString() == userIdViewModel.UserId)
                            join u in _context.Users on c.UserId equals u.Id

                            select new CarDetailsViewModel
                            {
                                Id = c.Id.ToString(),
                                Model = c.Model ?? "",
                                Brand = c.Brand ?? "",
                                Year = c.Year ?? "",
                                Price = c.Price,
                                Description = c.Description ?? "",
                                Image = c.Image ?? "",
                                Km = c.Km,
                                City = u.City ?? "",
                                State = u.State ?? "",
                                UserId = c.UserId.ToString() ?? "",
                                Like = c.Like,
                            }).AsEnumerable();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetCarById")]
        public async Task<ActionResult<CarDetailsViewModel>> GetCarById(CarIdViewModel carIdViewModel)
        {
            var carDetails = (from c in _context.Cars.Where(x => x.Id.ToString() == carIdViewModel.CarId)
                              join u in _context.Users on c.UserId equals u.Id

                              select new CarDetailsViewModel
                              {
                                  Id = c.Id.ToString(),
                                  Model = c.Model ?? "",
                                  Brand = c.Brand ?? "",
                                  Year = c.Year ?? "",
                                  Price = c.Price,
                                  Description = c.Description ?? "",
                                  Image = c.Image ?? "",
                                  Km = c.Km,
                                  City = u.City ?? "",
                                  State = u.State ?? "",
                                  UserId = c.UserId.ToString() ?? "",
                                  Seller = $"{u.FirstName} {u.LastName}",
                                  EmailSeller = u.Email ?? "",
                                  Color = c.Color ?? "",
                                  Like = c.Like,
                              });

            if (carDetails == null)
            {
                return NotFound("Carro não encontrado");
            }

            return Ok(carDetails);
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
                car.Image = carDetailsViewModel.Image ?? "";
                car.Price = carDetailsViewModel.Price;
                car.Brand = carDetailsViewModel.Brand ?? "";
                car.Model = carDetailsViewModel.Model ?? "";
                car.Year = carDetailsViewModel.Year ?? "";
                car.Color = carDetailsViewModel.Color ?? "";
                car.Km = carDetailsViewModel.Km;
                car.ChangeDate = DateTime.Now;

                _context.SaveChanges();

                return Ok("Carro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("LikeCar")]
        public async Task<ActionResult<List<Car>>> LikeCar(LikeCarViewModel likeCarViewModel)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(x => x.Id.ToString() == likeCarViewModel.CarId);

                if (car == null)
                {
                    return NotFound("Carro não encontrado");
                }

                car.Like = likeCarViewModel.Like;
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
