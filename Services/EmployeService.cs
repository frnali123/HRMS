using AutoMapper;
using HRMS.Database;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class EmployeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
    private readonly ApplicationDbContext dbContext;
    private readonly PasswordHasher<User> _passwordHasher; // इसे इंजेक्ट करें

    public EmployeService(IEmployeeRepository employeeRepository, IMapper mapper, ApplicationDbContext dbContext, PasswordHasher<User> _passwordHasher)
        {
           this.employeeRepository = employeeRepository;
           this.mapper = mapper;
      this.dbContext = dbContext;
      this._passwordHasher = _passwordHasher;
        }
        public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return null;
            }
            return mapper.Map<EmployeeDto>(employee);
        }
        //GetAll
        public async Task<List<EmployeeDto>> GetAllEmployeeAsync()
        {
            var employees = await employeeRepository.GetAllAsync();
            return mapper.Map<List<EmployeeDto>>(employees);
        }
    // Create
    // --- यह रहा आपका मुख्य Solution ---
    public async Task<EmployeeDto> AddEmployeeAsync(AddEmployeeRequestDto dto)
    {
      // 1. चेक करें कि User (Email) पहले से मौजूद है या नहीं
      if (await dbContext.Users.AnyAsync(u => u.Username == dto.Email))
      {
        throw new Exception("Email already exists."); // या null रिटर्न करें
      }

      // 2. User ऑब्जेक्ट बनाएँ
      var user = new User
      {
        Id = Guid.NewGuid(),
        Username = dto.Email,
        Roles = dto.Roles,
        PasswordHash = _passwordHasher.HashPassword(null, dto.Password)
      };

      // 3. User को Context में Add करें
      await dbContext.Users.AddAsync(user);

      // 4. Employee ऑब्जेक्ट बनाएँ (AutoMapper से)
      var employee = mapper.Map<Employee>(dto);

      // 5. Employee की अपनी Id जनरेट करें
      employee.Id = Guid.NewGuid();

      // 6. --- सबसे ज़रूरी स्टेप: User Id को Employee से लिंक करें ---
      employee.UserId = user.Id;

      // 7. Employee को Repository से Create करें
      // (Repository का CreateAsync 'SaveChangesAsync' कॉल करता है,
      // जो User और Employee दोनों को एक साथ सेव कर देगा)
      var createdEmployee = await employeeRepository.CreateAsync(employee);

      return mapper.Map<EmployeeDto>(createdEmployee);
    }
    //public async Task<EmployeeDto> AddEmployeeAsync(AddEmployeeRequestDto dto)
    //{
    //    var employee = mapper.Map<Employee>(dto);
    //    var createdEmployee = await employeeRepository.CreateAsync(employee);
    //    return mapper.Map<EmployeeDto>(createdEmployee);
    //}
    //Update
    public async Task<EmployeeDto?> UpdateEmployeeAsync( Guid id, UpdateEmployeeRequestDto updateEmployeeRequestDto)
        {
            var DomainModel = mapper.Map<Employee>(updateEmployeeRequestDto);
            await employeeRepository.UpdateAsync(id, DomainModel);
            if (DomainModel == null)
            {
                return null;
            }
            return mapper.Map<EmployeeDto>(DomainModel);

        }
    }
}
