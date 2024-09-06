using BookingSundorbon.Views.DTOs.PaymentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<int> CreatePaymentAsync(PaymentView payment);
        Task<PaymentView> GetPaymentAsync(int id);
        Task<IEnumerable<PaymentView>> GetAllPaymentsAsync();
        Task UpdatePaymentAsync(PaymentView payment);
        Task DeletePaymentAsync(int id);
        Task<PaymentView> GetPaymentAsyncByParcelNoAsync(string parcelNo);

        Task<IEnumerable<PaymentView>> GetAgentPaymentsAsync(int userId);
    }
}
