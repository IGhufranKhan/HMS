using HMS.Models;

namespace HMS.Abstractions
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);


        void DeletePatient(Patient patient);

        void DeletePatient(Guid id);

        Patient? GetPatientById(Guid id);

        List<Patient> GetPatients();
        List<Patient> GetPatients(string serachName);
    }
}
