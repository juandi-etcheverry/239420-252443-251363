using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using TypeHelper;

namespace ApiModels.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }

        public User ToEntity()
        {
           return new User()
           {
                Email = Email,
                Address = Address,
                Role = Role,
                Password = "NotAuthorized"
            };
        }
    }
}
