using BookingSundorbon.Views.DTOs.ParcelStatusView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelStatusRepository
{
    public interface IParcelStatusRepository
    {
        Task<int> CreateParcelStatusAsync(ParcelStatusView parcelStatus);
        Task<ParcelStatusView> GetParcelStatusAsync(int id);
        Task<IEnumerable<ParcelStatusView>> GetAllActiveParcelStatusAsync();
        Task UpdateParcelStatusAsync(ParcelStatusView parcelStatus);
        //Task DeleteParcelStatusAsync(int id);
    }
}
