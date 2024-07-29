using BookingSundorbon.Views.DTOs.ScanningPersonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScanningPersonRepository
{
    public interface IScanningPersonRepository
    {
        Task<int> CreateScanningPersonAsync(ScanningPersonView scanningPerson);
        Task<ScanningPersonView> GetScanningPersonAsync(int id);
        Task<IEnumerable<ScanningPersonView>> GetAllActiveScanningPersonsAsync();
        Task UpdateScanningPersonAsync(ScanningPersonView scanningPerson);
        Task DeleteScanningPersonAsync(int id);
    }
}
