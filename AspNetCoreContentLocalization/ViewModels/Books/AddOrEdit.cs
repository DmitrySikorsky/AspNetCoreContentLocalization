// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCoreContentLocalization.ViewModels.Shared;

namespace AspNetCoreContentLocalization.ViewModels.Books
{
  public class AddOrEdit : ViewModelBase
  {
    public int? Id { get; set; }

    [Localizable]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Localizable]
    [Display(Name = "Description")]
    [Required]
    [StringLength(256)]
    public string Description { get; set; }
    public IEnumerable<Localization> DescriptionLocalizations { get; set; }

    [Localizable]
    [Display(Name = "Author")]
    [Required]
    [StringLength(64)]
    public string Author { get; set; }
    public IEnumerable<Localization> AuthorLocalizations { get; set; }

    [Display(Name = "Year")]
    [Required]
    public int Year { get; set; }
  }
}