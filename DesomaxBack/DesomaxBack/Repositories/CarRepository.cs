using DesomaxBack.Common;
using DesomaxBack.Context;
using DesomaxBack.Interfaces;
using DesomaxBack.Models;
using DesomaxBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DesomaxBack.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DesomaxContext _context;

        public CarRepository(DesomaxContext context)
        {
            _context = context;
        }

        public async Task<JsonReturn> InsertCar(InsertCarViewModel request)
        {
            try
            {
                _context.Cars.Add(new Car()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name ?? "",
                    Description = request.Description ?? "",
                    Excluded = false,
                    InclusionDate = DateTime.Now,
                    ChangeDate = DateTime.Now,
                });

                _context.SaveChanges();

                return new JsonReturn()
                {
                    Success = _context.SaveChanges() > 0,
                };
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
