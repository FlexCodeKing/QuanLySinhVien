using StudentManagement.DataContexts;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class CoursesService
    {
        //private readonly PizzaContext _context = default!;
        private readonly CoursesContext _context = default!;
        public IList<Courses> Courses { get; set; }
        public CoursesService(CoursesContext context)
        {
            _context = context;
            Courses = GetCourses();
        }

        public IList<Courses> GetCourses()
        {
            if (_context.Courses != null)
            {
                return _context.Courses.ToList();
            }
            return new List<Courses>();
        }

        public void AddCourses(Courses courses)
        {
            if (_context.Courses != null)
            {
                _context.InsertCourse(courses);

            }
        }
        public void Updateourses(int id, Courses courses)
        {
            if (_context.Courses != null)
            {
                _context.UpdateCourse(id, courses);
            }
        }

        public void Deletecoursesa(int id)
        {
            if (_context.Courses != null)
            {
                var courses = _context.Courses.Find(p => p.ID == id);
                if (courses != null)
                {
                    _context.DeleteCourse(id);

                }
            }
        }
    }
}
