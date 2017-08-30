using System;
using System.Threading.Tasks;

namespace HttpClientSample.Core
{
    public class SampleApplication
    {
        private ILogger Log { get; }
        private readonly IRepository<Product> repository;

        public SampleApplication(IRepository<Product> repository, ILogger log)
        {
            Log = log;
            this.repository = repository;
        }

        public async Task RunAsync()
        {
            // Create a new product
            Product product = new Product { Name = "Gizmo", Price = 100, Category = "Widgets" };

            var id = await repository.Create(product).ConfigureAwait(false);
            Log.WriteLine($"Created product: {id}");

            // Get the product
            product = await repository.Get(id).ConfigureAwait(false);
            ShowProduct(product);

            // Update the product
            Log.WriteLine("Updating price...");
            product.Price = 80;
            await repository.Update(product).ConfigureAwait(false);

            // Get the updated product
            product = await repository.Get(id).ConfigureAwait(false);
            ShowProduct(product);

            // Delete the product
            var statusCode = await repository.Delete(product.Id).ConfigureAwait(false);
            Log.WriteLine($"Deleted (HTTP Status = {statusCode})");
        }


        void ShowProduct(Product product)
        {
            Log.WriteLine($"Name: {product.Name}\tPrice: {product.Price}\tCategory: {product.Category}");
        }
    }
}