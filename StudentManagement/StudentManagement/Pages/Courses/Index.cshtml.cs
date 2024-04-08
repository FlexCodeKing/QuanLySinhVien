using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CoursesService _service;
        public IList<modelCourses> CourseList { get; set; } = default!;

        [BindProperty]
        public modelCourses NewCourse { get; set; } = default!;

        public IndexModel(CoursesService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            CourseList = _service.GetCourses();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewCourse == null)
            {
                return Page();
            }

            _service.AddCourses(NewCourse);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.Deletecourses(id);
            return RedirectToAction("Get");
        }
    }
}

