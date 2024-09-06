using BookingSundorbon.Views.DTOs.ReceiveView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ReceiveRepository
{
    public interface IReceiveRepository
    {
        Task<int> CreateReceiveAsync(ReceiveView receive);
        Task<ReceiveView> GetReceiveAsync(int receiveNo);
        Task<IEnumerable<ReceiveView>> GetAllReceiveAsync();
        Task<string> GetNextReceiveNoAsync();
        Task<IEnumerable<ReceiveView>> GetReceivesByUserIdAsync(int userId);
        //Task UpdateReceiveAsync(ReceiveView receive);
        //Task DeleteReceiveAsync(int id);
    }
}
