using BookingSundorbon.Views.DTOs.ReceiverView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ReceiverRepository
{
    public interface IReceiverRepository
    {
        Task<long> CreateReceiverAsync(ReceiverView receiver);
        Task<ReceiverView> GetReceiverAsync(long id);
    }
}
