using Banca.Api.Entities;
using Banca.Api.Interfaces;
using Newtonsoft.Json;

namespace Banca.Api.Repositories
{
    public class AhorrosRepository : IAhorroRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _url;

        public AhorrosRepository(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _url = configuration.GetValue<string>("DuckBankMs");
        }

        public async Task<List<Ahorro>> ObtenerAsync()
        {
            try
            {
                HttpResponseMessage response;
                List<Ahorro> ahorros;

                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Get, _url + "/Ahorros");
                response = await client.SendAsync(request);
                var data = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    ahorros = JsonConvert.DeserializeObject<List<Ahorro>>(await response.Content.ReadAsStringAsync());
                else
                    throw new Exception(await response.Content.ReadAsStringAsync());

                return ahorros;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> AgregarAsycn(Ahorro ahorro)
        {
            HttpResponseMessage response;

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _url + "/Ahorros");
            var content = new StringContent(JsonConvert.SerializeObject(ahorro), null, "application/json");
            request.Content = content;
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
            }
            else
                throw new Exception(await response.Content.ReadAsStringAsync());

            return 0;
        }

        public async Task<Ahorro> ObtenerAsync(string id)
        {
            HttpResponseMessage response;
            Ahorro ahorro;

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _url + "/Ahorros/" + id);
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
                ahorro = JsonConvert.DeserializeObject<Ahorro>(await response.Content.ReadAsStringAsync());
            else
                ahorro = null;

            return ahorro;
        }

        public async Task DepositarAsync(string id, MovimientoDuckBank movimiento)
        {
            HttpResponseMessage response;

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _url + $"/Ahorros/{id}/Depositos");
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), null, "application/json");
            request.Content = content;
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
            }
            string mensaje = await response.Content.ReadAsStringAsync();
        }

        public async Task RetirarAsync(string id, MovimientoDuckBank movimiento)
        {
            HttpResponseMessage response;

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _url + $"/Ahorros/{id}/Retiros");
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), null, "application/json");
            request.Content = content;
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
            }
            string mensaje = await response.Content.ReadAsStringAsync();
        }
    }
}
