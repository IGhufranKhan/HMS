using HMS.Abstractions;
using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class DoctorService : IDoctorService
    {
        private static List<Doctor> _doctors = Seeds.Doctor();
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _doctors.Remove(doctor);
        }

        public void DeleteDoctor(Guid id)
        {
            Doctor? doctor = GetDoctorById(id);

            DeleteDoctor(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(m => m.Id == id);
        }

        public List<Doctor> GetDoctors()
        {
            _doctors = Seeds.Doctor();
            return _doctors;
        }

    }
}
