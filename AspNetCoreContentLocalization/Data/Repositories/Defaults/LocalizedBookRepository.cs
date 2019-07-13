// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using AspNetCoreContentLocalization.Data.Entities;
using AspNetCoreContentLocalization.Data.Repositories.Abstractions;

namespace AspNetCoreContentLocalization.Data.Repositories.Defaults
{
  public class LocalizedBookRepository : ILocalizedBookRepository
  {
    private Storage storage;

    public LocalizedBookRepository(Storage storage)
    {
      this.storage = storage;
    }

    public IEnumerable<LocalizedBook> GetAll(string cultureCode)
    {
      //return this.storage.Books.Select(
      //  b => new LocalizedBook()
      //  {
      //    Id = b.Id,
      //    Name = b.Name.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode).Value,
      //    Description = b.Description.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode).Value,
      //    Author = b.Author.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode).Value,
      //    Year = b.Year
      //  }
      //).ToList();

      return (from b in this.storage.Books
              join lName in this.storage.Localizations on b.NameId equals lName.LocalizationSetId
              join lDescription in this.storage.Localizations on b.DescriptionId equals lDescription.LocalizationSetId
              join lAuthor in this.storage.Localizations on b.AuthorId equals lAuthor.LocalizationSetId
              where lName.CultureCode == cultureCode && lDescription.CultureCode == cultureCode && lAuthor.CultureCode == cultureCode
              select new LocalizedBook()
              {
                Id = b.Id,
                Name = lName.Value,
                Description = lDescription.Value,
                Author = lAuthor.Value,
                Year = b.Year
              }).ToList();
    }
  }
}