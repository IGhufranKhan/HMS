using HMS.Abstractions;
using HMS.Data;
using HMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HMS.Services
{
    public class PatientService : IPatientService
    {
        
        private readonly HmsContext _hmsContext;
        private readonly IConfiguration _configuration;
        public PatientService(IConfiguration configuration, HmsContext hmsContext)
        {
            _configuration = configuration;
            _hmsContext = hmsContext;
        }
        public void AddPatient(Patient patient)
        {
            _hmsContext.Patients.Add(patient);
            _hmsContext.SaveChanges();
        }

        public void DeletePatient(Patient patient)
        {
            _hmsContext.Patients.Remove(patient);
            _hmsContext.SaveChanges();
        }

        public void DeletePatient(Guid id)
        {
            Patient? patient = GetPatientById(id);
            DeletePatient(patient);
        }

        public Patient? GetPatientById(Guid id)
        {
            return _hmsContext.Patients.FirstOrDefault(m => m.Id == id);
        }

        public List<Patient> GetPatients()
        {
            return _hmsContext.Patients.ToList() ;
        }
        public List<Patient> GetPatients(string serachName)
        {
            return _hmsContext.Patients.Where(x => x.Name.Contains(serachName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public void UpdatePatient(Patient patient)
        {
            _hmsContext.Patients.Update(patient);
            _hmsContext.SaveChanges();
        }
    }
}
