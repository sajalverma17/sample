using Microsoft.AspNetCore.Mvc;
using Sample.Domain.Models;
using Sample.Business.Services;
using Sample.Domain.Dtos;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContainerController : ControllerBase
    {
        private readonly ILogger<ContainerController> _logger;
        private readonly IContainerService _containerService;

        public ContainerController(
            ILogger<ContainerController> logger,
            IContainerService service)
        {
            _logger = logger;
            _containerService = service;
        }

        [HttpGet]
        public IEnumerable<Container> Get()
        {
            return _containerService.GetContainers().ToArray();
        }

        [HttpGet("[action]")]
        public IEnumerable<ContainerDto> GetWithProducts()
        {
            return _containerService.GetContainersWithProducts().ToArray();
        }

        [HttpPut]
        public bool Put(ContainerRequest containerRequest)
        {
            var updateRequestData = containerRequest.Container;
            var containerIdToUpdate = updateRequestData.Id;
            var productIds = updateRequestData.Products.Select(p => p.Id).ToList();
            return _containerService.UpdateContainer(containerIdToUpdate, productIds);
        }

        public class ContainerRequest
        {
            public UpdateRequestData Container { get; set; }
        }

        public class UpdateRequestData
        {
            public int Id { get; set; }

            public string ContainerCode { get; set; }

            public string Name { get; set; }

            public Product[] Products { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
