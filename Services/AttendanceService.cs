using AutoMapper;
using HRMS.Model.DTO;
using HRMS.Model;
using HRMS.Repository;
using HRMS.Model.HRMS.Models;
using HRMS.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRMS.Services
{
    public class AttendanceService
    {

        private readonly IAttendanceRepository attendanceRepository;
        private readonly IMapper mapper;
    private readonly ApplicationDbContext dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AttendanceService(IAttendanceRepository attendanceRepository,
      IMapper mapper, ApplicationDbContext dbContext, IHttpContextAccessor _httpContextAccessor)
        {
            this.attendanceRepository = attendanceRepository;
            this.mapper = mapper;
            this._httpContextAccessor = _httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<AttendanceDto?> GetAttendanceByIdAsync(Guid id)
        {
            var attendance = await attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return null;
            }
            return mapper.Map<AttendanceDto>(attendance);
        }
        //GetAll
        public async Task<List<AttendanceDto>> GetAllAttendanceAsync()
        {
            var attendance = await attendanceRepository.GetAllAsync();
            return mapper.Map<List<AttendanceDto>>(attendance);
        }
        // Create 
        public async Task<AttendanceDto> AddAttendanceAsync(AddAttendanceRequestDto dto)
        {
      //var attendance = mapper.Map<Attendance>(dto);
      //var createdEmployee = await attendanceRepository.CreateAsync(attendance);
      //return mapper.Map<AttendanceDto>(createdEmployee);
      // 1. टोकन से EmployeeId निकालें
      var employeeIdString = _httpContextAccessor.HttpContext.User
                              .FindFirstValue("EmployeeId"); // या जो भी क्लेम का नाम है

      if (string.IsNullOrEmpty(employeeIdString))
      {
        throw new Exception("EmployeeId not found in token.");
      }

      if (!Guid.TryParse(employeeIdString, out Guid employeeIdGuid))
      {
        throw new Exception("Invalid EmployeeId format in token.");
      }

      // 2. --- यह रहा असली फिक्स ---
      //    इंसर्ट करने से पहले चेक करें कि एम्प्लॉई मौजूद है या नहीं
      var employeeExists = await dbContext.Employees
                              .AnyAsync(e => e.Id == employeeIdGuid);

      if (!employeeExists)
      {
        // अगर एम्प्लॉई मौजूद नहीं है, तो वही एरर दें जो DB दे रहा था
        throw new Exception($"Employee not found with Id: {employeeIdGuid}. Please log out and log in again.");
      }

      // 3. अगर सब ठीक है, तो अटेंडेंस बनाएँ
      var attendance = mapper.Map<Attendance>(dto);

      // EmployeeId को टोकन से सेट करें
      attendance.EmployeeId = employeeIdGuid;

      // (यह सुनिश्चित करें कि बाकी DTO फ़ील्ड्स भी मैप हो रहे हैं)
      attendance.PunchInTime = dto.PunchInTime; // या DateTime.Now
      attendance.Status = dto.Status;

      var createdAttendance = await attendanceRepository.CreateAsync(attendance);
      return mapper.Map<AttendanceDto>(createdAttendance);
    }
    //Update
    public async Task<AttendanceDto?> UpdateAttendanceAsync(Guid id, UpdateAttendanceRequestDto Dto)
        {
            var DomainModel = mapper.Map<Attendance>(Dto);
            await attendanceRepository.UpdateAsync(id, DomainModel);
            if (DomainModel == null)
            {
                return null;
            }
            return mapper.Map<AttendanceDto>(DomainModel);

        }
    }
}

