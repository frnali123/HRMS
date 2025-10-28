using HRMS.Database;
using HRMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlPerformanceEvaluationRepository : IPerformanceEvaluationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlPerformanceEvaluationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<PerformanceEvaluation> CreateAsync(PerformanceEvaluation performanceEvaluation)
        {
            performanceEvaluation.AverageRating = (performanceEvaluation.WorkQuality +
                                        performanceEvaluation.Productivity +
                                        performanceEvaluation.Reliability +
                                        performanceEvaluation.Innovation +
                                        performanceEvaluation.Teamwork) / 5.0;
            await dbContext.PerformanceEvaluations.AddAsync(performanceEvaluation);
            await dbContext.SaveChangesAsync();
            return performanceEvaluation;
        }

        public async Task<List<PerformanceEvaluation>> GetAllPerformanceAsync()
        {
            return await dbContext.PerformanceEvaluations.ToListAsync();
        }

        public async Task<PerformanceEvaluation> GetByIdPerformanceAsync(Guid id)
        {
            return await dbContext.PerformanceEvaluations.FirstOrDefaultAsync(e => e.EvaluationId == id);
        }

        public async Task<PerformanceEvaluation> UpdateAsync(Guid id, PerformanceEvaluation performanceEvaluation)
        {
           var data= await dbContext.PerformanceEvaluations.FirstOrDefaultAsync(e => e.EvaluationId == id);
            if (data == null)
            {
                return null;
            }
            data.EvaluatedOn = performanceEvaluation.EvaluatedOn;
            data.WorkQuality = performanceEvaluation.WorkQuality;
            data.Productivity = performanceEvaluation.Productivity;
            data.Reliability = performanceEvaluation.Reliability;
            data.Teamwork = performanceEvaluation.Teamwork;
            data.Innovation = performanceEvaluation.Innovation;
            data.EvaluatedBy = performanceEvaluation.EvaluatedBy;
            data.Comments = performanceEvaluation.Comments;
            data.AverageRating = performanceEvaluation.AverageRating;
            await dbContext.SaveChangesAsync();
                return data;
        }
    }
}
