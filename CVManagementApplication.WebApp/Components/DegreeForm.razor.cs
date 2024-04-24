using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace CVManagementApplication.WebApp.Components
{
    public partial class DegreeForm
    {
        [Inject]
        public IDegreeService? DegreeService { get; set; }

        [Parameter]
        public DegreeModel? EditDegree { get; set; }

        [Parameter]
        public EventCallback<DegreeModel> DegreeAdded { get; set; }

        protected override void OnParametersSet()
        {
            if (EditDegree != null)
            {
                _degree = new CreateDegreeModel
                {
                    Name = EditDegree.Name,
                };
            }
        }

        private CreateDegreeModel? _degree = new CreateDegreeModel();

        private async Task Save()
        {
            if (EditDegree == null) //Add new record
            {
                var addResult = await DegreeService.Create(_degree);
                await DegreeAdded.InvokeAsync(addResult);
            }
            else //Edit existing one
            {
                var model = new DegreeModel
                {
                    ID = EditDegree.ID,
                    CreationTime = EditDegree.CreationTime,
                    Name = _degree.Name
                };
                var editResult = await DegreeService.Update(model);
                await DegreeAdded.InvokeAsync(editResult);
            }
        }
    }
}
