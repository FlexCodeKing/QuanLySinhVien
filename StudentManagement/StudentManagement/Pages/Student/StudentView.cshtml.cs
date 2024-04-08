using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using StudentManagement.DataContexts;
using StudentManagement.Models;
using StudentManagement.Pages.Logins;

namespace StudentManagement.Pages.Student
{
    public class StudentViewModel : PageModel
    {
        private readonly ILogger<StudentViewModel> _logger;
        private readonly StudentService _studentService;

        public StudentViewModel(ILogger<StudentViewModel> logger, StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            var userInfo = _studentService.GetUserByUsername(username);

            if (userInfo != null)
            {
                // Thực hiện xử lý với thông tin người dùng ở đây, ví dụ chuyển hướng đến trang StudentView
                return RedirectToPage("/Student/StudentView");
            }
            else
            {
                // Xử lý trường hợp không tìm thấy người dùng
                return Page();
            }
        }
    }

    public class StudentService
    {
        private readonly string _userFilePath = "~/../CSV_File/studentinfo.csv";

        public StudentService(string userFilePath)
        {
            _userFilePath = userFilePath;
        }

        public UserInfo GetUserByUsername(string username)
        {
            if (!File.Exists(_userFilePath))
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(_userFilePath))
            {
                reader.ReadLine(); // Bỏ qua dòng tiêu đề

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    if (values.Length >= 6 && values[4].Trim() == username)
                    {
                        return new UserInfo
                        {
                            Id = int.Parse(values[0]),
                            Name = values[1],
                            Email = values[2],
                            Address = values[3],
                            Username = values[4],
                            Password = values[5]
                        };
                    }
                }
            }

            return null;
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
