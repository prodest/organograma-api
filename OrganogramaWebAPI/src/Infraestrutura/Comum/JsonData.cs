using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

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

        public static string SerializeObject(object value)
        {
            string json = null;

            JsonSerializerSettings jss = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new IgnoreEmptyEnumerablesResolver()
            };

            json = JsonConvert.SerializeObject(value, jss);

            return json;
        }

        public static T DeserializeObject<T>(string value) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(value);
        }


    }

    public class IgnoreEmptyEnumerablesResolver : DefaultContractResolver
    {
        public new static readonly IgnoreEmptyEnumerablesResolver Instance = new IgnoreEmptyEnumerablesResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    IEnumerable enumerable = null;

                    // this value could be in a public field or public property
                    switch (member.MemberType)
                    {
                        case MemberTypes.Property:
                            enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                .GetValue(instance, null) as IEnumerable;
                            break;
                        case MemberTypes.Field:
                            enumerable = instance
                                .GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as IEnumerable;
                            break;
                        default:
                            break;

                    }

                    if (enumerable != null)
                    {
                        // check to see if there is at least one item in the Enumerable
                        return enumerable.GetEnumerator().MoveNext();
                    }
                    else
                    {
                        // if the list is null, we defer the decision to NullValueHandling
                        return true;
                    }
                };
            }

            return property;
        }
    }
}
