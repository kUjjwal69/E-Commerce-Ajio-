using System;
using System.Collections.Generic;

namespace E_Commerce_Project.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Who placed the order
        public int UserId { get; set; }
        public User User { get; set; }

        // When order was placed
        public DateTime OrderDate { get; set; }

        // Order status - Pending, Placed, Shipped, Delivered, Cancelled
        public string Status { get; set; }

        // Shipping address of the user
        public Address ShippingAddress { get; set; }

        // Payment info like method, transactionId etc.
        public PaymentDetail PaymentDetail { get; set; }

        // List of all items in the order
        public ICollection<OrderItem> OrderItems { get; set; }

        // Total price (calculated from order items)
        public decimal TotalAmount { get; set; }
    }
}
