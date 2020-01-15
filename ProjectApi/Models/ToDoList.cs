using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class ToDoList
    {
        public long id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string data { get; set; }
    }
}
