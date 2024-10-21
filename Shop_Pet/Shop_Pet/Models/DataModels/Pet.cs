using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Pet.Models.DataModels
{
    [Table("Pet")]
    public class Pet
    {
        [Key]
        public int pet_id { get; set; }
        [StringLength(150)]

        public string Images { get; set; }
        [StringLength(150)]
        public string name { get; set; }
       
        [StringLength(100)]
        public string breed { get; set; } //giống

        public int age { get; set; }//Tuổi

        public double price { get; set; }//giá
        public int quantity { get; set; }//số lượng có sẵn
        [StringLength(200)]
        public string description { get; set; }

        public int? CategoryPetId { get; set; }
        public CategoryPet CategoryPet { get; set; }

        


    }
}
