// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCoreContentLocalization.Data.Entities
{
  public class Localization
  {
    public int LocalizationSetId { get; set; }
    public string CultureCode { get; set; }
    public string Value { get; set; }

    public virtual LocalizationSet LocalizationSet { get; set; }
    public virtual Culture Culture { get; set; }
  }
}