﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreDbcontext:DbContext
    {
        public StoreDbcontext(DbContextOptions<StoreDbcontext>options):base(options)
        {
            

        }

        //fluent APi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType>ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }
    }
}
