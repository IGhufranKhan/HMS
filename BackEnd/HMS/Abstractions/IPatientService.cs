using HMS.Models;

namespace HMS.Abstractions
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);

        void DeletePatient(Guid id);

        Patient? GetPatientById(Guid? id);

        Task<List<Patient>> GetPatients();
        Task<List<Patient>> GetPatients(string serachName);
    }
}
