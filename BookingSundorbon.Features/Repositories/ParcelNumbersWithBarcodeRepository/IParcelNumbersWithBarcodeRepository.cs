using BookingSundorbon.Views.DTOs.ActiveParcelContentView;
using BookingSundorbon.Views.DTOs.ParcelNumbersWithBarcodeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelNumbersWithBarcodeRepository
{
    public interface IParcelNumbersWithBarcodeRepository
    {
        Task<IEnumerable<ParcelNumbersWithBarcodeView>> GetAllParcelNumbersWithBarcodes();
    }
}
