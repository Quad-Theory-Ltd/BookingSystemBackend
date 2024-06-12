using BookingSundorbon.Views.DTOs.CompanyView;
using BookingSundorbon.Views.DTOs.SenderView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SenderRepository
{
    public interface ISenderRepository
    {
        Task<long> CreateSenderAsync(SenderView sender);
        Task<SenderView> GetSenderAsync(long id);
    }
}
