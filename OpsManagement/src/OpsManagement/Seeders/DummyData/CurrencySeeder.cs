namespace OpsManagement.Seeders.DummyData;

using AutoBogus;
using OpsManagement.Domain.Currencys;
using OpsManagement.Databases;
using System.Linq;

public static class CurrencySeeder
{
    public static void SeedSampleCurrencyData(OpsDbContext context)
    {
        if (!context.Currencys.Any())
        {
            context.Currencys.Add(new AutoFaker<Currency>());
            context.Currencys.Add(new AutoFaker<Currency>());
            context.Currencys.Add(new AutoFaker<Currency>());

            context.SaveChanges();
        }
    }
}