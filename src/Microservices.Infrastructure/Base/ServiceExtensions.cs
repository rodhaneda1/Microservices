using System.Net;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microservices.Domain.Exceptions;

namespace Microservices.Infrastructure.Extensions
{
    public abstract class ServiceBae
    {
        protected StringContent ObterConteudo(object dado)
        {
            var options = new JsonSerializerOptions
            {
                Converters ={ new JsonStringEnumConverter() }
            };

            return new StringContent(JsonSerializer.Serialize(dado, options),Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
            }
            else
            {
                var resp = await responseMessage.Content.ReadAsStringAsync();
                throw new DomainException(responseMessage.StatusCode, resp);
            }
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
