// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace AspNetCoreContentLocalization.Data.Entities
{
  public class LocalizationSet
  {
    public int Id { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}