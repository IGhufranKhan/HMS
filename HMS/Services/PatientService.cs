using HMS.Abstractions;
using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class PatientService : IPatientService
    {
        private static List<Patient> _patients = Seeds.Patient();
        private readonly IConfiguration _configuration;
        public PatientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AddPatient(Patient patient)
        {
            string connectionString = _configuration.GetConnectionString("HMS");

            _patients.Add(patient);
        }

        public void DeletePatient(Patient patient)
        {
            _patients.Remove(patient);
        }

        public void DeletePatient(Guid id)
        {
            Patient? patient = GetPatientById(id);

            DeletePatient(patient);
        }

        public Patient? GetPatientById(Guid id)
        {
            return _patients.FirstOrDefault(m => m.Id == id);
        }

        public List<Patient> GetPatients()
        {
            return _patients;
        }
    }
}
