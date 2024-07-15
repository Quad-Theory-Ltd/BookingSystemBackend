using BookingSundorbon.Views.DTOs.BarcodeStatusView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeStatusRepository
{
    public interface IBarcodeStatusRepository
    {
        Task<int> CreateBarcodeStatusAsync(BarcodeStatusView barcodeStatus);
        Task<BarcodeStatusView> GetBarcodeStatusAsync(int id);
        Task<IEnumerable<BarcodeStatusView>> GetAllActiveBarcodeStatusAsync();
        Task UpdateBarcodeStatusAsync(BarcodeStatusView barcodeStatus);
        Task DeleteBarcodeStatusAsync(int id);
    }
}
