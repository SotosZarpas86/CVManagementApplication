using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace CVManagementApplication.WebApp.Components
{
    public partial class DegreeTable
    {
        [Inject]
        public IDegreeService? DegreeService { get; set; }

        [Parameter]
        public List<DegreeModel> DegreeList { get; set; } = new List<DegreeModel>();

        [Parameter]
        public DegreeModel? NewDegree { get; set; }

        [Parameter]
        public EventCallback<DegreeModel> DegreeToEdit { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await DegreeService.GetAll();
            DegreeList = result.ToList();
        }

        protected override void OnParametersSet()
        {
            if (NewDegree != null)
            {
                var recordToUpdate = DegreeList.FirstOrDefault(d => d.ID == NewDegree.ID);
                if (recordToUpdate != null)
                {
                    recordToUpdate.ID = NewDegree.ID;
                    recordToUpdate.Name = NewDegree.Name;
                    recordToUpdate.CreationTime = NewDegree.CreationTime;
                }
                else
                {
                    DegreeList.Add(NewDegree);
                }

                StateHasChanged();
            }
        }

        private async Task OnEditDegree(DegreeModel? degree)
        {
            await DegreeToEdit.InvokeAsync(degree);
        }

        private async Task OnDeleteDegree(int degreeId)
        {
            await DegreeService.Delete(degreeId);
            var recordToRemove = DegreeList.FirstOrDefault(d => d.ID == degreeId);
            if (recordToRemove != null)
                DegreeList.Remove(recordToRemove);
            StateHasChanged();
        }
    }
}
