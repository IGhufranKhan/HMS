using HMS.Abstractions;
using HMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS.Services
{
    public class PatientService : IPatientService
    {
        
        private readonly HmsContext _hmsContext;
        private readonly IConfiguration _configuration;
        private readonly IAddressService _addressService;
        private readonly IDoctorService _doctorService;
        public PatientService(IConfiguration configuration, HmsContext hmsContext, IAddressService addressService, IDoctorService doctorService)
        {
            _configuration = configuration;
            _hmsContext = hmsContext;
            _addressService = addressService;
            _doctorService = doctorService;
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
            if(patient != null)
            {
                DeletePatient(patient);
            }
            
            
        }

        public Patient? GetPatientById(Guid id)
        {
            var _patients = GetPatients();
            return _patients.Result.FirstOrDefault(m => m.Id == id);
        }

        public async Task <List<Patient>> GetPatients()
        {
            var _patients = await _hmsContext.Patients.Where(x => x.IsActive == true).ToListAsync();
            var addresses = _addressService.GetAddresss(); 
            var doctors = _doctorService.GetDoctors();
            
            //_patients = _patients.Where(x => addresses.Contains(x.Address)).ToList();
            //_patients = _patients.Where(x => doctors.Contains(x.Doctor)).ToList();

            return _patients;
        }
        public async Task <List<Patient>> GetPatients(string serachName)
        {
            return await _hmsContext.Patients.Where(x => x.Name.Contains(serachName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
        public void UpdatePatient(Patient patient)
        {
            _hmsContext.Patients.Update(patient);
            _hmsContext.SaveChanges();
        }
    }
}
