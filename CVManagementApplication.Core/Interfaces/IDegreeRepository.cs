using CVManagementApplication.Core.Entities;

namespace CVManagementApplication.Core.Interfaces
{
    public interface IDegreeRepository
    {
        Task<IList<Degree>> GetAll();
        Task<Degree> GetDegreeByName(string name);
        Task<Degree> Create(Degree degree);
        Task<Degree> Update(Degree degree);
        Task<bool> Delete(int degreeId);
    }
}
