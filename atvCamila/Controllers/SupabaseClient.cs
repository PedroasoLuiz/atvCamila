using atvCamila.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace atvCamila.Services
{
    public class SupabaseClient : ISupabaseClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://exlqgfmcdwniaaoeyocw.supabase.co/rest/v1";
        private const string ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV4bHFnZm1jZHduaWFhb2V5b2N3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzA4NDgyNTQsImV4cCI6MjA0NjQyNDI1NH0.rG-HEjHRUA2PTtYYYuHhiAPZx5H1BptfbTERhW1F0J8";
        private const string BearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV4bHFnZm1jZHduaWFhb2V5b2N3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzA4NDgyNTQsImV4cCI6MjA0NjQyNDI1NH0.rG-HEjHRUA2PTtYYYuHhiAPZx5H1BptfbTERhW1F0J8";

        public SupabaseClient(HttpClient httpClient)
        {
            /*
                Documentação da biblioteca httpClient disponível no site da microsoft:
                https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-9.0

            */

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            _httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);
        }
        public async Task<string> GetByIdAsync(string endpoint, int id)
        {
            var url = $"{BaseUrl}/{endpoint}?id=eq.{id}&select=*";
            var response = await _httpClient.GetAsync(url);
            return await HandleResponse(response);
        }

        public async Task<string> GetAsync(string endpoint)
        {
            var url = $"{BaseUrl}/{endpoint}?select=*";  
            var response = await _httpClient.GetAsync(url);
            return await HandleResponse(response);
        }

        public async Task<string> PostAsync(string endpoint, string jsonPayload)
        {
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}/{endpoint}", content);
            return await HandleResponse(response);
        }

        public async Task<string> PutAsync(string endpoint, string jsonPayload, int id)
        {
            endpoint += $"?id=eq.{id}";
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{endpoint}", content);
            return await HandleResponse(response);
        }
        public async Task<string> PatchAsync(string endpoint, string jsonPayload, int id)
        {
            endpoint += $"?id=eq.{id}";
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Patch, $"{BaseUrl}/{endpoint}")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            return await HandleResponse(response);
        }


        public async Task<string> DeleteAsync(string endpoint, int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{endpoint}?id=eq.{id}");
            return await HandleResponse(response);
        }

        private async Task<string> HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed: {response.StatusCode}, {errorContent}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
