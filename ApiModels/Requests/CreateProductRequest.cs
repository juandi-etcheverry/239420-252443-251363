using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Colors { get; set; }
    }
}
