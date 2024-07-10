using BookingSundorbon.Views.DTOs.ShippingServiceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ShippingServiceRepository
{
    public interface IShippingServiceRepository
    {
        Task<int> CreateShippingServiceAsync(ShippingServiceView shippingService);
        Task<ShippingServiceView> GetShippingServiceAsync(int id);
        Task<IEnumerable<ShippingServiceView>> GetAllActiveShippingServiceesAsync();
        Task UpdateShippingServiceAsync(ShippingServiceView shippingService);
        Task DeleteShippingServiceAsync(int id);
    }
}
