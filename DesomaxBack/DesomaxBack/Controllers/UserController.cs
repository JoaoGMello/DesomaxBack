using DesomaxBack.Context;
using DesomaxBack.Models;
using DesomaxBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace DesomaxBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DesomaxContext _context;

        public UserController(DesomaxContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("InsertUser")]
        public async Task<ActionResult<List<User>>> InsertUser(InsertUserViewModel insertUserViewModel)
        {
            try
            {
                _context.Users.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = insertUserViewModel.UserName ?? "",
                    FirstName = insertUserViewModel.FirstName ?? "",
                    LastName = insertUserViewModel.LastName ?? "",
                    Email = insertUserViewModel.Email ?? "",
                    CPF = insertUserViewModel.CPF ?? "",
                    City = insertUserViewModel.City ?? "",
                    State = insertUserViewModel.State ?? "",
                    Address = insertUserViewModel.Address ?? "",
                    Gender = insertUserViewModel.Gender,
                    Password = insertUserViewModel.Password ?? "",
                    Phone = insertUserViewModel.Phone ?? "",
                    Excluded = false,
                    InclusionDate = DateTime.Now,
                    ChangeDate = DateTime.Now,
                });

                _context.SaveChanges();

                return Ok("Usuário inserido com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetUserByNameAndPassword")]
        public async Task<ActionResult<User>> GetUserByNameAndPassword(LoginViewModel loginViewModel)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginViewModel.UserName && x.Password == loginViewModel.Password);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(UserIdViewModel userIdViewModel)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id.ToString() == userIdViewModel.UserId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<List<User>>> UpdateUser(UserDetailsViewModel userDetailsViewModel)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id.ToString() == userDetailsViewModel.Id);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                user.FirstName = userDetailsViewModel.FirstName ?? "";
                user.LastName = userDetailsViewModel.LastName ?? "";
                user.Email = userDetailsViewModel.Email ?? "";
                user.CPF = userDetailsViewModel.CPF ?? "";
                user.City = userDetailsViewModel.City ?? "";
                user.State = userDetailsViewModel.State ?? "";
                user.Address = userDetailsViewModel.Address ?? "";
                user.Gender = userDetailsViewModel.Gender;
                user.Password = userDetailsViewModel.Password ?? "";
                user.Phone = userDetailsViewModel.Phone ?? "";
                user.InclusionDate = DateTime.Now;

                _context.SaveChanges();

                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<User>> DeleteUser(string userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id.ToString() == userId);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("Usuário removido com sucesso!");
        }
    }
}
