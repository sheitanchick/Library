using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int AvailableQuantity { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
