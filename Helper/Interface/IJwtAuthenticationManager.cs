using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Helper.Interface
{
    public interface IJwtAuthenticationManager
    {
         string Authenticate (string username, string password );
    }
}
