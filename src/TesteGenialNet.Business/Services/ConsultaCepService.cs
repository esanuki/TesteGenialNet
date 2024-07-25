using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TesteGenialNet.Business.Models;

namespace TesteGenialNet.Business.Services
{
    public class ConsultaCepService
    {
        private readonly HttpClient _httpClient;

        public ConsultaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetEndereco(string cep)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"/ws/{cep}/json/");

                var endereco = JsonConvert.DeserializeObject<Endereco>(response);

                return endereco != null ? endereco.ToString() : "";
            } catch
            {
                return string.Empty;
            }
            
        }
    }
}
