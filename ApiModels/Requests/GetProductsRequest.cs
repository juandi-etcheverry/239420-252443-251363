using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.Requests
{
    public class GetProductsRequest
    {
        public string? Brand { get; set; } = "";
        public string? Category { get; set; } = "";
        public string? Text { get; set; } = "";
    }
}
