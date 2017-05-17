using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Organograma.Infraestrutura.Comum
{
    public class JsonData
    {
        public static async Task<T> DownloadAsync<T>(string url, string accessToken) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
                }
                else
                {
                    string mensagemErro = result.StatusCode + ": " + result.Content;
                    throw new OrganogramaException("Não foi possível obter os dados. " + mensagemErro);
                }
            }
        }
    }
}
