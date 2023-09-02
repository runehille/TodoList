using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.API.Controllers;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Tests
{
    public class TodoItemsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemsRepository>();
            mockRepo.Setup(repo => repo.GetAllTodoItemsAsync())
                .ReturnsAsync(
                    new List<TodoItem>()
                );
            var controller = new TodoItemsController(mockRepo.Object);
            // Act
            var result = await controller.Index();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemsRepository>();
            var controller = new TodoItemsController(mockRepo.Object);
            // Act
            var result = await controller.Details(null);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenTodoItemIsNull()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemsRepository>();
            mockRepo.Setup(repo => repo.GetTodoItemByIdAsync(It.IsAny<string?>()))
                .ReturnsAsync(() => null!);
            var controller = new TodoItemsController(mockRepo.Object);
            // Act
            var result = await controller.Details(Guid.NewGuid().ToString());
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsOkResult_WhenTodoItemIsNotNull()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemsRepository>();
            mockRepo.Setup(repo => repo.GetTodoItemByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(
                                   new TodoItem()
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       Title = "Test Title",
                                       Description = "Test Description",
                                       CreatedTimestamp = DateTime.Now,
                                       LastModifiedTimestamp = DateTime.Now,
                                   }
                                                  );
            var controller = new TodoItemsController(mockRepo.Object);
            // Act
            var result = await controller.Details(Guid.NewGuid().ToString());
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Create_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemsRepository>();
            var controller = new TodoItemsController(mockRepo.Object);
            // Act
            var result = await controller.Create(
                               new TodoItem()
                               {
                                   Id = Guid.NewGuid().ToString(),
                                   Title = "Test Title",
                                   Description = "Test Description",
                                   CreatedTimestamp = DateTime.Now,
                                   LastModifiedTimestamp = DateTime.Now,
                               }
                                          );
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
