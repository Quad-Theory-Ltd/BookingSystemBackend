using BookingSundorbon.Views.DTOs.ParcelBookingHistoryView;
using BookingSundorbon.Views.DTOs.ParcelBookingInformationView;
using BookingSundorbon.Views.DTOs.ParcelBoxCountView;
using BookingSundorbon.Views.DTOs.ParcelCountView;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository
{
    public interface IParcelBookingInformationRepository
    {
        Task<IEnumerable<ParcelBookingHistoryView>> GetParcelInfoByUserIdAsync(int userId);
        Task<IEnumerable<ParcelCountView>> GetParcelCounts();
        Task<IEnumerable<ParcelBoxCountView>> GetParcelCountsWithDimensions();
        Task<IEnumerable<ParcelBookingHistoryView>> GetParcelBookingHistory();
        Task<IEnumerable<ParcelBookingHistoryView>> GetParcelAgentBookingHistory();
        Task<IEnumerable<ParcelBookingHistoryView>> GetParcelAgentBookingHistoryByAgentId(int AgentId);
    }
}
