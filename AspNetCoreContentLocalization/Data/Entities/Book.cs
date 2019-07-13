// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCoreContentLocalization.Data.Entities
{
  public class Book
  {
    public int Id { get; set; }
    public int NameId { get; set; }
    public int DescriptionId { get; set; }
    public int AuthorId { get; set; }
    public int Year { get; set; }

    public virtual LocalizationSet Name { get; set; }
    public virtual LocalizationSet Description { get; set; }
    public virtual LocalizationSet Author { get; set; }
  }
}