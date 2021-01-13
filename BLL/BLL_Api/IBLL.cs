using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Api
{
    public interface IBLL
    {
        bool IsAdmin(string name, string password);
    }
}
