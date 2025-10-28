using HRMS.Database;
using HRMS.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlFeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlFeedbackRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await dbContext.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> SubmitFeedbackAsync(Feedback feedback)
        {
            await dbContext.Feedbacks.AddAsync(feedback);
            await dbContext.SaveChangesAsync();
            return feedback;
        }
    }
}
