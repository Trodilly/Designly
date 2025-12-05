using Designly.Application.Interfaces;
using Designly.Domain;

namespace Designly.Infrastructure.Persistence;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = new()
    {
        new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Enrique Cedeno",
            Birthdate = new DateTime(1985, 5, 15),
            IdentityNumber = 123456789,
            Email = "e.c@example.com"
        },
        new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Alvaro Baez",
            Birthdate = new DateTime(1990, 8, 22),
            IdentityNumber = 987654321,
            Email = "a.b@example.com"
        },
        new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Maki Reynoso",
            Birthdate = new DateTime(1992, 11, 3),
            IdentityNumber = 456123789,
            Email = "m.r@example.com"
        }
    };

    /// <summary>
    /// Retrieves all employees from the in-memory store.
    /// </summary>
    /// <returns>A collection of employees.</returns>
    public IEnumerable<Employee> GetAll() => _employees;

    /// <summary>
    /// Retrieves an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <returns>The employee if found, otherwise null.</returns>
    public Employee? GetById(Guid id) => _employees.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Adds a new employee to the in-memory store.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    /// <summary>
    /// Updates an existing employee in the in-memory store.
    /// </summary>
    /// <param name="employee">The employee with updated information.</param>
    public void Update(Employee employee)
    {
        var existing = GetById(employee.Id);
        if (existing != null)
        {
            existing.Name = employee.Name;
            existing.Birthdate = employee.Birthdate;
            existing.IdentityNumber = employee.IdentityNumber;
            existing.Email = employee.Email;
        }
    }

    /// <summary>
    /// Deletes an employee from the in-memory store by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    public void Delete(Guid id)
    {
        var employee = GetById(id);
        if (employee != null)
        {
            _employees.Remove(employee);
        }
    }
}
