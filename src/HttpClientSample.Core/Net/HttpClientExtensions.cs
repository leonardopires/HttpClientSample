using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClientSample.Core.Net
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Reads the specified <see cref="HttpContent"/> as JSON and deserializes it as the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="content">The <see cref="HttpContent"/> that will be deserialized from JSON.</param>
        /// <returns><typeparamref name="T"/></returns>
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var contentText = await content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<T>(contentText);

            return result;
        }

        /// <summary>
        /// Sends a PUT request to the specified URL, having as its content the specified object serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="client">The HTTP client.</param>
        /// <param name="url">The URL that will receive the request.</param>
        /// <param name="contentObject">The content to be serialized as JSON.</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string url, T contentObject)
        {
            var contentString = JsonConvert.SerializeObject(contentObject);

            using (var content = new StringContent(contentString, Encoding.UTF8, "application/json"))
            {
                return await client.PutAsync(url, content).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Sends a POST request to the specified URL, having as its content the specified object serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="client">The HTTP client.</param>
        /// <param name="url">The URL that will receive the request.</param>
        /// <param name="contentObject">The content to be serialized as JSON.</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string url, T contentObject)
        {
            var contentString = JsonConvert.SerializeObject(contentObject);

            using (var content = new StringContent(contentString, Encoding.UTF8, "application/json"))
            {
                return await client.PostAsync(url, content).ConfigureAwait(false);
            }
        }
    }
}
