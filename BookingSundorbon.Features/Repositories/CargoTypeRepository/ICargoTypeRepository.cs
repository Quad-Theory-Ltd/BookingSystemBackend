﻿using BookingSundorbon.Views.DTOs.CargoTypeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CargoTypeRepository
{
    public interface ICargoTypeRepository
    {
        Task<IEnumerable<ActiveCargoTypeView>> GetAllActiveCargoTypesAsync();
        Task <int> CreateCargoTypeAsync(ActiveCargoTypeView cargoType);
        Task<ActiveCargoTypeView> GetCargoTypeAsync(int id);
        Task UpdateCargoTypeAsync(ActiveCargoTypeView cargoType);
        Task DeleteCargoTypeAsync(int id);
    }
}
