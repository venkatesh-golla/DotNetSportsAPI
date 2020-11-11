using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAPI.Classes
{
    public class ProductQueryParameters:QueryParameters
    {
        public string Sku{ get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public string Name { get; set; }
    }
}
