using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace StudentManagement.Models
{
    public class Students
    {
        [Key]
        public int StudentsID {get; set;}

        [Required]
        public string? StudentsName {  get; set; }
        public string? StudentsPhone { get; set; }
        public string? StudentsEmail { get; set; }
        public string? StudentsAddress { get; set; }

    }
}
