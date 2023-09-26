using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.Requests.Users
{
    public class GetUserRequest
    {
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
