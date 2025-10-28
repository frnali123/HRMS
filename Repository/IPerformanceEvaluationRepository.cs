using HRMS.Model;

namespace HRMS.Repository
{
    public interface IPerformanceEvaluationRepository
    {
        Task<List<PerformanceEvaluation>> GetAllPerformanceAsync();
        Task<PerformanceEvaluation> GetByIdPerformanceAsync(Guid id);
        Task<PerformanceEvaluation>CreateAsync(PerformanceEvaluation performanceEvaluation);
        Task<PerformanceEvaluation> UpdateAsync(Guid id, PerformanceEvaluation performanceEvaluation);

    }
}
