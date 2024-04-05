using StudentManagement.DataContexts;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentServices
    {
        private readonly StudentContexts _studentContext = default;
        public IList<Students>Student { get; set; }

       public IList<Students>GetStudent()
        {
            if(_studentContext != null)
            {
                return _studentContext.Student.ToList();
            }
            return new List<Students>();
        }

        public void AddStudent(Students student)
        {
            if(_studentContext != null)
            {
                _studentContext.InsertStudent(student);
            }
        }

        public void UpdateStuden(int id, Students student)
        {
            if(_studentContext != null)
            {
                _studentContext.UpdateStudent(id, student);
            }
        }

        public void DeleteStudent(int id)
        {
            if (_studentContext != null)
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
