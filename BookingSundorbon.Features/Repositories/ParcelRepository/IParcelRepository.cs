using BookingSundorbon.Features.Services.EmailService;
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
        //Task<int> CreateParcelAsync(ParcelView parcel);
        //Task<ParcelView> GetParcelAsync(int id);
        //Task<IEnumerable<ParcelView>> GetAllActiveParcelsAsync();
        //Task UpdateParcelAsync(ParcelView parcel);
        //Task DeleteParcelAsync(int id);
        Task <IEnumerable<int>> GetAllParcelNoAsync();
        Task<ParcelForBarcodeScanView> GetParcelInfoByIdAsync(int id);
        Task<IEnumerable<ScanningPersonView>> GetAllScanningPersonsAsync();
        Task<string> GetLastParcelRecordSerialNoAsync();
    }
}
