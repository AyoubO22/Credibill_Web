using System;
using System.Collections.Generic;
using Credibill_Web.Models;

namespace CrediBill_Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? Deleted { get; set; } = null;

        // Navigation property for related invoices
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}