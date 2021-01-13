using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace BLL.BLL_Api
{
    public static class BLL_Factory
    {
        public static IBLL GetBL(string type = "")
        {
            switch (type)
            {
                case "1":
                    return new BLLImp();
                case "2":
                //return new BLImp2();
                default:
                    return new BLLImp();
            }
        }
    }
}
