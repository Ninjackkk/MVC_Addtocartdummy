﻿using Microsoft.EntityFrameworkCore;
using MVC_Addtocartdummy.Models;

namespace MVC_Addtocartdummy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> ProductsTable { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}
