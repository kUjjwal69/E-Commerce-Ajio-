using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDetail> ProcessPayment(string paymentMethod, decimal amount);
    }
 
}
