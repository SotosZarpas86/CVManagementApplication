using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace CVManagementApplication.WebApp.Pages
{
    public partial class Candidate
    {
        [Inject]
        public IDegreeService? DegreeService { get; set; }

        private List<DropdownItem> _availableDegrees = new List<DropdownItem>();

        private CandidateModel? _newCandidate;
        private CandidateModel? _existingCandidate;

        public void HandleNewCandidate(CandidateModel data)
        {
            _newCandidate = data;
            StateHasChanged();
        }

        public void HandleExistingCandidate(CandidateModel data)
        {
            _existingCandidate = data;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            _availableDegrees = await DegreeService.GetDegreeDropdown();
        }
    }
}
