using CVManagementApplication.Core.Entities;

namespace CVManagementApplication.Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IList<Candidate>> GetAll();
        Task<Candidate> Create(Candidate candidate);
        Task<Candidate> Edit(Candidate candidate);
        Task<bool> Delete(int candidateId);
    }
}
