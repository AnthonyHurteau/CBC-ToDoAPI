using CBC_ToDoAPI.Application.ServiceLayer.Contracts;
using CBC_ToDoAPI.Domain.Context;
using CBC_ToDoAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBC_ToDoAPI.Application.ServiceLayer.Services
{
    public class ToDoTaskService : IToDoTaskService, IDisposable
    {
        private readonly CBCToDoContext _context;
        private readonly DbSet<ToDoTask> _toDoTask;

        public string ErrorMessage { get; set; }

        public ToDoTaskService(CBCToDoContext context)
        {
            _context = context;
            _toDoTask = context.Set<ToDoTask>();
        }

        public async Task<ToDoTask> GetAsync(int id)
        {
            return await _toDoTask.AsNoTracking().FirstOrDefaultAsync((t) => t.Id == id);
        }

        public async Task<IEnumerable<ToDoTask>> GetAllAsync()
        {
            return await _toDoTask.AsNoTracking().ToListAsync();
        }

        public async Task<bool> InsertAsync(ToDoTask toDoTask)
        {
            try
            {
                await _toDoTask.AddAsync(toDoTask);
            }
            catch (Exception exception)
            {
                SetExceptionMessage(exception);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(ToDoTask toDoTask)
        {
            if (await GetAsync(toDoTask.Id) == null)
            {
                ErrorMessage = $"ToDo Task id: {toDoTask.Id} does not exist";
                return false;
            }

            try
            {
                _toDoTask.Update(toDoTask);
            }
            catch (Exception exception)
            {
                SetExceptionMessage(exception);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var toDoTask = await GetAsync(id);

            if (toDoTask == null)
            {
                ErrorMessage = $"ToDo Task id: {toDoTask.Id} does not exist";
                return false;
            }

            try
            {
                _toDoTask.Remove(toDoTask);
            }
            catch (Exception exception)
            {
                SetExceptionMessage(exception);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        private void SetExceptionMessage(Exception exception)
        {
            ErrorMessage = $"An error has occured: {exception.Message}";
        }

        //Dispose Functions

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
