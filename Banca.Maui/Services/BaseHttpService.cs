using Newtonsoft.Json;

namespace Banca.Maui.Services
{
    public class BaseHttpService
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        public BaseHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<T> ObtenerPorIdAsync<T>(string url)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;            

            using HttpClient httpClient = _httpClientFactory.CreateClient();
            request = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)            
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());            
            else
                return default;
        }

        protected async Task<T> Get<T>(string url)
        {
            HttpClient httpClient;
            HttpRequestMessage request;
            HttpResponseMessage response;

            httpClient = _httpClientFactory.CreateClient();
            request = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
                throw new Exception($"{response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }

        protected async Task<T> Post<T>(object data, string url)
        {
            HttpRequestMessage request;
            HttpResponseMessage response;

            using HttpClient httpClient = _httpClientFactory.CreateClient();
            request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
                throw new Exception($"{response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }
    }
}
