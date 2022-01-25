namespace OpsManagement.Domain.Countrys.Validators;

using OpsManagement.Dtos.Country;
using FluentValidation;

public class CountryForUpdateDtoValidator: CountryForManipulationDtoValidator<CountryForUpdateDto>
{
    public CountryForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}