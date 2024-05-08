using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
            Assert.IsTrue(model.Contains(user1));
            Assert.IsTrue(model.Contains(user2));
        }

        [TestMethod]
        public void Details_WithValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(user1, model);
        }

        [TestMethod]
        public void Details_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Details(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "" };

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Edit_WithValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Edit(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(user1, model);
        }

        [TestMethod]
        public void Edit_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Edit(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };
            var updatedUser = new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(updatedUser.Name, user1.Name);
            Assert.AreEqual(updatedUser.Email, user1.Email);
        }

        [TestMethod]
        public void Edit_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };
            var updatedUser = new User { Id = 1, Name = "", Email = "johndoe@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete_WithValidId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Delete(1, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Contains(user1));
        }

        [TestMethod]
        public void Delete_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Delete(3, null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
