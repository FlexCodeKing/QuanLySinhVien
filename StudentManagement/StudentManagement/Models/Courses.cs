using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class modelCourses
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? CourseName { get; set; }
    }
}
