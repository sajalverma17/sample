using Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Dtos
{
    /// <summary>
    /// Represents an unsaved Product passed between web and DB
    /// </summary>
    public class ContainerDto
    {
        public int Id { get; set; }

        public string ContainerCode { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
