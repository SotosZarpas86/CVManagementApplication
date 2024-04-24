using CVManagementApplication.WebApp.Models;

namespace CVManagementApplication.WebApp.Interfaces
{
    public interface ICandidateService
    {
        Task<List<CandidateModel>> GetAll();

        Task<CandidateModel> Create(CreateCandidateModel candidateCreateModel);

        Task<CandidateModel> Update(CandidateModel candidateModel);

        Task Delete(int candidateId);
    }
}
