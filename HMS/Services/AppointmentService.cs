using HMS.Abstractions;
//using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HmsContext _hmsContext;
        private readonly IPatientService _patientService;
        public AppointmentService(HmsContext hmsContext, IPatientService patientService)
        {
            _hmsContext = hmsContext;
            _patientService = patientService;
        }

        public void AddAppointment(Appointment appointment)
        {
            _hmsContext.Appointments.Add(appointment);
            _hmsContext.SaveChanges();
        }
        public void UpdateAppointment(Appointment appointment)
        {
            if(appointment !=null)
            {
                var model = GetAppointmentById(appointment.Id);
                if (model != null)
                {
                    model.PatientId = appointment.PatientId;
                    model.DoctorId = appointment.DoctorId;
                    model.AppointmentDate = appointment.AppointmentDate;
                    model.Purpose = appointment.Purpose;
                    model.IsCompleted = appointment.IsCompleted;
                    _hmsContext.Appointments.Update(model);
                    _hmsContext.SaveChanges();
                }
            }
            
            
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _hmsContext.Appointments.Remove(appointment);
        }

        public void DeleteAppointment(Guid id)
        {
            var appointment = GetAppointmentById(id);
            if (appointment != null)
            {
                appointment.IsActive = false;
                _hmsContext.Appointments.Update(appointment); 
                _hmsContext.SaveChanges();
            }

        }

        public Appointment? GetAppointmentById(Guid id)
        {
            return GetAppointments().FirstOrDefault(m => m.Id == id);
        }

        public List<Appointment> GetAppointments()
        {
            var _appointments = _hmsContext.Appointments.Where(x => x.IsActive == true || x.IsActive == null ).ToList();
            var patient = _patientService.GetPatientById(_appointments.Select(x => x.PatientId).FirstOrDefault());
            _appointments.Select(x => x.Patient == patient);
            return _appointments;
        }
    }
}
