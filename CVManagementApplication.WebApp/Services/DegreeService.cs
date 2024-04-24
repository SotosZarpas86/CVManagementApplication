using System.Net.Http.Json;
using System.Text.Json;
using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;

namespace CVManagementApplication.WebApp.Services
{
    public class DegreeService : IDegreeService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public DegreeService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<DegreeModel>> GetAll()
        {
            var response = await _client.GetAsync("degree");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var degrees = JsonSerializer.Deserialize<List<DegreeModel>>(content, _options);
            return degrees;
        }

        public async Task<DegreeModel> Create(CreateDegreeModel degreeCreateModel)
        {
            var response = await _client.PostAsJsonAsync("degree", degreeCreateModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var degrees = JsonSerializer.Deserialize<DegreeModel>(content, _options);
            return degrees;
        }

        public async Task<DegreeModel> Update(DegreeModel degreeModel)
        {
            var response = await _client.PutAsJsonAsync($"degree?Id={degreeModel.ID}", degreeModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var degrees = JsonSerializer.Deserialize<DegreeModel>(content, _options);
            return degrees;
        }

        public async Task Delete(int degreeId)
        {
            var response = await _client.DeleteAsync($"degree?degreeId={degreeId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task<List<DropdownItem>> GetDegreeDropdown()
        {
            var degrees = await GetAll();
            var dropdownList = new List<DropdownItem>();
            foreach (var degree in degrees)
            {
                var item = new DropdownItem
                {
                    Id = degree.ID,
                    Name = degree.Name
                };
                dropdownList.Add(item);
            }
            return dropdownList;
        }
    }
}
