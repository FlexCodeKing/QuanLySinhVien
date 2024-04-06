using Microsoft.AspNetCore.Mvc;
using StudentManagement.DataContexts;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class ManagerService
    {
        //private readonly PizzaContext _context = default!;
        private readonly ManagerContext _context = default!;
        public IList<Manager> Managers { get; set; }
        public ManagerService(ManagerContext context)
        {
            _context = context;
            Managers = GetManagers();
        }

        public IList<Manager> GetManagers()
        {
            if (_context.Managers != null)
            {
                return _context.Managers.ToList();
            }
            return new List<Manager>();
        }
        public void AddManager(Manager manager)
        {
            if (_context.Managers != null)
            {
                _context.InsertManager(manager);

            }
        }
        public void UpdateManager(int id, Manager manager)
        {
            if (_context.Managers != null)
            {
                _context.UpdateManager(id, manager);
            }
        }

        public void DeleteManager(int id)
        {
            if (_context.Managers != null)
            {
                var manager = _context.Managers.Find(p => p.Id == id);
                if (manager != null)
                {
                    _context.DeleteManager(id);

                }
            }
        }
    }
}
