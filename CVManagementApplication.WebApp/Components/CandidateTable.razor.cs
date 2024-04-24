using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace CVManagementApplication.WebApp.Components
{
    public partial class CandidateTable
    {
        [Inject]
        public ICandidateService? CandidateService { get; set; }

        [Parameter]
        public List<CandidateModel> CandidateList { get; set; } = new List<CandidateModel>();

        [Parameter]
        public CandidateModel? NewCandidate { get; set; }

        [Parameter]
        public EventCallback<CandidateModel> CandidateToEdit { get; set; }

        [Parameter]
        public List<DropdownItem>? AvailableDegrees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await CandidateService.GetAll();
            foreach (var item in result)
            {
                item.DegreeName = AvailableDegrees.FirstOrDefault(d => d.Id == item.DegreeID)?.Name;
                item.CVFileName = "file.pdf";
                item.CVDownloadLink = $"data:application/octet-stream;base64,{item.CVblob}";
            }

            CandidateList = result.ToList();
        }

        protected override void OnParametersSet()
        {
            if (NewCandidate != null)
            {
                var recordToUpdate = CandidateList.FirstOrDefault(d => d.ID == NewCandidate.ID);
                if (recordToUpdate != null)
                {
                    recordToUpdate.ID = NewCandidate.ID;
                    recordToUpdate.FirstName = NewCandidate.FirstName;
                    recordToUpdate.LastName = NewCandidate.LastName;
                    recordToUpdate.Email = NewCandidate.Email;
                    recordToUpdate.Mobile = NewCandidate.Mobile;
                    recordToUpdate.DegreeID = NewCandidate.DegreeID;
                    recordToUpdate.CVblob = NewCandidate.CVblob;
                    recordToUpdate.CVFileName = !string.IsNullOrEmpty(recordToUpdate.CVblob) ? "file.pdf" : null;
                    recordToUpdate.CVDownloadLink = !string.IsNullOrEmpty(recordToUpdate.CVblob) ? $"data:application/octet-stream;base64,{recordToUpdate.CVblob}" : null;
                }
                else
                {
                    NewCandidate.DegreeName = AvailableDegrees.FirstOrDefault(d => d.Id == NewCandidate.DegreeID)?.Name;
                    if (!string.IsNullOrEmpty(NewCandidate.CVblob))
                    {
                        NewCandidate.CVFileName = "file.pdf";
                        NewCandidate.CVDownloadLink = $"data:application/octet-stream;base64,{NewCandidate.CVblob}";
                    }

                    CandidateList.Add(NewCandidate);
                }

                StateHasChanged();
            }
        }

        private async Task OnEditCandidate(CandidateModel? degree)
        {
            await CandidateToEdit.InvokeAsync(degree);
        }

        private async Task OnDeleteCandidate(int candidateId)
        {
            await CandidateService.Delete(candidateId);
            var recordToRemove = CandidateList.FirstOrDefault(d => d.ID == candidateId);
            if (recordToRemove != null)
                CandidateList.Remove(recordToRemove);
            StateHasChanged();
        }
    }
}
