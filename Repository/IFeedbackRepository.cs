using HRMS.Model;

namespace HRMS.Repository
{
    public interface IFeedbackRepository
    {
        Task<Feedback> SubmitFeedbackAsync(Feedback feedback);
        Task<List<Feedback>> GetAllFeedbacksAsync();
    }
}
