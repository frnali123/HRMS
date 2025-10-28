using AutoMapper;
using HRMS.Enums;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Model.HRMS.Models;

namespace HRMS.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //GetAll
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            //Create
            CreateMap<AddEmployeeRequestDto, Employee>().ReverseMap();

            //Update
            CreateMap<UpdateEmployeeRequestDto, Employee>().ReverseMap();

          

            //Mapping for Attendace table 
            //GetAll
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            //Create
            CreateMap<AddAttendanceRequestDto, Attendance>().ReverseMap();
            //update
            CreateMap<UpdateAttendanceRequestDto, Attendance>().ReverseMap();

            //Mapping for Leave table
            //Create
            CreateMap<AddLeaveRequestDto, Leave>().ReverseMap();
            //GetAll
            CreateMap<Leave,LeaveDto>().ReverseMap();
            //update 
            CreateMap<UpdateLeaveStatusDto, Leave>().ReverseMap();

            //mapping for Meeting Table
            CreateMap<AddMeetingRequestDto, Meeting>().ReverseMap();
            CreateMap<UpdateMeetingRequestDto, Meeting>().ReverseMap();
            CreateMap<Meeting, MeetingDto>().ReverseMap();
            //Add mapping for message table

            CreateMap<MessageRequestDto, Message>().ReverseMap();
            CreateMap<Message, MessageResponseDto>().ReverseMap();

            //Add Feedback for mapping
            CreateMap<AddFeedbackDto, Feedback>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();

            //Add Performance table for mapping 
            CreateMap<AddPerformanceEvaluationRequestDto, PerformanceEvaluation>().ReverseMap();
            CreateMap<PerformanceEvaluation, PerformanceEvaluationDto>().ReverseMap();
            CreateMap<UpdatePerformanceEvaluationRequestDto, PerformanceEvaluation>().ReverseMap();
     
        }
    }
}
