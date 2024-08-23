using HMS.Abstractions;
using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private static List<Department> _departments = Seeds.Department();
        public void AddDepartment(Department department)
        {
            _departments.Add(department);
        }

        public void DeleteDepartment(Department department)
        {
            _departments.Remove(department);
        }

        public void DeleteDepartment(Guid id)
        {
            Department? department = GetDepartmentById(id);

            DeleteDepartment(department);
        }

        public Department? GetDepartmentById(Guid id)
        {
            return _departments.FirstOrDefault(m => m.Id == id);
        }

        public List<Department> GetDepartments()
        {
            _departments = Seeds.Department();
            return _departments;
        }
    }
}
