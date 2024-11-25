using System;

namespace CrediBill_Web.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? Deleted { get; set; } = null;

        // Foreign key for Invoice
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}