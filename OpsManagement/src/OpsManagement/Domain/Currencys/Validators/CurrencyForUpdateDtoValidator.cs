namespace OpsManagement.Domain.Currencys.Validators;

using OpsManagement.Dtos.Currency;
using FluentValidation;

public class CurrencyForUpdateDtoValidator: CurrencyForManipulationDtoValidator<CurrencyForUpdateDto>
{
    public CurrencyForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}