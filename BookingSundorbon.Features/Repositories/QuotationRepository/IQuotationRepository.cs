using BookingSundorbon.Views.DTOs.QuotationView;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.QuotationRepository
{
    public interface IQuotationRepository
    {
        Task<GetPricingResponseView> GetPricingAsync(GetPricingView getPricingView);
        Task<GetPricingResponseView> GetMangoPricingAsync(MangoPricingView mangoPricingView);
    }
}

