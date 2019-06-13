using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Data
{
    public class myStoreDbContext : IdentityDbContext<MyUser>
    {
        public myStoreDbContext(DbContextOptions<myStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }


    }
}
