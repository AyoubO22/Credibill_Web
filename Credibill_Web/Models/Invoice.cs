using System;
using System.Collections.Generic;
using Credibill_Web.Models;

namespace CrediBill_Web.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? Deleted { get; set; } = null;

        // Foreign key for Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Navigation property for related payments
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}