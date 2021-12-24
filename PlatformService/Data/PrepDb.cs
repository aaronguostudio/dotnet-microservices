using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app)
    {
      using(var serviceScope = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
      }
    }

    private static void SeedData(AppDbContext context)
    {
      if(!context.Platforms.Any())
      {
        Console.WriteLine("--> Seeding data");

        context.Platforms.AddRange(
          new Platform() { Name = "Dotnet", Publisher = "Microsoft", Price = "Free" },
          new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Price = "Free" },
          new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Price = "Free" }
        );

        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> Data is seeded");
      }
    }
  }
}