namespace OpsManagement.Dtos.Country;

using OpsManagement.Dtos.Shared;

public class CountryParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}