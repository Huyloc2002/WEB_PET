using Microsoft.EntityFrameworkCore;

namespace Shop_Pet.Models.DataModels
{
    public class ShopPetDbContext:DbContext
    {
        public ShopPetDbContext() { }
        public ShopPetDbContext(DbContextOptions<ShopPetDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<CategoryPet> Categories { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Orders> Orders { get; set; }
    


    }
}
