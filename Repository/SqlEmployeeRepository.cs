using HRMS.Database;
using HRMS.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlEmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

       

        public async Task<List<Employee>> GetAllAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
           return await dbContext.Employees.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Employee> UpdateAsync(Guid id, Employee employee)
        {
           var updateid= await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if(updateid==null)
            {
                return null;

            }
            updateid.FullName = employee.FullName;
            updateid.Email = employee.Email;
            updateid.PhoneNumber = employee.PhoneNumber;
            updateid.EmergencyContact = employee.EmergencyContact;
            updateid.JoiningDate = employee.JoiningDate;
            updateid.Position = employee.Position;
            updateid.Department = employee.Department;
            updateid.Address = employee.Address;
            updateid.EmployeeBio = employee.EmployeeBio;
            updateid.BankName = employee.BankName;
            updateid.IFSCCode = employee.IFSCCode;
            updateid.AccountNumber = employee.AccountNumber;
            updateid.BasicPay = employee.BasicPay;
            updateid.SalaryCycle = employee.SalaryCycle;
            updateid.Allowances = employee.Allowances;
            updateid.Deductions = employee.Deductions;
            updateid.IdentityProof = employee.IdentityProof;
            updateid.EmployeeImage = employee.EmployeeImage;
            updateid.TenthPassCertificate = employee.TenthPassCertificate;
            updateid.TwelfthPassCertificate = employee.TwelfthPassCertificate;
            updateid.GraduationCertificate = employee.GraduationCertificate;
            updateid.ExperienceCertificate = employee.ExperienceCertificate;
      updateid.JobRole = employee.JobRole;
      updateid.FatherName = employee.FatherName;
      updateid.AttendancePercentage = employee.AttendancePercentage;
      updateid.Status = employee.Status;
      updateid.WorkType = employee.WorkType;

            await dbContext.SaveChangesAsync();
            return updateid;

       
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            // AnyAsync डेटाबेस में बहुत तेजी से जांच करता है कि कोई रिकॉर्ड मौजूद है या नहीं।
            // यह GetByIdAsync से बेहतर है क्योंकि यह पूरा डेटा नहीं लाता है।
            return await dbContext.Employees.AnyAsync(e => e.Id == id);
        }
    }
}
