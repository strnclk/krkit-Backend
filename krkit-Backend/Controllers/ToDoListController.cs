using krkit_Backend.Data;
using krkit_Backend.Data.UnitOfWork;
using krkit_Backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using krkit_Backend.Data.DTOs.ToDoItemDTOs;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ToDoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        // Bağımlılıkları constructor üzerinden alıyoruz.
        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Tüm görevleri getir (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _unitOfWork.ToDoLists.GetAllAsync();
            if (tasks == null || !tasks.Any())
            {
                return Ok(tasks);
            }

            return Ok(tasks);
        }

        // Belirli bir görevi getir (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _unitOfWork.ToDoLists.GetByIdAsync(id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            return Ok(task);
        }

        // Yeni görev ekleme (POST)
        [HttpPost("add")]
        public async Task<IActionResult> AddTask([FromBody] AddToDoItemDto task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Geçerli bir başlık girin.");
            }

            // Yeni görev ekliyoruz
            var newTask = new ToDoList
            {
                Title = task.Title,
                Description = task.Description,
                CreatedAt = DateTime.Now,
                IsCompleted=false,
            };

            await _unitOfWork.ToDoLists.AddAsync(newTask);
            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
        }

        // Görev güncelleme (PUT)
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask(UpdateToDoItemDto updatedTask)
        {
            var task = await _unitOfWork.ToDoLists.GetByIdAsync(updatedTask.Id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            await _unitOfWork.ToDoLists.UpdateAsync(task);
            return Ok(task);
        }

        // Görev silme (DELETE)
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTask(DeleteToDoItemDto dto)
        {
            var task = await _unitOfWork.ToDoLists.GetByIdAsync(dto.Id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            await _unitOfWork.ToDoLists.DeleteAsync(dto.Id);
            return Ok(dto);
        }
    }
}
