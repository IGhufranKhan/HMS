using HMS.Abstractions;
using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class AppointmentService : IAppointmentService
    {
        private static List<Appointment> _appointments = Seeds.Appointment();
        public void AddAppointment(Appointment appointment)
        {
            _appointments.Add(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _appointments.Remove(appointment);
        }

        public void DeleteAppointment(Guid id)
        {
            Appointment? appointment = GetAppointmentById(id);

            DeleteAppointment(appointment);
        }

        public Appointment? GetAppointmentById(Guid id)
        {
            return _appointments.FirstOrDefault(m => m.Id == id);
        }

        public List<Appointment> GetAppointments()
        {
            _appointments = Seeds.Appointment();
            return _appointments;
        }
    }
}
