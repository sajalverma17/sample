using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Dtos
{
    /// <summary>
    /// Represents an unsaved Product passed between web and DB
    /// </summary>
    public class ProductDto
    {
        public string Name { get; set; }
    }
}
