using HMS.Abstractions;
using HMS.Models;
using HMS.ViewModels;
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
            patient.IsActive = true;
            _hmsContext.Patients.Add(patient);
            _hmsContext.SaveChanges();
        }

        public Patient? GetPatientById(Guid? id)
        {
            var _patients = GetPatients();
            return _patients.Result.FirstOrDefault(m => m.Id == id);
        }

        public async Task<List<Patient>> GetPatients()
        {
            var _patients = await _hmsContext.Patients.Where(x => x.IsActive == true).ToListAsync();
            var addresses = _addressService.GetAddresss();
            var doctors = _doctorService.GetDoctors();

            //_patients = _patients.Where(x => addresses.Contains(x.Address)).ToList();
            //_patients = _patients.Where(x => doctors.Contains(x.Doctor)).ToList();

            return _patients;
        }
        public async Task<List<Patient>> GetPatients(string serachName)
        {
            return await _hmsContext.Patients.Where(x => x.Name.Contains(serachName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
        public void UpdatePatient(Patient patient)
        {
            if (patient != null)
            {
                var model = GetPatientById(patient.Id);

                if (model != null)
                {
                    model.Name = patient.Name;
                    model.Age = patient.Age;
                    model.Gender = patient.Gender;
                    model.ContactNumber = patient.ContactNumber;
                    model.Email = patient.Email;
                    model.Address.Id = model.Address.Id;
                    model.Address.Country = patient.Address.Country;
                    model.Address.State = patient.Address.State;
                    model.Address.City = patient.Address.City;
                    model.Address.PostalCode = patient.Address.PostalCode;
                    model.Address.Street = patient.Address.Street;
                    
                    model.DoctorId = patient.DoctorId;
                    model.AdmissionDate = patient.AdmissionDate;
                    model.DischargeDate = patient.DischargeDate;
                    model.ProfilePictureId = patient.ProfilePictureId;
                }
                _hmsContext.Patients.Update(model);
                _hmsContext.SaveChanges();
            }
        }
        public void DeletePatient(Guid id)
        {
            var model = GetPatientById(id);
            if (model != null)
            {
                 model.IsActive = false;
                _hmsContext.Patients.Update(model);
                _hmsContext.SaveChanges();
            }

        }
    }
}
