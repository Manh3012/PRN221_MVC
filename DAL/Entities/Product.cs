using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string? imgPath { get; set; }
        public bool isAvailable { get; set; }
        public virtual Category? Category { get; set; }
        public bool isDeleted { get; set; }
    }
}
