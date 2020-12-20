using CBC_ToDoAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBC_ToDoAPI.Application.ServiceLayer.Contracts
{
  public  interface IToDoTaskService
    {
        string ErrorMessage { get; set; }
        Task<ToDoTask> GetAsync(int id);
        Task<IEnumerable<ToDoTask>> GetAllAsync();
        Task<bool> InsertAsync(ToDoTask toDoTask);
        Task<bool> UpdateAsync(ToDoTask toDoTask);
        Task<bool> DeleteAsync(int id);
    }
}
