using StudentManagement.DataContexts;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentServices
    {
        private readonly StudentContexts _studentContext = default!;
        public IList<Students> Student { get; set; }

        public StudentServices(StudentContexts studentContext)
        {
            _studentContext = studentContext;
            Student = GetStudent();
        }

       public IList<Students>GetStudent()
        {
            if(_studentContext.Student != null)
            {
                return _studentContext.Student.ToList();
            }
            return new List<Students>();
        }

        public void AddStudent(Students student)
        {
            if(_studentContext.Student != null)
            {
                _studentContext.InsertStudent(student);
            }
        }

        public void UpdateStudent(int id, Students student)
        {
            if(_studentContext.Student != null)
            {
                _studentContext.UpdateStudent(id, student);
            }
        }

        public void DeleteStudent(int id)
        {
            if (_studentContext.Student != null)
            {
                var student = _studentContext.Student.Find(s => s.StudentsID == id);
                if (student != null)
                {
                    _studentContext.DeleteStudent(id);
                }
            }
        }
    }
}
