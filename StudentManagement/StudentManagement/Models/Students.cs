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

        [RegularExpression(@"^((\+84|84|0|0084){1})(3|5|7|8|9)+([0-9]{8})$", ErrorMessage = "Invalid Phone Number")]
        public string? StudentsPhone { get; set; }

        [RegularExpression(@"^[\w\.-]+@gmail\.com$", ErrorMessage = "Email must have @gmail.com")]
        public string? StudentsEmail { get; set; }
        public string? StudentsAddress { get; set; }

    }
}
