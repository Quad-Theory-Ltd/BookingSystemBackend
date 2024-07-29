using BookingSundorbon.Views.DTOs.DeviceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DeviceRepository
{
    public interface IDeviceRepository
    {
        Task<int> CreateDeviceAsync(DeviceView device);
        Task<DeviceView> GetDeviceAsync(int id);
        Task<IEnumerable<DeviceView>> GetAllActiveDevicesAsync();
        Task UpdateDeviceAsync(DeviceView device);
        Task DeleteDeviceAsync(int id);
    }
}
