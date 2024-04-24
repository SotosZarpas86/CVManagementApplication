using CVManagementApplication.WebApp.Models;

namespace CVManagementApplication.WebApp.Pages
{
    public partial class Degree
    {
        private DegreeModel? _newDegree;
        private DegreeModel? _existingDegree;

        public void HandleNewDegree(DegreeModel data)
        {
            _newDegree = data;
            StateHasChanged();
        }

        public void HandleExistingDegree(DegreeModel data)
        {
            _existingDegree = data;
            StateHasChanged();
        }
    }
}
