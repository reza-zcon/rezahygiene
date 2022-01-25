namespace OpsManagement.Dtos.Currency;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class CurrencyForManipulationDto 
{
   public string Name { get; set; }
   public string Code { get; set; }
}