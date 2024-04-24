using System.Net.Http.Json;
using System.Text.Json;
using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Models;

namespace CVManagementApplication.WebApp.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public CandidateService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CandidateModel>> GetAll()
        {
            var response = await _client.GetAsync("candidate");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var candidates = JsonSerializer.Deserialize<List<CandidateModel>>(content, _options);
            return candidates;
        }

        public async Task<CandidateModel> Create(CreateCandidateModel candidateCreateModel)
        {

            var response = await _client.PostAsJsonAsync("candidate", candidateCreateModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var candidates = JsonSerializer.Deserialize<CandidateModel>(content, _options);
            return candidates;
        }

        public async Task<CandidateModel> Update(CandidateModel candidateModel)
        {
            var response = await _client.PutAsJsonAsync($"candidate?Id={candidateModel.ID}", candidateModel);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var candidates = JsonSerializer.Deserialize<CandidateModel>(content, _options);
            return candidates;
        }

        public async Task Delete(int candidateId)
        {
            var response = await _client.DeleteAsync($"candidate?candidateId={candidateId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
    }
}
