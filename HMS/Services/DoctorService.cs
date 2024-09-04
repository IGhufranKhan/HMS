using HMS.Abstractions;
using HMS.Models;

namespace HMS.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HmsContext _hmsContext;
        private readonly IDepartmentService _departmentService;
        public DoctorService(HmsContext hmsContext, IDepartmentService departmentService)
        {
            _hmsContext = hmsContext;
            _departmentService = departmentService;
        }
        public void AddDoctor(Doctor doctor)
        {
            _hmsContext.Doctors.Add(doctor);
            _hmsContext.SaveChanges();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _hmsContext.Doctors.Remove(doctor);
            _hmsContext.SaveChanges();
        }

        public void DeleteDoctor(Guid id)
        {
            Doctor? doctor = GetDoctorById(id);

            DeleteDoctor(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _hmsContext.Doctors.FirstOrDefault(m => m.Id == id);
        }

        public List<Doctor> GetDoctors()
        {
            var _doctors = _hmsContext.Doctors.ToList();
            
            var department = _departmentService.GetDepartments();
            //_doctors = _doctors.Where( x => department.Contains(x.Department)).ToList();
            _doctors = _doctors.Where(x => x.IsActive == true).ToList();
            return _doctors;
        }
        public void UpdateDoctor(Doctor doctor)
        {
            _hmsContext.Doctors.Update(doctor);
            _hmsContext.SaveChanges();
        }

    }
}
