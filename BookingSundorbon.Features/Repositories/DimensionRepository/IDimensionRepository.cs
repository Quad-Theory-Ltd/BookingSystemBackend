using BookingSundorbon.Views.DTOs.DimensionView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DimensionRepository
{
    public interface IDimensionRepository
    {
        Task<int> CreateDimensionAsync(DimensionView dimension);
        Task<DimensionView> GetDimensionAsync(int id);
        Task<IEnumerable<DimensionView>> GetAllActiveDimensionsAsync();
        Task UpdateDimensionAsync(DimensionView dimension);
        Task DeleteDimensionAsync(int id);
        Task<decimal> GetDimensionPriceByIdAsync(int id);
    }
}
