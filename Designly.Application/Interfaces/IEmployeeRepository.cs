using Designly.Domain;

namespace Designly.Application.Interfaces;

public interface IEmployeeRepository
{
    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    /// <returns>A collection of employees.</returns>
    IEnumerable<Employee> GetAll();

    /// <summary>
    /// Retrieves an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <returns>The employee if found, otherwise null.</returns>
    Employee? GetById(Guid id);

    /// <summary>
    /// Adds a new employee.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    void Add(Employee employee);

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="employee">The employee with updated information.</param>
    void Update(Employee employee);

    /// <summary>
    /// Deletes an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    void Delete(Guid id);
}
