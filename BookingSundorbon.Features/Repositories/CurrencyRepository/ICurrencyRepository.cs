using BookingSundorbon.Views.DTOs.CurrencyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CurrencyRepository
{
    public interface ICurrencyRepository
    {

        Task<IEnumerable<CurrencyView>> GetAllActiveCurrencyAsync();

    }
}
