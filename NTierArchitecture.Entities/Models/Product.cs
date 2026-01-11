using NTierArchitecture.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Entities.Models
{
    public sealed class Product : AbstractEntity
    {
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid CategoryId { get; set; }
    }
}
