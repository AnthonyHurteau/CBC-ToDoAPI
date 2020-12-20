using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CBC_ToDoAPI.Application.ServiceLayer.Contracts;
using CBC_ToDoAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CBC_ToDoAPI.Interface.Controllers
{
    [Route("api/todotask")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService _toDoTaskService;

        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            _toDoTaskService = toDoTaskService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetToDoTask(int id)
        {
            var toDoTasks = await _toDoTaskService.GetAsync(id);

            if (toDoTasks == null)
            {
                return NotFound();
            }

            return Ok(toDoTasks);
        }

        [HttpGet]
        public async Task<ActionResult> GetToDoTasks()
        {
            var toDoTasks = await _toDoTaskService.GetAllAsync();

            if (toDoTasks == null)
            {
                return NoContent();
            }

            return Ok(toDoTasks);
        }

        [HttpPost]
        public async Task<ActionResult> PostToDoTask([FromBody] ToDoTask toDoTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!await _toDoTaskService.InsertAsync(toDoTask))
                {
                    ModelState.AddModelError("ToDoTask", _toDoTaskService.ErrorMessage);
                    return BadRequest(ModelState);
                }

                return Ok(toDoTask);
            }
            catch (DataException)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutToDoTask(int id, [FromBody]ToDoTask toDoTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!await _toDoTaskService.UpdateAsync(toDoTask))
                {
                    ModelState.AddModelError("ToDoTask", _toDoTaskService.ErrorMessage);
                    return BadRequest(ModelState);
                }

                var toDoTaskEntity = await _toDoTaskService.GetAsync(id);
                return Ok(toDoTaskEntity);
            }
            catch (DataException)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDoTask(int id)
        {
            try
            {
                if (!await _toDoTaskService.DeleteAsync(id))
                {
                    ModelState.AddModelError("ToDoTask", _toDoTaskService.ErrorMessage);
                    return BadRequest(ModelState);
                }
                return NoContent();
            }
            catch (DataException)
            {
                return BadRequest(ModelState);
            }
        }
    }
}