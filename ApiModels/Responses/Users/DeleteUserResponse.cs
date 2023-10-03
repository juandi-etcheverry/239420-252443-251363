using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeHelper;

namespace ApiModels.Responses.Users
{
    public class DeleteUserResponse
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
    }
}
