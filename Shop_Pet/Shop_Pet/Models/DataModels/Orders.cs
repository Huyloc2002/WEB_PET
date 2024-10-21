using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Pet.Models.DataModels
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string NamePet { get; set; } = string.Empty;
        public string ImagesPet { get; set; } = string.Empty;

    }
}
