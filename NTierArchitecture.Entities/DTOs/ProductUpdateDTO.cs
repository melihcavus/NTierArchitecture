using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Entities.DTOs
{
    public sealed record class ProductUpdateDTO(string Name, decimal UnitPrice, Guid Id, Guid CategoryId);
    
}
