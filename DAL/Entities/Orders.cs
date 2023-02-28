using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Orders
    {

        [Key] public Guid ID { get; set; }

        public float Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }

    }
}
