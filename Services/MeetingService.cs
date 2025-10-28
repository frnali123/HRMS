using AutoMapper;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Model.HRMS.Models;
using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services
{
    public class MeetingService
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IMapper mapper;
        public MeetingService(IMeetingRepository meetingRepository, IMapper mapper)
        {
            this.meetingRepository = meetingRepository;
            this.mapper = mapper;
        }
        public async Task<MeetingDto?> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                return null;
            }
            return mapper.Map<MeetingDto>(meeting);
        }
        //GetAll
        public async Task<List<MeetingDto>> GetAllMeetingAsync()
        {
            var meeting = await meetingRepository.GetAllAsync();
            return mapper.Map<List<MeetingDto>>(meeting);
        }
        // Create 
        public async Task<MeetingDto> AddMeetingAsync(AddMeetingRequestDto dto)
        {
            var meeting = mapper.Map<Meeting>(dto);
            var createdEmployee = await meetingRepository.CreateAsync(meeting);
            return mapper.Map<MeetingDto>(createdEmployee);
        }
        //Update
        public async Task<MeetingDto?> UpdateMeetingAsync(Guid id, UpdateMeetingRequestDto Dto)
        {
            var DomainModel = mapper.Map<Meeting>(Dto);
            await meetingRepository.UpdateAsync(id, DomainModel);
            if (DomainModel == null)
            {
                return null;
            }
            return mapper.Map<MeetingDto>(DomainModel);

        }
        public async Task<MeetingDto?> DeleteMeetingAsync(Guid id)
        {
            var data = await meetingRepository.DeleteAsync(id);
            if (data == null)
            {
                return null;
            }
            return mapper.Map<MeetingDto>(data);
        }
    }
}


