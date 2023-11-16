using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ApiModels.Responses.Users
{
    public class GetAllUsersResponse
    {
        public List<User> Users { get; set; }
    }
}
