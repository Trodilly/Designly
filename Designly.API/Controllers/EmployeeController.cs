using Designly.Application.Interfaces;
using Designly.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Designly.API.Controllers;

[Authorize]
[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <returns>A list of all employees.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    /// <summary>
    /// Gets an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <returns>An <see cref="Employee"/> if found, otherwise a 404 Not Found response.</returns>
    [HttpGet("{id:guid}")]
    public ActionResult<Employee> GetById(Guid id)
    {
        var employee = _repository.GetById(id);
        return employee is not null ? Ok(employee) : NotFound();
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employee">The employee object to create.</param>
    /// <returns>The newly created <see cref="Employee"/> with its assigned ID.</returns>
    [HttpPost]
    public ActionResult<Employee> Create(Employee employee)
    {
        employee.Id = Guid.NewGuid();
        _repository.Add(employee);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to update.</param>
    /// <param name="inputEmployee">The updated employee object.</param>
    /// <returns>A 204 No Content response if successful, otherwise a 404 Not Found response.</returns>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, Employee inputEmployee)
    {
        var existing = _repository.GetById(id);
        if (existing is null) return NotFound();

        inputEmployee.Id = id; // Ensure ID matches path
        _repository.Update(inputEmployee);

        return NoContent();
    }

    /// <summary>
    /// Deletes an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    /// <returns>A 204 No Content response if successful, otherwise a 404 Not Found response.</returns>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existing = _repository.GetById(id);
        if (existing is null) return NotFound();

        _repository.Delete(id);
        return NoContent();
    }
}