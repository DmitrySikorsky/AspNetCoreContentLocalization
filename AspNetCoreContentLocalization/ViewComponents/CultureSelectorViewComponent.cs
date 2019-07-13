// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using AspNetCoreContentLocalization.Data.Repositories.Abstractions;
using AspNetCoreContentLocalization.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreContentLocalization.ViewComponents
{
  public class CultureSelectorViewComponent : ViewComponent
  {
    private ICultureRepository cultureRepository;

    public CultureSelectorViewComponent(ICultureRepository cultureRepository)
    {
      this.cultureRepository = cultureRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(this.cultureRepository.GetAll().Select(c => new Culture() { Code = c.Code, Name = c.Name }).ToList());
    }
  }
}