using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Pages.Courresess
{
    public class EditModel : PageModel
    {
        private readonly CoursesService _service;

        public EditModel(CoursesService service)
        {
            _service = service;
        }

        [BindProperty]
        public modelCourses ACourse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var course = _service.ACourses.FirstOrDefault(p => p.ID == itemid);
            if (course == null)
            {
                return NotFound();
            }
            ACourse = course;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateCourses(ACourse.ID, ACourse);
            return Redirect("~/../Courses/Index");
        }
    }
}
