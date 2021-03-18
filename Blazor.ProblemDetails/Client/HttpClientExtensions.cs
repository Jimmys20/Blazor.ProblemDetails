using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.ProblemDetails.Client
{
    public static class HttpClientExtensions
    {
        public static Task<TValue> GetJsonAsync<TValue>(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
            => client.GetJsonAsync<TValue>(requestUri, options: null, cancellationToken);

        public static async Task<TValue> GetJsonAsync<TValue>(this HttpClient client, string requestUri, JsonSerializerOptions options, CancellationToken cancellationToken = default)
        {
            using var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TValue>(options, cancellationToken).ConfigureAwait(false);
            }

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(options, cancellationToken).ConfigureAwait(false);

            throw new ProblemDetailsException(problem);
        }
    }
}
