using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int ID { get; set; }
    }
}
