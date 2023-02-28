using Microsoft.AspNetCore.Mvc;
using Sample.Business.Services;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ContainerController> _logger;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ContainerController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost("[action]")]
        public bool Insert(InsertRequestData insertRequestData)
        {
            return _productService.AddProduct(insertRequestData.Name, insertRequestData.ContainerCode);
        }

        public class InsertRequestData
        {
            public string Name { get; set; }

            public string ContainerCode { get; set; }    
        }
    }
}
