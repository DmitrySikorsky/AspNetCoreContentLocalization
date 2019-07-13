// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using AspNetCoreContentLocalization.Data.Entities;
using AspNetCoreContentLocalization.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreContentLocalization.Data.Repositories.Defaults
{
  public class BookRepository : IBookRepository
  {
    private Storage storage;

    public BookRepository(Storage storage)
    {
      this.storage = storage;
    }

    public Book GetById(int id)
    {
      return this.storage.Books
        .Include(b => b.Name).ThenInclude(ls => ls.Localizations)
        .Include(b => b.Description).ThenInclude(ls => ls.Localizations)
        .Include(b => b.Author).ThenInclude(ls => ls.Localizations)
        .FirstOrDefault(b => b.Id == id);
    }

    public void Create(Book book)
    {
      this.storage.Books.Add(book);
    }

    public void Update(Book book)
    {
    }
  }
}