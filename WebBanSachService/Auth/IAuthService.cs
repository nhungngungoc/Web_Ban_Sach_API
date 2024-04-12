using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachService.Auth
{
    public interface IAuthService
    {
        dynamic checklogin(string username, string password);
    }
}
