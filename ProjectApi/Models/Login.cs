using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class Login
    {
        public long Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
