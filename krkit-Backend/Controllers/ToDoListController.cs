using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace krkit_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> ToDoList = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Görev 1", Description = "Açıklama 1", IsCompleted = false },
            new ToDoItem { Id = 2, Title = "Görev 2", Description = "Açıklama 2", IsCompleted = true }
        };

        // Tüm görevleri getir (GET)
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(ToDoList);
        }

        // Belirli bir görevi getir (GET)
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = ToDoList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            return Ok(task);
        }

        // Yeni görev ekleme (POST)
        [HttpPost("add")]
        public IActionResult AddTask([FromBody] ToDoItem task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Geçerli bir başlık girin.");
            }

            task.Id = ToDoList.Count + 1;
            ToDoList.Add(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // Görev güncelleme (PUT)
        [HttpPut("update")]
        public IActionResult UpdateTask(int id, [FromBody] ToDoItem updatedTask)
        {
            var task = ToDoList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            return Ok(task);
        }

        // Görev silme (DELETE)
        [HttpDelete("delete")]
        public IActionResult DeleteTask(int id)
        {
            var task = ToDoList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Görev bulunamadı.");

            ToDoList.Remove(task);
            return NoContent();
        }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
