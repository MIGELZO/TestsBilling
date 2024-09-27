namespace kukiluli
{
    internal class Customer
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<int> Invoices { get; set; }
        public Dictionary<int, bool> Orders { get; set; }

        public Customer(int customerID, string fullName, string email, Invoice invoice)
        {
            CustomerID = customerID;
            FullName = fullName;
            Email = email;
            Orders = new Dictionary<int, bool>();
            Invoices = new List<int>();
            AddInvoice(invoice);
        }

        public void AddInvoice(Invoice invoice)
        {
            if (invoice.InvoiceType == 0)
            {
                Orders.Add(invoice.InvoiceID, false);
            }
            else
            {
                Invoices.Add(invoice.InvoiceID);
            }
        }

        public void ApproveOder(int id)
        {
            Orders[id] = true;
        }
    }
}
