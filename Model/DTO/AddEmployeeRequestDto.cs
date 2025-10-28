using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class AddEmployeeRequestDto
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public string Roles { get; set; } // e.g., "Employee", "Admin"
    [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, Phone]
        public string EmergencyContact { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }
        [Required]
        public string Address { get; set; }
        public string EmployeeBio { get; set; }
    public string JobRole { get; set; }
    [Range(0, 100)]
    public int AttendancePercentage { get; set; }
    [Required]
    public string WorkType { get; set; } // e.g. Remote, Work from office
    [Required]
    public string Status { get; set; }   // e.g. Active, Inactive
    [Required]
    public string FatherName { get; set; }

    //Bank Details

    [Required]
        public string BankName { get; set; }

        [Required]
        public string IFSCCode { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        //Employee Salary Details

        [Required]
        public decimal BasicPay { get; set; }

        [Required]
        public string SalaryCycle { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetSalary => BasicPay + Allowances - Deductions;

        // Employee Documents Deatails

        [Required]
        public string IdentityProof { get; set; }
        [Required]
        public string EmployeeImage { get; set; }
        [Required]
        public string TenthPassCertificate { get; set; }
        [Required]
        public string TwelfthPassCertificate { get; set; }
        [Required]
        public string GraduationCertificate { get; set; }
        [Required]
        public string ExperienceCertificate { get; set; }
    }
}
