using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Pages.Student
{
    public class Student_informationModel : PageModel
    {
        private readonly StudentServices _studentService;

        public IList<Students> StudentList { get; set; } = default;

        [BindProperty]
        public Students NewStudent { get; set; } = default!;

        public Student_informationModel(StudentServices studentService)
        {
            _studentService = studentService;
        }
        public void OnGet()
        {
            StudentList = _studentService.GetStudent();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewStudent == null)
            {
                return Page();
            }

            _studentService.AddStudent(NewStudent);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _studentService.DeleteStudent(id);
            return RedirectToAction("Get");
        }
    }
}
