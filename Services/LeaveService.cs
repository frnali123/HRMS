using AutoMapper;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Model.HRMS.Models;
using HRMS.Repository;

namespace HRMS.Services
{
    public class LeaveService
    {
        private readonly ILeaveRepository leaveRepository;
        private readonly IMapper mapper;

        public LeaveService(ILeaveRepository leaveRepository, IMapper mapper)
        {
            this.leaveRepository = leaveRepository;
            this.mapper = mapper;
        }
        public async Task<LeaveDto?> GetleaveByIdAsync(Guid id)
        {
            var leave = await leaveRepository.GetLeaveByIdAsync(id);
            if (leave == null)
            {
                return null;
            }
            return mapper.Map<LeaveDto>(leave);
        }
        //GetAll
        public async Task<List<LeaveDto>> GetAllleaveByIdAsync()
        {
            var leave = await leaveRepository.GetAllLeaveAsync();
            return mapper.Map<List<LeaveDto>>(leave);
        }
        // Create 
        public async Task<LeaveDto> AddleaveAsync(AddLeaveRequestDto dto)
        {
            var leave = mapper.Map<Leave>(dto);
            var createdEmployee = await leaveRepository.CreateLeaveAsync(leave);
            return mapper.Map<LeaveDto>(createdEmployee);
        }
        //Update
        public async Task<LeaveDto?> UpdateleaveAsync(UpdateLeaveStatusDto Dto)
        {
            
           var leavestaus= await leaveRepository.UpdateLeaveStatusAsync(Dto.LeaveId, Dto.Status);
            if (leavestaus == null)
            {
                return null;
            }
            return mapper.Map<LeaveDto>(leavestaus);

        }
    }
}

