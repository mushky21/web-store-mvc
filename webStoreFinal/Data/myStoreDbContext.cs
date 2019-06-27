using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Data
{
    public class myStoreDbContext : IdentityDbContext<MyUser,IdentityRole<int>,int>
    {
        public myStoreDbContext(DbContextOptions<myStoreDbContext> options) : base(options)
        {

        }
        //the synthax dolve used in the identity project
        //    public class myStoreDbContext: IdentityDbContext<MyUser>{
        //public myStoreDbContext(DbContextOptions options) : base(options)
        //{
        //}}
        public DbSet<Product> Products { get; set; }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    // a way i found online to handle the pk exception(did not check it yet):
    //base.OnModelCreating(builder);


    //        //defining new products
    //        //builder.Entity<Product>().HasData(
    //        //    new Product {ProductKey=1, Title="Fridge", Price=1000, ProductState=State.Available, ShortDescription="25 liter second hand fridge",
    //        //        LongDescription ="white, as good as new, great for cooling your food and drinks," +
    //        //        "fits to a medium family " });


    //        //properties to add: seller, sellerId, buyer, buyer id, photos, 




    //        //defining pk (not sure that we need this since we used the [key] attribute in the model properties)
    //        //builder.Entity<Product>(entity =>
    //        //{
    //        //    entity.HasKey("ProductKey");
    //        //});

    //        ////defining fk (not finished):
    //        //builder.Entity<Product>(entity =>
    //        //{
    //        //    entity.HasMany
    //        //});
            

    //    //NOT REQUIRED configures foreign key
    //    //modelBuilder.Entity<Product>(entity =>
    //    //{
    //    //    entity.HasOne(p => p.Brand)
    //    //        .WithMany(b => b.Products)
    //    //        .HasForeignKey("BrandId");
    //    //});

    //}


    }
}
