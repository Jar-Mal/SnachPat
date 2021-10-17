using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Services.Interfaces
{
    public interface IAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
