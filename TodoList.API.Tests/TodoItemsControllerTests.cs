using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.API.Controllers;
using TodoList.API.Dtos;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Tests
{
    public class IssuesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IIssuesRepository>();
            mockRepo.Setup(repo => repo.GetAllIssuesAsync())
                .ReturnsAsync(
                    new List<Issue>()
                );
            var controller = new IssuesController(mockRepo.Object);
            // Act
            var result = await controller.Index();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var mockRepo = new Mock<IIssuesRepository>();
            var controller = new IssuesController(mockRepo.Object);
            // Act
            var result = await controller.Details(null);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIssueIsNull()
        {
            // Arrange
            var mockRepo = new Mock<IIssuesRepository>();
            mockRepo.Setup(repo => repo.GetIssueByIdAsync(It.IsAny<string?>()))
                .ReturnsAsync(() => null!);
            var controller = new IssuesController(mockRepo.Object);
            // Act
            var result = await controller.Details(Guid.NewGuid().ToString());
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsOkResult_WhenIssueIsNotNull()
        {
            // Arrange
            var mockRepo = new Mock<IIssuesRepository>();
            mockRepo.Setup(repo => repo.GetIssueByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(
                                   new Issue()
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       Title = "Test Title",
                                       Description = "Test Description",
                                       CreatedTimestamp = DateTime.Now,
                                       LastModifiedTimestamp = DateTime.Now,
                                   }
                                                  );
            var controller = new IssuesController(mockRepo.Object);
            // Act
            var result = await controller.Details(Guid.NewGuid().ToString());
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Create_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IIssuesRepository>();
            var controller = new IssuesController(mockRepo.Object);
            // Act
            var result = await controller.Create(
                               new IssueCreateDto()
                               {
                                   Title = "Test Title",
                                   Description = "Test Description",
                                   Priority = "Low",
                                   CreatedBy = "Test User",
                                   AssignedTo = "Test User",
                               }
                                          );
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
