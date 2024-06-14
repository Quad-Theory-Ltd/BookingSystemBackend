using BookingSundorbon.Views.DTOs.ParcelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelRepository
{
    public interface IParcelRepository
    {
        Task<long> CreateParcelAsync(ParcelView parcel);
        Task<ParcelView> GetParcelAsync(long id);
    }
}
