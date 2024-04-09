/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace StudentManagement.Pages.Logins
{
    public class LoginModel : PageModel

    {

        public IActionResult OnPost(string username, string password, string role)
        {

            if (role == "manager")
            {
                string managerFilePath = "~/../CSV_File/Manager.csv";

                if (System.IO.File.Exists(managerFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(managerFilePath);
                    foreach (var line in lines.Skip(1))
                    {
                        var fields = line.Split(",");
                        if (fields.Length >= 5)
                        {
                            string storedUsername = fields[4].Trim();
                            string storedPassword = fields[5].Trim();


                            if (storedUsername == username && storedPassword == password)
                            {
                                // return RedirectToPage("Manager_a/Manager'sInformation");
                                return Redirect("~/../Manager_a/Manager'sInformation");
                            }
                        }
                    }
                }
               // HttpContext.Session.SetString("Username", username);
            }
            else if (role == "student")
            {
                string studentFilePath = "~/../CSV_File/studentinfo.csv";

                if (System.IO.File.Exists(studentFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(studentFilePath);
                    foreach (var line in lines.Skip(1))
                    {
                        var fields = line.Split(",");
                        if (fields.Length >= 6)
                        {
                            string storedUsername = fields[5].Trim();
                            string storedPassword = fields[6].Trim();

                            if (storedUsername == username && storedPassword == password)
                            {
                                return Redirect("~/../Student/StudentView");
                            }
                        }
                    }
                }
              //  HttpContext.Session.SetString("Username", username);
            }


           
            return Page();
        }



    }
}
*/




using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;

namespace StudentManagement.Pages.Logins
{
    public class LoginModel : PageModel
    {
        public IActionResult OnPost(string username, string password, string role)
        {
            if (role == "manager")
            {
                string managerFilePath = Path.Combine(Directory.GetCurrentDirectory(), "CSV_File", "Manager.csv");
                if (System.IO.File.Exists(managerFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(managerFilePath);
                    foreach (var line in lines.Skip(1))
                    {
                        var fields = line.Split(",");
                        if (fields.Length >= 5)
                        {
                            string storedUsername = fields[4].Trim();
                            string storedPassword = fields[5].Trim();
                            if (storedUsername == username && storedPassword == password)
                            {
                                return Redirect("/Manager_a/Manager'sInformation");
                            }
                        }
                    }
                }
            }
            else if (role == "student")
            {
                string studentFilePath = Path.Combine(Directory.GetCurrentDirectory(), "CSV_File", "studentinfo.csv");
                if (System.IO.File.Exists(studentFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(studentFilePath);
                    foreach (var line in lines.Skip(1))
                    {
                        var fields = line.Split(",");
                        if (fields.Length >= 6)
                        {
                            string storedUsername = fields[5].Trim();
                            string storedPassword = fields[6].Trim();
                            if (storedUsername == username && storedPassword == password)
                            {
                                return Redirect("/Student/StudentView");
                            }
                        }
                    }
                }
            }
            // Chuyển hướng về trang hiện tại nếu thông tin đăng nhập không chính xác
            return Page();
        }
    }
}
