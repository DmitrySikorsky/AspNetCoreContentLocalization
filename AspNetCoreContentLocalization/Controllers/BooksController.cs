// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Linq;
using AspNetCoreContentLocalization.Data;
using AspNetCoreContentLocalization.Data.Repositories.Abstractions;
using AspNetCoreContentLocalization.ViewModels.Books;
using AspNetCoreContentLocalization.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreContentLocalization.Controllers
{
  public class BooksController : LocalizableController
  {
    private Storage storage;
    private IBookRepository bookRepository;
    private ILocalizedBookRepository localizedBookRepository;

    public BooksController(Storage storage, IBookRepository bookRepository, ILocalizedBookRepository localizedBookRepository)
    {
      this.storage = storage;
      this.bookRepository = bookRepository;
      this.localizedBookRepository = localizedBookRepository;
    }

    public IActionResult Index()
    {
      return this.View(
        localizedBookRepository.GetAll(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
          .Select(b => new Book() { Id = b.Id, Name = b.Name, Description = b.Description, Author = b.Author, Year = b.Year })
      );
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult AddOrEdit(int? id)
    {
      AddOrEdit addOrEdit = new AddOrEdit();

      if (id == null)
      {
        addOrEdit.NameLocalizations = this.CreateEmptyLocalizations();
        addOrEdit.DescriptionLocalizations = this.CreateEmptyLocalizations();
        addOrEdit.AuthorLocalizations = this.CreateEmptyLocalizations();
        addOrEdit.Year = DateTime.Now.Year;
      }

      else
      {
        Data.Entities.Book book = this.bookRepository.GetById((int)id);

        addOrEdit.NameLocalizations = this.CreateLocalizationsFor(book.Name);
        addOrEdit.DescriptionLocalizations = this.CreateLocalizationsFor(book.Description);
        addOrEdit.AuthorLocalizations = this.CreateLocalizationsFor(book.Author);
        addOrEdit.Year = book.Year;
      }

      return this.View(addOrEdit);
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult AddOrEdit(AddOrEdit addOrEdit)
    {
      if (!this.ModelState.IsValid)
        return this.RedirectToAction("AddOrEdit");

      Data.Entities.Book book = addOrEdit.Id == null ? new Data.Entities.Book() : this.bookRepository.GetById((int)addOrEdit.Id);

      this.CreateOrUpdateLocalizationsFor(book);
      book.Year = addOrEdit.Year;

      if (addOrEdit.Id == null)
        this.bookRepository.Create(book);

      else this.bookRepository.Update(book);

      this.storage.SaveChanges();
      return this.RedirectToAction("Index");
    }
  }
}