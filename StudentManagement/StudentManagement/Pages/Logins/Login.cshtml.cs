using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                                return Redirect("~/../Student/Studen_information");
                            }
                        }
                    }
                }
            }


            /*string filePath = ""; // Đường dẫn tới tệp CSV

            if (role == "manager")
            {
                filePath = "~/../CSV_File/Manager.csv";
            }
            else if (role == "student")
            {
                filePath = "..../CSV_File/studentinfo.csv";
            }

            // Kiểm tra xem tệp có tồn tại không
            if (System.IO.File.Exists(filePath))
            {
                // Đọc tất cả các dòng từ tệp CSV
                var lines = System.IO.File.ReadAllLines(filePath);

                // In ra hoặc ghi log mảng dòng
                foreach (var line in lines)
                {
                    Console.WriteLine(line); // In ra mỗi dòng trong console
                                             // Ghi log: sử dụng thư viện log của ASP.NET Core để ghi log dòng này
                }

                // Tiếp tục xử lý thông tin đăng nhập và chuyển hướng
            }
            else
            {
                Console.WriteLine("File not found: " + filePath); // In ra thông báo nếu tệp không tồn tại
                                                                  // Ghi log: Sử dụng thư viện log của ASP.NET Core để ghi log thông báo này
            }
            // Chuyển hướng tới trang báo lỗi nếu thông tin đăng nhập không chính xác*/
            return Page();
        }



    }
}
