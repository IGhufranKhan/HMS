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

        //public void DeleteDoctor(Doctor doctor)
        //{
        //    _hmsContext.Doctors.Remove(doctor);
        //    _hmsContext.SaveChanges();
        //}

        //public void DeleteDoctor(Guid id)
        //{
        //    Doctor? doctor = GetDoctorById(id);

        //    DeleteDoctor(doctor);
        //}

        public Doctor? GetDoctorById(Guid id)
        {
            var doctors = GetDoctors().ToList().FirstOrDefault(x => x.Id == id);
            return doctors;
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
            var model = GetDoctorById(doctor.Id);
            if (model != null)
            {
                model.Name = doctor.Name;
                model.Specialization = doctor.Specialization;
                model.Experience = doctor.Experience;
                model.ContactNumber = doctor.ContactNumber;
                model.Email = doctor.Email;
                model.DepartmentId = doctor.DepartmentId;

                _hmsContext.Doctors.Update(model);
                _hmsContext.SaveChanges();
            }

        }
        public void DeleteDoctor(Guid id)
        {
            var model = GetDoctorById(id);
            if (model != null)
            {
                model.IsActive = false;
                _hmsContext.Doctors.Update(model);
                _hmsContext.SaveChanges();
            }

        }
    }
}
