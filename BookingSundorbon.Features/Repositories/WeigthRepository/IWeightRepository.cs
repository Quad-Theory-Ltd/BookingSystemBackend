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
        Task<int> CreateWeightAsync(CreateWeigthView weigth);
    }
}
