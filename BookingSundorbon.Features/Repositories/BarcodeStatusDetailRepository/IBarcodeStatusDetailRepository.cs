using BookingSundorbon.Views.DTOs.BarcodeStatusDetailView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeStatusDetailRepository
{
    public interface IBarcodeStatusDetailRepository
    {
        Task<int> CreateBarcodeStatusDetailAsync(BarcodeStatusDetailView barcodeStatusDetail);
        Task<BarcodeStatusDetailView> GetBarcodeStatusDetailAsync(int id);
        Task<IEnumerable<BarcodeStatusDetailView>> GetAllActiveBarcodeStatusDetailAsync();
        Task UpdateBarcodeStatusDetailAsync(BarcodeStatusDetailView barcodeStatusDetail);
        Task DeleteBarcodeStatusDetailAsync(int id);
    }
}
