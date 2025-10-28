using AutoMapper;
using HRMS.Model.DTO;
using HRMS.Model;
using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services
{
    public class PerformanceService
    {

        private readonly IPerformanceEvaluationRepository evaluationRepository;
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepository;
        public PerformanceService(IPerformanceEvaluationRepository evaluationRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            this.evaluationRepository = evaluationRepository;
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
        }

        public async Task<PerformanceEvaluationDto> GetByIdAsync(Guid id)
        {
            var data = await evaluationRepository.GetByIdPerformanceAsync(id);
            return mapper.Map<PerformanceEvaluationDto>(data);
        }
        [HttpGet]
        public async Task<List<PerformanceEvaluationDto>> GetAllAsync()
        {
            var alldata = await evaluationRepository.GetAllPerformanceAsync();
            return mapper.Map<List<PerformanceEvaluationDto>>(alldata);
        }
        public async Task<PerformanceEvaluationDto> UpdateperformanceASync(Guid id, UpdatePerformanceEvaluationRequestDto updatePerformance)
        {
            var data = mapper.Map<PerformanceEvaluation>(updatePerformance);
            await evaluationRepository.UpdateAsync(id, data);
            return mapper.Map<PerformanceEvaluationDto>(data);

        }

        public async Task<PerformanceEvaluationDto> CreateAsync(AddPerformanceEvaluationRequestDto addPerformanceEvaluation)
        {
            // Step 1: VALIDATE THE INPUT
            // ExistsAsync के बजाय GetByIdAsync का उपयोग करें
            var employee = await employeeRepository.GetByIdAsync(addPerformanceEvaluation.EmployeeId);

            // अगर कर्मचारी नहीं मिलता है, तो GetByIdAsync null लौटाएगा
            if (employee == null)
            {
                return null;
            }

            // ... बाकी का कोड वैसा ही रहेगा ...
            var evaluationEntity = mapper.Map<PerformanceEvaluation>(addPerformanceEvaluation);
            var newEvaluation = await evaluationRepository.CreateAsync(evaluationEntity);
            return mapper.Map<PerformanceEvaluationDto>(newEvaluation);
        }
        //var data = mapper.Map<PerformanceEvaluation>(addPerformanceEvaluation);
        //await evaluationRepository.CreateAsync(data);
        //return mapper.Map<PerformanceEvaluationDto>(data);

    }

       
      
    }



   
