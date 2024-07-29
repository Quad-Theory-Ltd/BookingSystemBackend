using BookingSundorbon.Views.DTOs.BranchView;
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
        Task<int> CreateMeasurementUnitAsync(MeasurementUnitView measurementUnit);
       Task<MeasurementUnitView> GetMeasurementUnitAsync(int id);
        Task<IEnumerable<MeasurementUnitView>> GetAllActiveMeasurementUnitsAsync();
        Task UpdateMeasurementUnitAsync(MeasurementUnitView measurementUnit);
        Task DeleteMeasurementUnitAsync(int id);
    }
}
