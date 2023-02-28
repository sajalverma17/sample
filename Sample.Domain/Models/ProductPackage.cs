using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Models
{
    public class ProductPackage
    {
        public int ProductId { get; set; }

        public int ContainerId { get; set; }
    }
}
