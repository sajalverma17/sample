using Sample.Domain.Models;
using Sample.Domain.Database;
using Sample.Domain.Dtos;

namespace Sample.Business.Services
{
    /// <summary>
    /// To inject in API controllers for handling of CRUD operations, and relations between entites.
    /// </summary>
    public interface ISampleDbService
    {
        /// <summary>
        /// Inserts a product, and links it to a container provided
        /// </summary>
        /// <param name="productDto">Unsaved product to insert</param>
        /// <param name="container">Existing Container to link it to</param>
        /// <returns></returns>
        bool InsertProduct(ProductDto productDto, Container container);

        IList<Product> ReadProducts(IList<int> productIds);

        /// <summary>
        /// Removes packaged products from the given container
        /// </summary>
        public bool DeleteProductPackage(ProductPackage productPackage);
    }

    public class SampleDbService : ISampleDbService
    {
        private ISampleDbContext _dbContext;

        public SampleDbService(ISampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool InsertProduct(ProductDto productDto, Container container)
        {
            var usavedProduct = new Product { Name = productDto.Name };
            var savedProductId = _dbContext.CreateProduct(usavedProduct);

            if (savedProductId == 0)
            {
                return false;
            }

            var unsavedProductPackage = new ProductPackage { ContainerId = container.Id, ProductId = savedProductId };
            return _dbContext.CreateProductPackage(unsavedProductPackage);
        }

        public IList<Product> ReadProducts(IList<int> productIds)
        {
            var products = new List<Product>(productIds.Count);

            foreach (var productId in productIds)
            {
                var foundProduct = _dbContext.ReadProduct(productId);
                if (foundProduct is not null)
                {
                    products.Add(foundProduct);
                }
            }

            products.TrimExcess();
            return products;
        }

        public bool DeleteProductPackage(ProductPackage productPackage)
        {
            return _dbContext.DeleteProductPackage(productPackage);
        }
    }

}
