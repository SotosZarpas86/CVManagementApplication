using System.Text;
using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CVManagementApplication.WebApp.Components
{
    public partial class CandidateForm
    {
        [Inject]
        public ICandidateService? CandidateService { get; set; }

        [Parameter]
        public CandidateModel? EditCandidate { get; set; }

        [Parameter]
        public EventCallback<CandidateModel> CandidateAdded { get; set; }

        [Parameter]
        public List<DropdownItem>? AvailableDegrees { get; set; }

        private int _selectedDegree;

        private string _selectedFile;

        private CreateCandidateModel? _candidate = new CreateCandidateModel();

        protected override void OnParametersSet()
        {
            if (EditCandidate != null)
            {
                _candidate = new CreateCandidateModel
                {
                    FirstName = EditCandidate.FirstName,
                    LastName = EditCandidate.LastName,
                    Email = EditCandidate.Email,
                    Mobile = EditCandidate.Mobile,
                    CVblob = EditCandidate.CVblob,
                };
                _selectedDegree = EditCandidate.DegreeID;
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender && _selectedDegree == 0)
                _selectedDegree = AvailableDegrees.FirstOrDefault().Id;
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                // Convert the file content to a base64 string
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);
                var base64File = Convert.ToBase64String(buffer);
                _selectedFile = base64File;


                // Optionally, you can also display the file details
                Console.WriteLine($"Uploaded File Name: {file.Name}");
                Console.WriteLine($"File Size: {file.Size} bytes");
                Console.WriteLine($"File Content (Base64): {base64File}");
            }
        }

        private async Task Save()
        {
            _candidate.DegreeID = _selectedDegree;
            _candidate.CVblob = _selectedFile;

            if (EditCandidate == null) //Add new record
            {
                var addResult = await CandidateService.Create(_candidate);
                await CandidateAdded.InvokeAsync(addResult);
            }
            else //Edit existing one
            {
                var model = new CandidateModel
                {
                    ID = EditCandidate.ID,
                    FirstName = _candidate.FirstName,
                    LastName = _candidate.LastName,
                    Email = EditCandidate.Email,
                    Mobile = _candidate.Mobile,
                    DegreeID = _selectedDegree,
                    CVblob = _selectedFile
                };
                var editResult = await CandidateService.Update(model);
                await CandidateAdded.InvokeAsync(editResult);
            }
        }
    }
}
