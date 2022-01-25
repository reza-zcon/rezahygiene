namespace OpsManagement.Domain.Countrys.Validators;

using OpsManagement.Dtos.Country;
using FluentValidation;

public class CountryForCreationDtoValidator: CountryForManipulationDtoValidator<CountryForCreationDto>
{
    public CountryForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}