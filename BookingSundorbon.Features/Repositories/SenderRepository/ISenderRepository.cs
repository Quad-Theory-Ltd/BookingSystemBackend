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
        Task<int> CreateSenderAsync(SenderView sender);
    }
}
