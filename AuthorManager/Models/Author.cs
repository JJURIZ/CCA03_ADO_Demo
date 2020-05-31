using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorManager.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
    }
}
