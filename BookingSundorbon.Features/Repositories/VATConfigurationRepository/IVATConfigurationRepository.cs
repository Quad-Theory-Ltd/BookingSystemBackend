using BookingSundorbon.Views.DTOs.VATConfigurationView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.VATConfigurationRepository
{
    public interface IVATConfigurationRepository
    {
        Task<int> CreateVatConfigurationAsync(CreateVATConfigurationView vatConfiguration);
    }
}
