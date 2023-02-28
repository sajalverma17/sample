using Sample.Domain.Dtos;
using Sample.Domain.Models;
using Sample.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Repository
{
    /// <summary>
    /// The idea is to have a cache-able layer, 
    /// and also an interface for readonly access, where needed in frontend.
    /// 
    /// So far only acts as a wrapper of the database with no caching.
    /// </summary>
    public interface ISampleRepository
    {
        IEnumerable<Container> GetAllContainers();  
        
        IEnumerable<ProductPackage> GetAllProductPackages();
    }

    public class SampleRepository : ISampleRepository
    {
        private readonly ISampleDbContext _dbContext;

        public SampleRepository(ISampleDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Container> GetAllContainers()
        {
            return this._dbContext.ContainersInDb;
        }

        public IEnumerable<ProductPackage> GetAllProductPackages()
        {
            return this._dbContext.ProductPackagesInDb;
        }
    }
}
