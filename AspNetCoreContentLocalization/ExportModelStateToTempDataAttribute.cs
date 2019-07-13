// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AspNetCoreContentLocalization
{
  public class ExportModelStateToTempDataAttribute : ModelStateTempDataTransferAttribute
  {
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      base.OnActionExecuted(filterContext);

      Controller controller = filterContext.Controller as Controller;

      if (!controller.ViewData.ModelState.IsValid)
        controller.TempData[ModelStateTempDataTransferAttribute.Key] =
          JsonConvert.SerializeObject(
            controller.ViewData.ModelState.Select(
              ms => new ModelState()
              {
                Key = ms.Key,
                Value = ms.Value.AttemptedValue,
                ValidationState = ms.Value.ValidationState,
                Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
              }
            )
          );
    }
  }
}