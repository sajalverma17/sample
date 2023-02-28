using Sample.Domain.Models;
using Sample.Domain.Database;

namespace Sample.Business.Services
{
    public class DbInitializer
    {
        public static void Initialize(ISampleDbContext dBContext)
        {
            var containers = new List<Container>() {
                new Container { ContainerCode = "DT101", Name = "Food Container" },
                new Container { ContainerCode = "DT102", Name = "Accessories Container" },
                new Container { ContainerCode = "DT103", Name = "Spare PC Parts" },
            };

            dBContext.Init(containers);
        }
    }
}
