using Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Database
{
    /// <summary>
    /// An interface to extract EntityFramework's DbContext capabilitites.
    /// </summary>
    public interface ISampleDbContext
    {
        void Init(IEnumerable<Container> containers);

        /// <summary>
        /// Inserts product in the databse and returns the Id of the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int CreateProduct(Product product);

        Product? ReadProduct(int productIds);

        /// <summary>
        /// Inserts product package in the database.
        /// </summary>
        /// <param name="productPackage"></param>
        /// <returns></returns>
        bool CreateProductPackage(ProductPackage productPackage);

        bool DeleteProductPackage(ProductPackage productPackage);

        IEnumerable<Container> ContainersInDb { get; }

        IEnumerable<ProductPackage> ProductPackagesInDb { get; }
    }
}
