using BookingSundorbon.Views.DTOs.InquiryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.InquiryRepository
{
    public interface IInquiryRepository
    {
        Task InsertInquiryAsync(InquiryView inquiry);
        Task<InquiryView> GetInquiryByIdAsync(int id);
        Task<IEnumerable<InquiryView>> GetInquiriesAsync(string status = null, int page = 1, int pageSize = 20);
        Task UpdateInquiryAsync(InquiryView inquiry);
    }
}
