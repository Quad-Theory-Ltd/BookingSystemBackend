using BookingSundorbon.Views.DTOs.CurrentStockCurierServiceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CurrentStockCurierServiceRepository
{
    public interface ICurrentStockCurierServiceRepository
    {
        Task<int> CreateCurrentStockCurierServiceAsync(CurrentStockCurierServiceView currentStockCurierService);
        Task<CurrentStockCurierServiceView> GetCurrentStockCurierServiceAsync(int id);
        Task<IEnumerable<CurrentStockCurierServiceView>> GetAllCurrentStockCurierServicesAsync();
        Task UpdateCurrentStockCurierServiceAsync(CurrentStockCurierServiceView currentStockCurierService);
        Task DeleteCurrentStockCurierServiceAsync(int id);
    }
}
