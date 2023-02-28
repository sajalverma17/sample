using Sample.Domain.Dtos;
using Sample.Domain.Repository;

namespace Sample.Business.Services
{
    public interface IProductService
    {
        public bool AddProduct(string productName, string containerCode);
    }

    public class ProductService : IProductService
    {
        private readonly ISampleDbService dbService;
        private readonly ISampleRepository repository;

        public ProductService(
            ISampleDbService dbService,
            ISampleRepository repository)
        {
            this.dbService = dbService;
            this.repository = repository;
        }

        public bool AddProduct(string productName, string containerCode)
        {
            var productDto = new ProductDto { Name = productName };
            var container = this.repository
                .GetAllContainers()
                .FirstOrDefault(c => c.ContainerCode.Equals(containerCode, StringComparison.OrdinalIgnoreCase));

            if (container is null)
            {
                // TODO: Fix exception handling, custom Db exception to controller
                throw new InvalidOperationException("This shouldn't have happened, selected container not found in Db");
            }

            return this.dbService.InsertProduct(productDto, container);
        }
    }
}