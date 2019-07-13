// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspNetCoreContentLocalization.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreContentLocalization.TagHelpers
{
  [HtmlTargetElement("localizable-input", Attributes = ForAttributeName + "," + LocalizationsAttributeName)]
  public class LocalizableInput : TagHelper
  {
    private const string ForAttributeName = "asp-for";
    private const string LocalizationsAttributeName = "asp-localizations";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression For { get; set; }

    [HtmlAttributeName(LocalizationsAttributeName)]
    public IEnumerable<Localization> Localizations { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();

      foreach (Localization localization in this.Localizations)
      {        
        output.Content.AppendHtml(this.CreateInputTag(localization));
        output.Content.AppendHtml(" ");
        output.Content.AppendHtml(this.CreateCultureCodeTag(localization.CultureCode));
        output.Content.AppendHtml(this.CreateBrTag());
      }
    }

    protected string GetIdentity(Localization localization)
    {
      return this.For.Name + localization.CultureCode;
    }

    protected string GetValue(Localization localization)
    {
      ModelStateEntry modelState;

      if (this.ViewContext.ModelState.TryGetValue(this.GetIdentity(localization), out modelState) && !string.IsNullOrEmpty(modelState.AttemptedValue))
        return modelState.AttemptedValue;

      return localization.Value;
    }

    protected bool IsValid(Localization localization)
    {
      ModelStateEntry modelState;

      if (this.ViewContext.ModelState.TryGetValue(this.GetIdentity(localization), out modelState))
        return modelState.ValidationState != ModelValidationState.Invalid;

      return true;
    }

    protected RequiredAttribute GetRequiredAttribute()
    {
      return this.For.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is RequiredAttribute) as RequiredAttribute;
    }

    protected TagBuilder CreateInputTag(Localization localization)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.TagRenderMode = TagRenderMode.SelfClosing;

      if (!this.IsValid(localization))
        tb.AddCssClass("input-validation-error");

      tb.AddCssClass("field__input");
      tb.AddCssClass("input");
      tb.MergeAttribute("id", this.GetIdentity(localization));
      tb.MergeAttribute("name", this.GetIdentity(localization));

      string value = this.GetValue(localization);

      if (!string.IsNullOrEmpty(value))
        tb.MergeAttribute("value", value);

      RequiredAttribute requiredAttribute = this.GetRequiredAttribute();

      if (requiredAttribute != null)
      {
        tb.MergeAttribute("data-val", true.ToString().ToLower());
        tb.MergeAttribute("data-val-required", string.Empty);
      }

      return tb;
    }

    protected TagBuilder CreateCultureCodeTag(string cultureCode)
    {
      TagBuilder tb = new TagBuilder("span");

      tb.InnerHtml.Append($"({cultureCode})");
      return tb;
    }

    protected TagBuilder CreateBrTag()
    {
      TagBuilder tb = new TagBuilder("br");

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      return tb;
    }
  }
}