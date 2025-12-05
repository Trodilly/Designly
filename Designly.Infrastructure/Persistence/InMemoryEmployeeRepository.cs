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
            Name = "John Doe",
            Birthdate = new DateTime(1985, 5, 15),
            IdentityNumber = 123456789,
            Email = "john.doe@example.com"
        },
        new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Jane Smith",
            Birthdate = new DateTime(1990, 8, 22),
            IdentityNumber = 987654321,
            Email = "jane.smith@example.com"
        },
        new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Alice Johnson",
            Birthdate = new DateTime(1992, 11, 3),
            IdentityNumber = 456123789,
            Email = "alice.johnson@example.com"
        }
    };

    public IEnumerable<Employee> GetAll() => _employees;

    public Employee? GetById(Guid id) => _employees.FirstOrDefault(e => e.Id == id);

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

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

    public void Delete(Guid id)
    {
        var employee = GetById(id);
        if (employee != null)
        {
            _employees.Remove(employee);
        }
    }
}
