using BookingSundorbon.Views.DTOs.DiscountedOfferDetailView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DiscountedOfferDetailRepository
{
    public interface IDiscountedOfferDetailRepository
    {
        Task<int> CreateDiscountedOfferDetailAsync(DiscountedOfferDetailView discountedOfferDetail);
        Task<DiscountedOfferDetailView> GetDiscountedOfferDetailAsync(int id);
        Task<IEnumerable<DiscountedOfferDetailView>> GetAllDiscountedOfferDetailsAsync();
        Task UpdateDiscountedOfferDetailAsync(DiscountedOfferDetailView discountedOfferDetail);
        Task DeleteDiscountedOfferDetailAsync(int id);
    }
}
