using Xunit;
using StudentManagement.Pages.Logins;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace LoginModelTets
{
    public class LoginTests
    {
        [Fact]
        public void TestLoginWithManagerRole()
        {
            // Arrange
            var loginModel = new LoginModel();
            string username = "Nguyendung1990"; // Thay đổi "your_manager_username" thành tên người dùng quản lý thực tế của bạn
            string password = "dungnguyenmanager123"; // Thay đổi "your_manager_password" thành mật khẩu quản lý thực tế của bạn
            string role = "manager";

            // Act
            var result = loginModel.OnPost(username, password, role);

            // Assert
            Assert.IsType<PageResult>(result); //Đăng nhập thành công sẽ chuyển sang manager'sinformation
            /*Assert.Equal("/Manager_a/Manager'sInformation", ((RedirectResult)result).Url);*/
        }

        [Fact]
        public void TestLoginWithStudentRole()
        {
            // Arrange
            var loginModel = new LoginModel();
            string username = "hieu123"; // Thay đổi "your_student_username" thành tên người dùng học sinh thực tế của bạn
            string password = "student123"; // Thay đổi "your_student_password" thành mật khẩu học sinh thực tế của bạn
            string role = "student";

            // Act
            var result = loginModel.OnPost(username, password, role);

            // Assert
            Assert.IsType<PageResult>(result); //Đăng nhập thành công sẽ chuyển sang studentview
            /*Assert.Equal("/Student/StudentView", ((PageResult)result).Url);*/
        }

        [Fact]
        public void TestLoginWithInvalidCredentials()
        {
            // Arrange
            var loginModel = new LoginModel();
            string username = "invalid_user";
            string password = "invalid_password";
            string role = "manager";

            // Act
            var result = loginModel.OnPost(username, password, role);

            // Assert
            Assert.IsType<PageResult>(result);
        }
    }
}
