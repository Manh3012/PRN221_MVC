using System.ComponentModel.DataAnnotations;

namespace DAL.Entities {
    public class Orders {

        [Key]
        public Guid ID { get; set; }

        public float Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
