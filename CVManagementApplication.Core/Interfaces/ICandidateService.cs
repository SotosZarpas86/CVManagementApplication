using CVManagementApplication.Core.Domain;

namespace CVManagementApplication.Core.Interfaces
{
    public interface ICandidateService
    {
        Task<IList<CandidateModel>> GetAll();
        Task<CandidateModel> Create(CandidateCreateModel candidateModel);
        Task<CandidateModel> Edit(int Id,CandidateModel model);
        Task<bool> Delete(int candidateId);
    }
}
