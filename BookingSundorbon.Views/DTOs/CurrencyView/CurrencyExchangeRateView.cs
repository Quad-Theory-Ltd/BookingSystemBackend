using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.CurrencyView
{
    public class CurrencyExchangeRateView
    {
       public int Id { get; set; }
       public string BaseCurrency { get; set; }
       public string TargetCurrency { get; set; }
       public decimal Rate { get; set; }
       public DateTime CreationDateb {  get; set; }
       public DateTime ModificationDate {  get; set; }

    }
}
