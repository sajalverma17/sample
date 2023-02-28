using Sample.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Sample.Domain.Database;
using System.Linq;

namespace Sample.Database
{
    public class SampleDbContext : DbContext, ISampleDbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {     
        }

        public DbSet<Container> Containers { get; set; }

        public DbSet<ProductPackage> ProductPackages { get; set; }  

        public IEnumerable<Container> ContainersInDb => this.Containers;

        public IEnumerable<ProductPackage> ProductPackagesInDb => this.ProductPackages;

        public void Init(IEnumerable<Container> containers)
        {
            foreach (var container in containers)
            {
                this.Add<Container>(container);
            }
            this.SaveChanges();
        }

        public int CreateProduct(Product product)
        {
            this.Add<Product>(product);
            this.SaveChanges();
            return product.Id;
        }

        public Product? ReadProduct(int productId)
        {
            return this.Find<Product>(productId);
        }

        public bool CreateProductPackage(ProductPackage productPackage)
        {
            var added = this.Add<ProductPackage>(productPackage);
            this.SaveChanges();
            return true;
        }

        public bool DeleteProductPackage(ProductPackage productPackage)
        {
            var removed = base.Remove<ProductPackage>(productPackage);
            this.SaveChanges();
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPackage>().HasKey(new string[] { "ContainerId", "ProductId"});
            modelBuilder.Entity<Product>();
        }
    }
}
