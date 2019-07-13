﻿// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCoreContentLocalization.ViewModels.Shared
{
  public class Book : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
  }
}