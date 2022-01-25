namespace OpsManagement.Dtos.Currency;

using OpsManagement.Dtos.Shared;

public class CurrencyParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}