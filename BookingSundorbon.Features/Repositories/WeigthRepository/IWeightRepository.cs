using BookingSundorbon.Views.DTOs.WeightView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.WeigthRepository
{
    public interface IWeightRepository
    {
        Task<IEnumerable<WeightView>> GetAllActiveWeightsAsync();
        Task<int> CreateWeightAsync(WeightView weigth);
        Task<WeightView> GetWeightAsync(int id);
        Task UpdateWeightAsync(WeightView weight);
        Task DeleteWeightAsync(int id);
    }
}
