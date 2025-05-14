using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.CurrencyView
{
    public class CurrencyView
    {

      public int Id { get; set; }
      public string CurrencyType {  get; set; }
      public string Symbol {  get; set; }
      public bool IsActive {  get; set; }
      public int CreatorId {  get; set; }
      public DateTime CreationDate {  get; set; }
      public int ModifierId { get; set; }
      public DateTime ModificationDate {  get; set; }


    }
}
