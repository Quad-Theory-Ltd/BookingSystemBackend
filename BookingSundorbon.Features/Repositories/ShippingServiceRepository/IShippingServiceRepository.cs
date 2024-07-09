using BookingSundorbon.Views.DTOs.ShippingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ShippingServiceRepository
{
    public interface IShippingServiceRepository
    {
        Task<int> CreateShippingServiceAsync(CreateShippingServiceView shippingService);
    }
}
