// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCoreContentLocalization.ViewModels.Shared
{
  public class Localization : ViewModelBase
  {
    public string CultureCode { get; set; }
    public string Value { get; set; }
  }
}