using HMS.Models;

namespace HMS.Abstractions
{
    public interface IDepartmentService
    {
        void AddDepartment(Department department);

        void DeleteDepartment(Department department);

        void DeleteDepartment(Guid id);

        Department? GetDepartmentById(Guid id);

        List<Department> GetDepartments();
    }
}
