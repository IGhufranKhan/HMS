using HMS.Abstractions;
using HMS.Models;

namespace HMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HmsContext _hmsContext;
        public DepartmentService(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }
        public void AddDepartment(Department department)
        {
            _hmsContext.Departments.Add(department);
        }

        public void DeleteDepartment(Department department)
        {
            _hmsContext.Departments.Remove(department);
        }

        public void DeleteDepartment(Guid id)
        {
            Department? department = GetDepartmentById(id);

            DeleteDepartment(department);
        }

        public Department? GetDepartmentById(Guid id)
        {
            var _department = GetDepartments();
            //_department = _department.FirstOrDefault(x => x.Id == id);
            return _department.FirstOrDefault(m => m.Id == id);
        }

        public List<Department> GetDepartments()
        {
            var _departments = _hmsContext.Departments.ToList();
            return _departments;
        }
    }
}
