using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using StudentManagement.DataContexts;
using StudentManagement.Models;
using StudentManagement.Services;
using Xunit;

namespace StudentTests
{
    public class StudentServiceTests
    {

        private readonly string testCsvFilePath = "test_students.csv";

        [Fact]
        public void AddStudent_ShouldAddStudent()
        {
            // Arrange
            var studentContext = new StudentContexts(testCsvFilePath);
            var studentServices = new StudentServices(studentContext);
            var studentToAdd = new Students
            {
                StudentsName = "Test Student",
                StudentsPhone = "1234567890",
                StudentsEmail = "test@gmail.com",
                StudentsAddress = "Test Address",
                Studentsusername = "testuser",
                Studentspassword = "testpassword"
            };

            // Act
            studentServices.AddStudent(studentToAdd);

            // Assert
            var addedStudent = studentContext.Student.Last();
            Assert.Equal("Test Student", addedStudent.StudentsName);
            Assert.Equal("1234567890", addedStudent.StudentsPhone);
            Assert.Equal("test@gmail.com", addedStudent.StudentsEmail);
            Assert.Equal("Test Address", addedStudent.StudentsAddress);
            Assert.Equal("testuser", addedStudent.Studentsusername);
            Assert.Equal("testpassword", addedStudent.Studentspassword);

            // Clean up
            /*File.Delete(testCsvFilePath);*/
        }

        [Fact]
        public void UpdateStudent_ShouldUpdateStudent()
        {
            // Arrange
            var studentContext = new StudentContexts(testCsvFilePath);
            var studentServices = new StudentServices(studentContext);

            // Ensure there is a student to update
            var initialStudent = new Students
            {
                StudentsName = "Initial Name",
                StudentsPhone = "1234567890",
                StudentsEmail = "initial@gmail.com",
                StudentsAddress = "Initial Address",
                Studentsusername = "initialuser",
                Studentspassword = "initialpassword"
            };
            studentServices.AddStudent(initialStudent);

            var studentToUpdate = studentContext.Student.First();
            var updatedStudent = new Students
            {
                StudentsName = "Updated Name",
                StudentsPhone = "9876543210",
                StudentsEmail = "updated@gmail.com",
                StudentsAddress = "Updated Address",
                Studentsusername = "updateduser",
                Studentspassword = "updatedpassword"
            };

            // Act
            studentServices.UpdateStudent(studentToUpdate.StudentsID, updatedStudent);

            // Assert
            var updatedStudentFromContext = studentContext.Student.FirstOrDefault(s => s.StudentsID == studentToUpdate.StudentsID);
            Assert.NotNull(updatedStudentFromContext);
            Assert.Equal("Updated Name", updatedStudentFromContext.StudentsName);
            Assert.Equal("9876543210", updatedStudentFromContext.StudentsPhone);
            Assert.Equal("updated@gmail.com", updatedStudentFromContext.StudentsEmail);
            Assert.Equal("Updated Address", updatedStudentFromContext.StudentsAddress);
            Assert.Equal("updateduser", updatedStudentFromContext.Studentsusername);
            Assert.Equal("updatedpassword", updatedStudentFromContext.Studentspassword);

            // Clean up
            File.Delete(testCsvFilePath);
        }


        [Fact]
        public void DeleteStudent_ShouldDeleteStudent()
        {
            // Arrange
            var studentContext = new StudentContexts(testCsvFilePath);
            var studentServices = new StudentServices(studentContext);
            var studentToDelete = studentContext.Student.First();

            // Act
            studentServices.DeleteStudent(studentToDelete.StudentsID);

            // Assert
            var deletedStudentFromContext = studentContext.Student.FirstOrDefault(s => s.StudentsID == studentToDelete.StudentsID);
            Assert.Null(deletedStudentFromContext);

            // Clean up
            /*File.Delete(testCsvFilePath);*/
        }
    }
}