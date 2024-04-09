using StudentManagement.DataContexts;
using StudentManagement.Services;
using StudentManagement.Models;
using Xunit;
namespace Courses.Tests
{
    public class CourseServiceTest
    {
        private readonly string TestCsvFilePath = "test_course.csv";

        [Fact]
        public void AddCourses_ShouldBeAdd()
        {
            //Arrange
            var courseContext = new CoursesContext(TestCsvFilePath);
            var courseService = new CoursesService(courseContext);
            var courseToAdd = new modelCourses
            {
                CourseName = "Test Course",
                CourseDayofweek = "Test Date",
                CourseTime = "Test Time",
            };

            //Act
            courseService.AddCourses(courseToAdd);

            //Assert
            var addedCourse = courseContext.Course.Last();
            Assert.Equal("Test Course", addedCourse.CourseName);
            Assert.Equal("Test Date", addedCourse.CourseDayofweek);
            Assert.Equal("Test Time", addedCourse.CourseTime);
            
            File.Delete(TestCsvFilePath);
        }

        [Fact]
        public void UpdateCourse_ShouldBeUpdate()
        {
            //Arrange
            var courseContext = new CoursesContext(TestCsvFilePath);
            var courseService = new CoursesService(courseContext);

            // Add a course to the context
            var courseToAdd = new modelCourses
            {
                CourseName = "DSA",
                CourseDayofweek = "Tuesday",
                CourseTime = "11am-4pm",
            };
            courseService.AddCourses(courseToAdd);
            
            var courseToUpdate = courseContext.Course.First();
            var updatedCourse = new modelCourses
            {
                CourseName = "DSA",
                CourseDayofweek= "Monday",
                CourseTime = "11am-4pm",
            };

            //Arrange
            courseService.UpdateCourses(courseToUpdate.ID, updatedCourse);

            //Assert
            var updatedCourseFromContext = courseContext.Course.FirstOrDefault(c => c.ID == courseToUpdate.ID);
            Assert.NotNull(updatedCourseFromContext);
            Assert.Equal("DSA", updatedCourseFromContext.CourseName);
            Assert.Equal("Monday", updatedCourseFromContext.CourseDayofweek);
            Assert.Equal("11am-4pm", updatedCourseFromContext.CourseTime);

            File.Delete(TestCsvFilePath);
        }

        [Fact]
        public void DeleteCourse_ShouldBeDelete()
        {
            //Arrange
            var courseContext = new CoursesContext(TestCsvFilePath);
            var courseService =  new CoursesService(courseContext);
            // Add a course to the context
            var courseToAdd = new modelCourses
            {
                CourseName = "Test Course",
                CourseDayofweek = "Test Date",
                CourseTime = "Test Time",
            };
            courseService.AddCourses(courseToAdd);

            // Act
            var addedCourse = courseContext.Course.Last();
            courseService.Deletecourses(addedCourse.ID);

            // Assert
            var deletedCourse = courseContext.Course.FirstOrDefault(c => c.ID == addedCourse.ID);
            Assert.Null(deletedCourse);

            File.Delete(TestCsvFilePath);
        }
    }
}
