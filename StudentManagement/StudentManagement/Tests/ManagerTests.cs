using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using StudentManagement.DataContexts;
using StudentManagement.Models;
using StudentManagement.Services;
using Xunit;

namespace ManagerTests
{
    public class ManagerServiceTests
    {
        private readonly string testCsvFilePath = "test_managers.csv";

        [Fact]
        public void AddManager_ShouldAddManager()
        {
            // Arrange
            var managerContext = new ManagerContext(testCsvFilePath);
            var managerService = new ManagerService(managerContext);
            var managerToAdd = new Manager
            {
                Name = "Test Manager",
                Email = "testmanager@gmail.com",
                Address = "Test Address",
                Username = "testmanager",
                Password = "testpassword"
            };

            // Act
            managerService.AddManager(managerToAdd);

            // Assert
            var addedManager = managerContext.Managers.Last();
            Assert.Equal("Test Manager", addedManager.Name);
            Assert.Equal("testmanager@gmail.com", addedManager.Email);
            Assert.Equal("Test Address", addedManager.Address);
            Assert.Equal("testmanager", addedManager.Username);
            Assert.Equal("testpassword", addedManager.Password);

            // Clean up
            /*File.Delete(testCsvFilePath);*/
        }

        [Fact]
        public void UpdateManager_ShouldUpdateManager()
        {
            // Arrange
            var managerContext = new ManagerContext(testCsvFilePath);
            var managerService = new ManagerService(managerContext);

            // Ensure there is a manager to update
            var initialManager = new Manager
            {
                Name = "Initial Manager",
                Email = "initialmanager@gmail.com",
                Address = "Initial Address",
                Username = "initialmanager",
                Password = "initialpassword"
            };
            managerService.AddManager(initialManager);

            var managerToUpdate = managerContext.Managers.First();
            var updatedManager = new Manager
            {
                Name = "Updated Manager",
                Email = "updatedmanager@gmail.com",
                Address = "Updated Address",
                Username = "updatedmanager",
                Password = "updatedpassword"
            };

            // Act
            managerService.UpdateManager(managerToUpdate.Id, updatedManager);

            // Assert
            var updatedManagerFromContext = managerContext.Managers.FirstOrDefault(m => m.Id == managerToUpdate.Id);
            Assert.NotNull(updatedManagerFromContext);
            Assert.Equal("Updated Manager", updatedManagerFromContext.Name);
            Assert.Equal("updatedmanager@gmail.com", updatedManagerFromContext.Email);
            Assert.Equal("Updated Address", updatedManagerFromContext.Address);
            Assert.Equal("updatedmanager", updatedManagerFromContext.Username);
            Assert.Equal("updatedpassword", updatedManagerFromContext.Password);

            // Clean up
            File.Delete(testCsvFilePath);
        }

        [Fact]
        public void DeleteManager_ShouldDeleteManager()
        {
            // Arrange
            var managerContext = new ManagerContext(testCsvFilePath);
            var managerService = new ManagerService(managerContext);

            // Ensure there is a manager to delete
            var managerToDelete = new Manager
            {
                Name = "Manager to Delete",
                Email = "deletemanager@gmail.com",
                Address = "Delete Address",
                Username = "deletemanager",
                Password = "deletepassword"
            };
            managerService.AddManager(managerToDelete);

            // Act
            managerService.DeleteManager(managerToDelete.Id);

            // Assert
            var deletedManagerFromContext = managerContext.Managers.FirstOrDefault(m => m.Id == managerToDelete.Id);
            Assert.Null(deletedManagerFromContext);

            // Clean up
            /*File.Delete(testCsvFilePath);*/
        }
    }
}