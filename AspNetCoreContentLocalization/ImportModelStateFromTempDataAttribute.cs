// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AspNetCoreContentLocalization
{
  public class ImportModelStateFromTempDataAttribute : ModelStateTempDataTransferAttribute
  {
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      base.OnActionExecuted(filterContext);

      Controller controller = filterContext.Controller as Controller;

      if (controller.TempData.ContainsKey(ModelStateTempDataTransferAttribute.Key))
      {
        IEnumerable<ModelState> modelStates = JsonConvert.DeserializeObject<IEnumerable<ModelState>>(controller.TempData[ModelStateTempDataTransferAttribute.Key] as string);

        if (filterContext.Result is ViewResult)
        {
          foreach (ModelState modelState in modelStates)
          {
            controller.ViewData.ModelState.SetModelValue(modelState.Key, modelState.Value, modelState.Value);
            controller.ViewData.ModelState[modelState.Key].ValidationState = modelState.ValidationState;

            if (modelState.ValidationState == ModelValidationState.Invalid)
              foreach (string error in modelState.Errors)
                controller.ViewData.ModelState[modelState.Key].Errors.Add(new ModelError(error));
          }
        }

        else controller.TempData.Remove(Key);
      }
    }
  }
}