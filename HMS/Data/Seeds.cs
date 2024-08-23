using HMS.Models;

namespace HMS.Data
{
    public static class Seeds
    {
        public static List<Patient> Patient()
        {
            var model = new List<Patient>();
            var patient = new Patient()
            {
                Id = Guid.NewGuid(),
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
                DoctorId = Guid.NewGuid()

            };
            model.Add(patient);
            return model;
        }
        public static List<Doctor> Doctor()
        {
            var model = new List<Doctor>();
            var doctor = new Doctor()
            {
                Id = Guid.NewGuid(),
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
        public static List<Appointment> Appointment()
        {
            var model = new List<Appointment>();
            var appointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(5),
                Purpose = "CBC",
                IsCompleted = true

            };
            model.Add(appointment);
            return model;
        }
        public static List<Billing> Billing()
        {
            var model = new List<Billing>();
            var billing = new Billing()
            {
                PatientId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                Amount = 5000,
                BillingDate = DateTime.Now.AddDays(-2),
                IsPaid = true

            };
            model.Add(billing);
            return model;
        }
        public static List<Department> Department()
        {
            var model = new List<Department>();
            var department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = "Cardialogy",
                HeadOfDepartment = "Imran Ullah Khan",
                ContactNumber = "090078601",
                Doctors = Doctor()

            };
            model.Add(department);
            return model;
        }
        public static void SeedMembers()
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
