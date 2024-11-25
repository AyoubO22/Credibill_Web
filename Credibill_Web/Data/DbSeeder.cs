using System;
using System.Linq;
using CrediBill_Web.Models;

namespace CrediBill_Web.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            try
            {
                // Vérifie si la base est vide
                if (context.Customers.Any() || context.Invoices.Any() || context.Payments.Any())
                    return;

                var customer1 = new Customer
                {
                    Name = "John Doe",
                    CreatedDate = DateTime.Now
                };

                var customer2 = new Customer
                {
                    Name = "Jane Smith",
                    CreatedDate = DateTime.Now
                };

                context.Customers.AddRange(customer1, customer2);
                context.SaveChanges();

                var invoice1 = new Invoice
                {
                    CustomerId = customer1.Id,
                    Amount = 150.00m,
                    InvoiceDate = DateTime.Now
                };

                var invoice2 = new Invoice
                {
                    CustomerId = customer2.Id,
                    Amount = 200.00m,
                    InvoiceDate = DateTime.Now
                };

                context.Invoices.AddRange(invoice1, invoice2);
                context.SaveChanges();

                var payment1 = new Payment
                {
                    InvoiceId = invoice1.Id,
                    Amount = 150.00m,
                    PaymentDate = DateTime.Now
                };

                var payment2 = new Payment
                {
                    InvoiceId = invoice2.Id,
                    Amount = 100.00m,
                    PaymentDate = DateTime.Now
                };

                context.Payments.AddRange(payment1, payment2);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
            }
        }
    }
}
