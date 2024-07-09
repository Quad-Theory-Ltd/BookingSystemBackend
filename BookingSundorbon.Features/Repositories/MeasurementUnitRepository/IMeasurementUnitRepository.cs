using BookingSundorbon.Views.DTOs.MeasurementUnitView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.MeasurementUnitRepository
{
    public interface IMeasurementUnitRepository
    {
        Task<int> CreateMeasurementUnitAsync(CreateMeasurementUnitView measurementUnit);
    }
}
