using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ApiModels.Responses.Products
{
    public class GetProductResponse
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public IList<Color> Colors { get; set; }
    }
}
