using System.Net.Http;
using System.Threading.Tasks;
using HttpClientSample.Core.Net;

namespace HttpClientSample.Core
{
    class ProductRepository : IRepository<Product>
    {
        private readonly HttpClient client;

        public ProductRepository(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> Create(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location.PathAndQuery;

        }

        public async Task<Product> Get(string id)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>().ConfigureAwait(false);
            }
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>().ConfigureAwait(false);
            return product;
        }

        public async Task<int> Delete(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/products/{id}").ConfigureAwait(false);
            return (int)response.StatusCode;
        }
    }
}