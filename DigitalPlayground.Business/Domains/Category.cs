using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category() { }
        public Category(int categoryId, string name)
        {
            Id = categoryId;
            Name = name;
        }
        
    }
}
