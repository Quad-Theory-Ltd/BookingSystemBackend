using BookingSundorbon.Views.DTOs.PaymentTypeMethodView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.PaymentTypeMethodRepository
{
    public interface IPaymentTypeMethodRepository
    {
        Task<IEnumerable<PaymentTypeMethodView>> GetAllActivePaymentTypeMethodsAsync();
        //Task<int> CreatePaymentTypeMethodAsync(PaymentTypeMethodView paymentTypeMethod);
        //Task<PaymentTypeMethodView> GetPaymentTypeMethodAsync(int id);      
        //Task UpdatePaymentTypeMethodAsync(PaymentTypeMethodView PaymentTypeMethod);
        //Task DeletePaymentTypeMethodAsync(int id);
    }
}
