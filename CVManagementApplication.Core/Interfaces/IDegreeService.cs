using CVManagementApplication.Core.Domain;

namespace CVManagementApplication.Core.Interfaces
{
    public interface IDegreeService
    {
        Task<IList<DegreeModel>> GetAll();
        Task<DegreeModel> Create(DegreeCreateModel model);
        Task<DegreeModel> Update(int Id, DegreeModel model);
        Task<bool> Delete(int candidateId);
    }
}
