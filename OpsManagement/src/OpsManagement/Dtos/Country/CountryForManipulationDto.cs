namespace OpsManagement.Dtos.Country;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class CountryForManipulationDto 
{
   public string Name { get; set; }
   public string Code { get; set; }
}