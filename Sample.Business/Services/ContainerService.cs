using Sample.Domain.Models;
using Sample.Domain.Repository;
using Sample.Domain.Dtos;

namespace Sample.Business.Services
{
    public interface IContainerService
    {
        /// <summary>
        /// Gets all containers in Db.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Container> GetContainers();

        /// <summary>
        /// Return the containers and their associated products from the Db through the product packages in Db.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ContainerDto> GetContainersWithProducts();

        public bool UpdateContainer(int containerId, IList<int> productIds);
    }

    public class ContainerService : IContainerService
    {
        private readonly ISampleDbService dbService;
        private readonly ISampleRepository repository;

        public ContainerService(
            ISampleDbService dbService,
            ISampleRepository repository)
        {
            this.dbService = dbService;
            this.repository = repository;
        }

        public IEnumerable<Container> GetContainers()
        {
            return this.repository.GetAllContainers();
        }

        public IEnumerable<ContainerDto> GetContainersWithProducts()
        {
            var containers = this.repository.GetAllContainers();

            foreach (var container in containers)
            {
                // Search for associated product Ids via ProductPackages table:
                var productIdsToLoad = this.repository
                    .GetAllProductPackages()
                    .Where(link => link.ContainerId == container.Id)
                    .Select(link => link.ProductId)
                    .ToList();

                var products = this.dbService.ReadProducts(productIdsToLoad);
                var dto = new ContainerDto()
                {
                    Id = container.Id,
                    ContainerCode = container.ContainerCode,
                    Name = container.Name,
                    Products = products,
                };

                yield return dto;
            }
        }

        public bool UpdateContainer(int containerId, IList<int> productIds)
        {
            var allPackagesOfContainer = this.repository
                .GetAllProductPackages()
                .Where(link => link.ContainerId == containerId);

            // Get packages to remove (productIds in Db that are NOT found in this Http Put reques):
            var productPackagesToRemove = allPackagesOfContainer
                .Where(link => !productIds.Contains(link.ProductId));

            foreach (var link in productPackagesToRemove)
            {
                this.dbService.DeleteProductPackage(link);
            }

            return true;
        }
    }
}
