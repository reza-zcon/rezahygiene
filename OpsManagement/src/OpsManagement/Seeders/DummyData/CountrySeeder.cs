namespace OpsManagement.Seeders.DummyData;

using AutoBogus;
using OpsManagement.Domain.Countrys;
using OpsManagement.Databases;
using System.Linq;

public static class CountrySeeder
{
    public static void SeedSampleCountryData(OpsDbContext context)
    {
        if (!context.Countrys.Any())
        {
            context.Countrys.Add(new AutoFaker<Country>());
            context.Countrys.Add(new AutoFaker<Country>());
            context.Countrys.Add(new AutoFaker<Country>());

            context.SaveChanges();
        }
    }
}