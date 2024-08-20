using BookingSundorbon.Views.DTOs.SenderDetails;
using BookingSundorbon.Views.DTOs.SenderDetailsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SenderDetailsRepository
{
    public interface ISenderDetailsRepository
    {
 
        Task<PickUpAndDeliveryInfoView> GetPickupAndDeliveryPointAsync(int parcelOrderId);
        Task<IEnumerable<int>> GetAllParcelOrderNo();
    }
}
