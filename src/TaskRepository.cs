using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB2.src
{
    public class TaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository()
        {
            _context = new TaskContext();
            _context.Database.EnsureCreated();
        }

        public IEnumerable<Task> GetAllTasks() => _context.Tasks.ToList();

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
