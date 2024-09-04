using HMS.Models;

namespace HMS.Abstractions
{
    public interface IDoctorService
    {
        void AddDoctor(Doctor doctor);

        void DeleteDoctor(Doctor doctor);

        void DeleteDoctor(Guid id);

        Doctor? GetDoctorById(Guid id);

        List<Doctor> GetDoctors();
        void UpdateDoctor(Doctor doctor);
    }
}
