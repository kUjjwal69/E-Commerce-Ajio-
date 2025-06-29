namespace E_Commerce_Project.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } // PayPal, Stripe
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
    }

}
