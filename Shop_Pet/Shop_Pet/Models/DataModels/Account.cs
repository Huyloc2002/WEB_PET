using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Pet.Models.DataModels
{
    [Table("Account")]
    public class Account
    {
        [Key]
     
        [Column(TypeName = "varchar"), StringLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AccountId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(150)]
        public string FullName { get; set; } = null!;
        [StringLength(150)]
        public string UserName { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(256), Column(TypeName = "varchar")]
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; } = true;
        public string Picture { get; set; } = string.Empty!;
        public bool IsActive { get; set; } = true;


    }
}
