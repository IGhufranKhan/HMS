using HMS.Models;

namespace HMS.Abstractions
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);

        void DeletePatient(Patient patient);

        void DeletePatient(Guid id);

        Patient? GetPatientById(Guid id);

        List<Patient> GetPatients();
    }
}
