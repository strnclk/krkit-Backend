using krkit_Backend.Data;
using krkit_Backend.Data.UnitOfWork;
using krkit_Backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using krkit_Backend.Data.DTOs.UserDTOs;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        // Bağımlılıkları constructor üzerinden alıyoruz.
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Tüm görevleri getir (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if (users == null || !users.Any())
            {
                return Ok(users);
            }

            return Ok(users);
        }


        // Görev silme (DELETE)
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleteuser(DeleteUserDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(dto.Id);
            if (user == null)
                return NotFound("Görev bulunamadı.");

            await _unitOfWork.Users.DeleteAsync(dto.Id);
            return Ok(dto);
        }
    }
}
