using HMS.Abstractions;
//using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HmsContext _hmsContext;
        public AppointmentService(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }

        public void AddAppointment(Appointment appointment)
        {
            _hmsContext.Appointments.Add(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _hmsContext.Appointments.Remove(appointment);
        }

        public void DeleteAppointment(Guid id)
        {
            Appointment? appointment = GetAppointmentById(id);

            DeleteAppointment(appointment);
        }

        public Appointment? GetAppointmentById(Guid id)
        {
            return _hmsContext.Appointments.FirstOrDefault(m => m.Id == id);
        }

        public List<Appointment> GetAppointments()
        {
            var _appointments = _hmsContext.Appointments.ToList();
            return _appointments;
        }
    }
}
