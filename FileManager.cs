using System.IO;
using System.Text.Json;

namespace kukiluli
{
    internal class FileManager
    {
        private readonly string CustomersFilePath = "customers.json";
        private readonly string InvoicesFilePath = "Invoices.json";
        private readonly string ItemsFilePath = "Items.json";

        public FileManager()
        {
            InitializeFromDataFile();
        }
        public Customer CreateCustomer(int customerID, string customerName, string customerEmail, Invoice invoice)
        {
            List<Customer>? customers = GetAllCustomers() ?? new List<Customer>();
            Customer customer = new Customer(customerID, customerName, customerEmail, invoice);
            customers.Add(customer);
            File.WriteAllText(CustomersFilePath, JsonSerializer.Serialize(customers));
            CreateNewInvoice(invoice);
            return customer;
        }

        public Customer UpdateCustomer(int customerId, Invoice invoice)
        {
            List<Customer> customers = GetAllCustomers() ?? new List<Customer>();
            Customer? customer = customers.FirstOrDefault(c => c.CustomerID == customerId);
            customer.AddInvoice(invoice);
            File.WriteAllText(CustomersFilePath, JsonSerializer.Serialize(customers));
            CreateNewInvoice(invoice);
            return customer;

        }

        public List<Customer>? GetAllCustomers()
        {
            try
            {
                string jsonContent = File.ReadAllText(CustomersFilePath);
                return JsonSerializer.Deserialize<List<Customer>>(jsonContent);
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public List<Invoice>? GetAllInvoices()
        {
            try
            {
                string jsonContent = File.ReadAllText(InvoicesFilePath);
                return JsonSerializer.Deserialize<List<Invoice>>(jsonContent);
            }
            catch (Exception)
            {
                return new List<Invoice>();
            }
        }

        public void CreateNewInvoice(Invoice newInvoice)
        {
            List<Invoice>? invoices = GetAllInvoices() ?? new List<Invoice>();
            invoices.Add(newInvoice);
            File.WriteAllText(InvoicesFilePath, JsonSerializer.Serialize(invoices));
        }

        public void UpdateExistingInvoice(Invoice updatedInvoice)
        {
            List<Invoice>? invoices = GetAllInvoices() ?? new List<Invoice>();
            Invoice? existingInvoice = invoices.FirstOrDefault(inv => inv.InvoiceID == updatedInvoice.InvoiceID);

            if (existingInvoice != null)
            {
                int index = invoices.IndexOf(existingInvoice);
                invoices[index] = updatedInvoice;
                File.WriteAllText(InvoicesFilePath, JsonSerializer.Serialize(invoices));
            }
            else
            {
                throw new Exception("Invoice not found");
            }
        }

        public List<Invoice> GetAllInvoices(int type)
        {
            List<Invoice> invoices = GetAllInvoices() ?? new List<Invoice>();
            return invoices.Where(i => i.InvoiceType == type).ToList();
        }

        public List<Invoice> GetAllInvoicesPerCustomer(int customerID)
        {
            List<Invoice> invoices = GetAllInvoices() ?? new List<Invoice>();
            return invoices.Where(i => i.CustomerID == customerID).ToList();
        }

        public List<Invoice> GetAllInvoicesPerCustomer(int customerID, int type)
        {
            List<Invoice> invoices = GetAllInvoices() ?? new List<Invoice>();
            return invoices.Where(i => i.CustomerID == customerID && i.InvoiceType == type).ToList();
        }

        public List<Item>? GetAllItems()
        {
            try
            {
                string jsonContent = File.ReadAllText(ItemsFilePath);
                return JsonSerializer.Deserialize<List<Item>>(jsonContent);
            }
            catch (Exception)
            {
                return new List<Item>();
            }
        }

        // להוסיף item general - לטקסט חופשי
        public Item CreateItem(string name, decimal price)
        {
            List<Item> items = GetAllItems() ?? new List<Item>();
            int id = items.Count > 1 ? items.Max(i => i.ItemId) + 1 : 1;
            Item item = new Item(id, name, price);
            items.Add(item);
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
            return item;
        }

        public Item UpdateItem(int id, string newName, decimal newPrice)
        {
            List<Item> items = GetAllItems() ?? new List<Item>();
            Item? item = items.FirstOrDefault(i => i.ItemId == id);
            item.Name = newName;
            item.Price = newPrice;
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
            return item;
        }
        public void DeleteItem(int id)
        {
            List<Item> items = GetAllItems() ?? new List<Item>();
            Item? item = items.FirstOrDefault(i => i.ItemId == id);
            items.Remove(item);
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
        }

        public List<Invoice> GetAllInvoicesByItem(int itemId)
        {
            List<Invoice> Allinvoices = GetAllInvoices() ?? new List<Invoice>();
            return Allinvoices.Where(i => i.Items.Keys.Contains(itemId)).ToList();
        }

        public List<Invoice> GetAllInvoicesByItem(int itemId, int customerId)
        {
            List<Invoice> Allinvoices = GetAllInvoices() ?? new List<Invoice>();
            return Allinvoices.Where(i => i.CustomerID == customerId && i.Items.Keys.Contains(itemId)).ToList();
        }

        public void InitializeFromDataFile()
        {
            if (!File.Exists(ItemsFilePath))
            {
                string ItemsInitialData = "{\r\n  \"ItemId\": 0,\r\n  \"Name\": \"General Item\",\r\n  \"Price\": 0\r\n}";
                string CustomerInitialData = "{\r\n  \"ItemId\": 0,\r\n  \"Name\": \"General Customers\",\r\n  \"Price\": 0\r\n}";
                File.WriteAllText(ItemsFilePath, ItemsInitialData);
                File.WriteAllText(CustomersFilePath, CustomerInitialData);
                File.WriteAllText(InvoicesFilePath, "");
            }
        }
    }
}
