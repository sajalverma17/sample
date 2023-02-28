﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
