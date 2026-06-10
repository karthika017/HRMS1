namespace HRMS.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;

        public string SpouseName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;

        public string PermanentAddress { get; set; } = string.Empty;
        public string CurrentAddress { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string PFNumber { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
    }
}