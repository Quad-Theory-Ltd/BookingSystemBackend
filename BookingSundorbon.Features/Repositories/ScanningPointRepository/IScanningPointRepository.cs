using BookingSundorbon.Views.DTOs.ScanningPointView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScanningPointRepository
{
    public interface IScanningPointRepository
    {
        Task<int> CreateScanningPointAsync(ScanningPointView scanningPoint);
        Task<ScanningPointView> GetScanningPointAsync(int id);
        Task<IEnumerable<ScanningPointView>> GetAllActiveScanningPointsAsync();
        Task UpdateScanningPointAsync(ScanningPointView scanningPoint);
        Task DeleteScanningPointAsync(int id);
    }
}
