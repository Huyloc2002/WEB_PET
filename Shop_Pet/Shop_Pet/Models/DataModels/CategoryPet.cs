using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Pet.Models.DataModels
{
    [Table("CategoryPet")]
    public class CategoryPet
    {
        [Key]
        public int CategoryPetId { get; set; }
        [StringLength(100)]
        public string species { get; set; } //loài

        public ICollection<Pet> Pets { get; set; }
    }
}
