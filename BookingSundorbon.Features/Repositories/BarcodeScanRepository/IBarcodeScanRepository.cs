using BookingSundorbon.Views.DTOs.BarcodeScanView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeScanRepository
{
    public interface IBarcodeScanRepository
    {
        Task<BarCodeGetView> CreateBarcodeScanAsync(BarcodeScanView barcodeScan);
        Task<BarcodeScanView> GetBarcodeScanAsync(int id);
        Task<IEnumerable<BarcodeScanView>> GetAgentBarcodeScanAsync(string userId);
        Task<IEnumerable<BarcodeScanView>> GetAllActiveBarcodeScansAsync();
        //Task UpdateBarcodeScanAsync(BarcodeScanView barcodeScan);
        //Task DeleteBarcodeScanAsync(int id);
    }
}
