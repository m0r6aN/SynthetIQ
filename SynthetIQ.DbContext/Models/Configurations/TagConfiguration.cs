﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynthetIQ.DbContext.Models;
using System;
using System.Collections.Generic;

namespace SynthetIQ.DbContext.Models.Configurations
{
    public partial class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4CFE96139B");

            entity.HasIndex(e => e.Name, "UQ__Tags__737584F69A22842F").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Tag> entity);
    }
}