namespace OpsManagement.Domain.Currencys.Validators;

using OpsManagement.Dtos.Currency;
using FluentValidation;

public class CurrencyForCreationDtoValidator: CurrencyForManipulationDtoValidator<CurrencyForCreationDto>
{
    public CurrencyForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}