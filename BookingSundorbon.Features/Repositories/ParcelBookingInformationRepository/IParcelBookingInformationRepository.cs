using BookingSundorbon.Views.DTOs.ParcelBookingInformationView;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository
{
    public interface IParcelBookingInformationRepository
    {
        Task<IEnumerable<ParcelInfoByUserIdView>> GetParcelInfoByUserIdAsync(string userId);
    }
}
