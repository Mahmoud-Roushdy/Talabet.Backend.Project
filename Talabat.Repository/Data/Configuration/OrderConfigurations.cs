﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Repository.Data.Configuration
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                   S => S.ToString(),
                   S => (OrderStatus)Enum.Parse(typeof(OrderStatus), S)
                );
            builder.Property(O => O.Subtotal)
                .HasColumnType("decimal(18,2)");
            builder.HasMany(O => O.Items).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
