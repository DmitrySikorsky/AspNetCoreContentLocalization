// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using AspNetCoreContentLocalization.Data;
using AspNetCoreContentLocalization.Data.Repositories.Abstractions;
using AspNetCoreContentLocalization.Data.Repositories.Defaults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreContentLocalization
{
  public class Startup
  {
    private IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<Storage>(
        options => options.UseSqlite(this.configuration.GetConnectionString("DefaultConnection"))
      );

      services.AddScoped<ICultureRepository, CultureRepository>();
      services.AddScoped<ILocalizationSetRepository, LocalizationSetRepository>();
      services.AddScoped<ILocalizationRepository, LocalizationRepository>();
      services.AddScoped<IBookRepository, BookRepository>();
      services.AddScoped<ILocalizedBookRepository, LocalizedBookRepository>();
      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();
    }

    public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
    {
      if (hostingEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
      }

      CultureInfo[] supportedCultures = new [] { new CultureInfo("en"), new CultureInfo("uk") };

      applicationBuilder.UseRequestLocalization(
        new RequestLocalizationOptions()
        {
          DefaultRequestCulture = new RequestCulture("en"),
          SupportedCultures = supportedCultures,
          SupportedUICultures = supportedCultures
        }
      );

      applicationBuilder.UseStaticFiles();
      applicationBuilder.UseMvc(routeBuilder =>
        {
          routeBuilder.MapRoute("Create", "{controller=Books}/add", new { action = "AddOrEdit" });
          routeBuilder.MapRoute("Update", "{controller=Books}/edit/{id?}", new { action = "AddOrEdit" });
          routeBuilder.MapRoute("Default", "{controller=Books}/{action=Index}/{id?}");
        }
      );
    }
  }
}
