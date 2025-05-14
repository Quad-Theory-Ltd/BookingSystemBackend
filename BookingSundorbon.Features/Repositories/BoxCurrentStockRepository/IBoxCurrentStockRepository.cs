using BookingSundorbon.Views.DTOs.BoxCurrentStockView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BoxCurrentStockRepository
{
    public interface IBoxCurrentStockRepository
    {
        Task<int> CreateBoxCurrentStockAsync(BoxCurrentStockView boxCurrentStock);
        Task<BoxCurrentStockView> GetBoxCurrentStockAsync(int id);
        Task<IEnumerable<BoxCurrentStockView>> GetAllBoxCurrentStocksAsync();
        Task UpdateBoxCurrentStockAsync(BoxCurrentStockView boxCurrentStock);
        Task DeleteBoxCurrentStockAsync(int id);
    }
}
