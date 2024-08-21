namespace HMS.Models
{
    public enum Specialization
    {
        Cardiology,
        Neurology,
        GeneralMedicine,
        Orthopedics
    }
    public class Doctor
    {
        public Guid Id { get; set; } 
        public string? Name { get; set; } 
        public Specialization? Specialization { get; set; } 
        public int? Experience { get; set; } 
        public string? ContactNumber { get; set; } 
        public string? Email { get; set; } 
        public Guid? DepartmentId { get; set; }
    }
}
