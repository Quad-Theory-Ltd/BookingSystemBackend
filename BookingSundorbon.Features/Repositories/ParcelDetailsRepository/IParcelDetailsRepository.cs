using BookingSundorbon.Views.DTOs.ParcelDetailsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelDetailsRepository
{
    public interface IParcelDetailsRepository
    {

        Task<ParcelDetailsForPaymentView> GetParcelDetailsByParcelNoAsync(int parcelNo);
  
    }
}
