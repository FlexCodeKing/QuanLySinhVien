using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.DataContexts;

namespace StudentManagement.Pages.Manager_a
{
    public class EditManagerModel : PageModel
    {
        private readonly ManagerService _service;

        public EditManagerModel(ManagerService service)
        {
            _service = service;
        }

        [BindProperty]
        public Manager Managers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var manager = _service.Managers.FirstOrDefault(p => p.Id == itemid);
            if (manager == null)
            {
                return NotFound();
            }
            Managers = manager;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateManager(Managers.Id, Managers);
            return Redirect("~/../Manager_a/Manager'sInformation");
        }
    }
}
