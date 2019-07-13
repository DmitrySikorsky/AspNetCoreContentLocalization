// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreContentLocalization
{
  public class ModelState
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public ModelValidationState ValidationState { get; set; }
    public IEnumerable<string> Errors { get; set; }
  }
}