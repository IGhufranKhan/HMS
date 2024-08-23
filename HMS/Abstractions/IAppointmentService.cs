using HMS.Models;

namespace HMS.Abstractions
{
    public interface IAppointmentService
    {
        void AddAppointment(Appointment appointment);

        void DeleteAppointment(Appointment appointment);

        void DeleteAppointment(Guid id);

        Appointment? GetAppointmentById(Guid id);

        List<Appointment> GetAppointments();
    }
}
