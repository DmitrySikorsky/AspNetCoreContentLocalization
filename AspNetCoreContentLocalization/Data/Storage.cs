// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using AspNetCoreContentLocalization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreContentLocalization.Data
{
  public class Storage : DbContext
  {
    public DbSet<Culture> Cultures { get; set; }
    public DbSet<LocalizationSet> LocalizationSets { get; set; }
    public DbSet<Localization> Localizations { get; set; }
    public DbSet<Book> Books { get; set; }

    public Storage(DbContextOptions<Storage> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Culture>(etb =>
        {
          etb.HasKey(e => e.Code);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Cultures");
        }
      );

      modelBuilder.Entity<LocalizationSet>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("LocalizationSets");
        }
      );

      modelBuilder.Entity<Localization>(etb =>
        {
          etb.HasKey(e => new { e.LocalizationSetId, e.CultureCode });
          etb.ToTable("Localizations");
        }
      );

      modelBuilder.Entity<Book>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("Books");
        }
      );
    }
  }
}