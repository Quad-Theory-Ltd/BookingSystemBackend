using BookingSundorbon.Views.DTOs.SettingsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SettingsRepository
{
    public interface ISettingsRepository
    {
        Task<GtmSettingsView> GetGtmSettingsAsync();
        Task SaveGtmSettingsAsync(SaveGtmSettingsView settings);
    }
}
