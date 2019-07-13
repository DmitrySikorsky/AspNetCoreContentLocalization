﻿// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using AspNetCoreContentLocalization.Data.Entities;

namespace AspNetCoreContentLocalization.Data.Repositories.Abstractions
{
  public interface ILocalizationRepository
  {
    void Create(Localization localization);
    void Delete(IEnumerable<Localization> localizations);
  }
}