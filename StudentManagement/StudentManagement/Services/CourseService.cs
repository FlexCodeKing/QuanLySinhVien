using StudentManagement.DataContexts;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class CoursesService
    {
        //private readonly PizzaContext _context = default!;
        private readonly CoursesContext _context = default!;
        public IList<modelCourses> ACourses { get; set; }
        public CoursesService(CoursesContext context)
        {
            _context = context;
            ACourses = GetCourses();
        }

        public IList<modelCourses> GetCourses()
        {
            if (_context.Course != null)
            {
                return _context.Course.ToList();
            }
            return new List<modelCourses>();
        }

        public void AddCourses(modelCourses courses)
        {
            if (_context.Course != null)
            {
                _context.InsertCourse(courses);

            }
        }
        public void UpdateCourses(int id, modelCourses courses)
        {
            if (_context.Course != null)
            {
                _context.UpdateCourse(id, courses);
            }
        }

        public void Deletecourses(int id)
        {
            if (_context.Course != null)
            {
                var courses = _context.Course.Find(p => p.ID == id);
                if (courses != null)
                {
                    _context.DeleteCourse(id);

                }
            }
        }
    }
}
