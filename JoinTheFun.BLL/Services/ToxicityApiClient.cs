using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class ToxicityApiClient
    {
        private readonly HttpClient _http;

        public ToxicityApiClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("http://localhost:8000"); // FastAPI server
        }

        public async Task<int> PredictAsync(string text)
        {
            var response = await _http.PostAsJsonAsync("/predict", new { text });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ToxicResponse>();

            return result.Toxic;
        }
    }

    public class ToxicResponse
    {
        public int Toxic { get; set; }
    }
}
