using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Pages.Student
{
    public class EditStudentModel : PageModel
    {
        private readonly StudentServices _studentService;

        public EditStudentModel(StudentServices studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public Students Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if(itemid == null)
            {
                return NotFound();
            }
            var student = _studentService.Student.FirstOrDefault(s => s.StudentsID == itemid);
            if (student == null)
            {
                return NotFound();
            }
            Student = student;
            return Page();
        }

        public async Task<IActionResult>OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _studentService.UpdateStudent(Student.StudentsID, Student);
            return Redirect("~/../Student/Studen_information");
        }
    }
}
