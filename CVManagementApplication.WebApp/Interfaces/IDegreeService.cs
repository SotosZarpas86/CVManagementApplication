using CVManagementApplication.WebApp.Models;

namespace CVManagementApplication.WebApp.Interfaces
{
    public interface IDegreeService
    {
        Task<List<DegreeModel>> GetAll();

        Task<DegreeModel> Create(CreateDegreeModel degreeCreateModel);

        Task<DegreeModel> Update(DegreeModel degreeModel);

        Task Delete(int degreeId);

        Task<List<DropdownItem>> GetDegreeDropdown();
    }
}
