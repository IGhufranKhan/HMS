using System.Net;

namespace HMS.Models
{
    public enum Gender 
    {
        Male,
        Female,
        Other
    }
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } 
        public int? Age { get; set; } 
        public Gender? Gender { get; set; } 
        public string? ContactNumber { get; set; } 
        public string? Email { get; set; } 
        public Address? Address { get; set; } 
        public Guid? DoctorId { get; set; } 
        public DateTime? AdmissionDate { get; set; } 
        public DateTime? DischargeDate { get; set; }
        
    }
}
