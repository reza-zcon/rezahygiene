namespace OpsManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class Currencys
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/currencys";
        public const string GetRecord = $"{Base}/currencys/{Id}";
        public const string Create = $"{Base}/currencys";
        public const string Delete = $"{Base}/currencys/{Id}";
        public const string Put = $"{Base}/currencys/{Id}";
        public const string Patch = $"{Base}/currencys/{Id}";
    }

public static class Countrys
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/countrys";
        public const string GetRecord = $"{Base}/countrys/{Id}";
        public const string Create = $"{Base}/countrys";
        public const string Delete = $"{Base}/countrys/{Id}";
        public const string Put = $"{Base}/countrys/{Id}";
        public const string Patch = $"{Base}/countrys/{Id}";
    }
}
