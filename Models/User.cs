using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
