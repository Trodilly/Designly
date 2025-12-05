using Designly.API.Controllers;
using Designly.Application.Interfaces;
using Designly.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Designly.Tests;

public class EmployeeControllerTests
{
    private readonly Mock<IEmployeeRepository> _repositoryMock;
    private readonly EmployeeController _controller;

    public EmployeeControllerTests()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _controller = new EmployeeController(_repositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnOkWithEmployees()
    {
        // Arrange
        var employees = new List<Employee> { new Employee(), new Employee() };
        _repositoryMock.Setup(x => x.GetAll()).Returns(employees);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnEmployees = Assert.IsType<List<Employee>>(okResult.Value);
        Assert.Equal(2, returnEmployees.Count);
    }

    [Fact]
    public void GetById_ShouldReturnOk_WhenEmployeeExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var employee = new Employee { Id = id };
        _repositoryMock.Setup(x => x.GetById(id)).Returns(employee);

        // Act
        var result = _controller.GetById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(employee, okResult.Value);
    }

    [Fact]
    public void GetById_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(x => x.GetById(id)).Returns((Employee?)null);

        // Act
        var result = _controller.GetById(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var employee = new Employee();

        // Act
        var result = _controller.Create(employee);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(EmployeeController.GetById), createdResult.ActionName);
    }
}
