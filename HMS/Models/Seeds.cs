using HMS.Extensions;

namespace HMS.Models
{
    public class Seeds
    {
        Guid guid = Guid.Empty;
        public List<Patient> SeedPatient()
        {
            var model = new List<Patient>();
            var patient = new Patient()
            {
                Id = guid.NewGuid(),
                Name = "Ghufran",
                Age = 26,
                Gender = Gender.Male,
                ContactNumber = "+92 3244562896",
                Email = "devghufran786@gmail.com",
                Address = new Address
                {
                    Street = "19",
                    City = "Lahore",
                    State = "Punjab",
                    Country = "Pakistan",
                    PostalCode = "54900",
                },
                AdmissionDate = DateTime.Now.AddDays(-3),
                DischargeDate = DateTime.Now,
                DoctorId = guid.NewGuid()

            };
            model.Add(patient);
            return model;
        }
        public List<Doctor> SeedDoctor()
        {
            var model = new List<Doctor>();
            var doctor = new Doctor()
            {
                Id = guid.NewGuid(),
                Name = "Ghufran Khan",
                Specialization = Specialization.Cardiology,
                Experience = 5,
                ContactNumber = "+92 3244562896",
                Email = "devghufran786@gmail.com",
                DepartmentId = null,

            };
            model.Add(doctor);
            return model;
        }
        public List<Appointment> SeedAppointment()
        {
            var model = new List<Appointment>();
            var appointment = new Appointment()
            {
                Id = guid.NewGuid(),
                PatientId = guid.NewGuid(),
                DoctorId = guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(5),
                Purpose = "CBC",
                IsCompleted = true

            };
            model.Add(appointment);
            return model;
        }
        public List<Billing> SeedBilling()
        {
            var model = new List<Billing>();
            var billing = new Billing()
            {
                PatientId = guid.NewGuid(),
                DoctorId = guid.NewGuid(),
                Amount = 5000,
                BillingDate = DateTime.Now.AddDays(-2),
                IsPaid = true

            };
            model.Add(billing);
            return model;
        }
        public List<Department> SeedDepartment()
        {
            var model = new List<Department>();
            var department = new Department()
            {
                Id = guid.NewGuid(),
                Name = "Cardialogy",
                HeadOfDepartment = "Imran Ullah Khan",
                ContactNumber = "090078601",
                Doctors = SeedDoctor()

            };
            model.Add(department);
            return model;
        }
        public void SeedMembers()
        {
            var model = new List<Member>();
            var members = new List<Member>()
            {
                new Member { Id = 1, Name = "Ghufran", Email = "ghfuran@gmail.com" },
                new Member { Id = 2, Name = "Saad", Email = "saad@gmail.com" },
            };
            model.AddRange(members);

        }
    }


}


