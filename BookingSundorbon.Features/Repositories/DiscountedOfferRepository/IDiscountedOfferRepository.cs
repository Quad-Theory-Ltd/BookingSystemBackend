using BookingSundorbon.Views.DTOs.DiscountedOfferView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DiscountedOfferRepository
{
    public interface IDiscountedOfferRepository
    {
        Task<int> CreateDiscountedOfferAsync(DiscountedOfferView discountedOffer);
        Task<DiscountedOfferView> GetDiscountedOfferAsync(int id);
        Task<IEnumerable<DiscountedOfferView>> GetAllActiveDiscountedOffersAsync();
        Task UpdateDiscountedOfferAsync(DiscountedOfferView discountedOffer);
        Task DeleteDiscountedOfferAsync(int id);
    }
}
