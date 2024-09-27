namespace kukiluli
{
    internal class Invoice
    {
        public int InvoiceID { get; set; }

        // 0 = order / 1 = invoice
        public int InvoiceType { get; set; }
        public int CustomerID { get; set; }

        // <itemID, quantity> 
        public Dictionary<int, int> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public bool IsRefunded { get; set; }
        public string DealNumber { get; set; }

        public Invoice(int invoiceID, int invoiceType, int customerID, Dictionary<int, int> items, decimal totalPrice)
        {
            InvoiceID = invoiceID;
            InvoiceType = invoiceType;
            CustomerID = customerID;
            Items = items;
            TotalPrice = totalPrice;
            IsRefunded = false;
            Date = DateTime.Now;
            DealNumber = "";
        }

        public void UpdateDealNumber(string dealNumber)
        {
            DealNumber = dealNumber;
        }

        public void RefundInvoice()
        {
            IsRefunded = true;
        }
    }
}
