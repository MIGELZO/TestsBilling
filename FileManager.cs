﻿using System.Collections.ObjectModel;
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

        public Customer CreateCustomer(Customer customer, Invoice invoice)
        {
            ObservableCollection<Customer> customers = GetAllCustomers();
            customers.Add(customer);

            File.WriteAllText(CustomersFilePath, JsonSerializer.Serialize(customers));
            CreateNewInvoice(invoice);
            return customer;
        }

        public Customer UpdateCustomer(int customerId, Invoice invoice)
        {
            ObservableCollection<Customer> customers = GetAllCustomers();
            Customer customer = customers.FirstOrDefault(c => c.CustomerID == customerId);
            customer.AddInvoice(invoice);
            File.WriteAllText(CustomersFilePath, JsonSerializer.Serialize(customers));
            CreateNewInvoice(invoice);
            return customer;
        }

        public ObservableCollection<Customer> GetAllCustomers()
        {
            try
            {
                string jsonContent = File.ReadAllText(CustomersFilePath);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<ObservableCollection<Customer>>(jsonContent, options) ?? new ObservableCollection<Customer>();
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Customer>();
            }
        }

        public ObservableCollection<Invoice>? GetAllInvoices()
        {
            try
            {
                string jsonContent = File.ReadAllText(InvoicesFilePath);
                return JsonSerializer.Deserialize<ObservableCollection<Invoice>>(jsonContent);
            }
            catch (Exception)
            {
                return new ObservableCollection<Invoice>();
            }
        }

        public void CreateNewInvoice(Invoice newInvoice)
        {
            ObservableCollection<Invoice>? invoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();
            invoices.Add(newInvoice);
            File.WriteAllText(InvoicesFilePath, JsonSerializer.Serialize(invoices));
        }

        public void UpdateExistingInvoice(Invoice updatedInvoice)
        {
            ObservableCollection<Invoice>? invoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();
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

        public ObservableCollection<Invoice> GetAllInvoices(int type)
        {
            ObservableCollection<Invoice> invoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();

            ObservableCollection<Invoice> filteredInvoices = new ObservableCollection<Invoice>(
                invoices.Where(i => i.InvoiceType == type).ToList()
            );

            return filteredInvoices;
        }
        public ObservableCollection<Invoice> GetAllInvoicesPerCustomer(int customerID)
        {
            ObservableCollection<Invoice> invoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();

            ObservableCollection<Invoice> filteredInvoices = new ObservableCollection<Invoice>(
                invoices.Where(i => i.CustomerID == customerID).ToList()
                );

            return filteredInvoices;
        }
        public ObservableCollection<Invoice> GetAllInvoicesPerCustomer(int customerID, int type)
        {
            ObservableCollection<Invoice> invoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();

            ObservableCollection<Invoice> filteredInvoices = new ObservableCollection<Invoice>(
                invoices.Where(i => i.CustomerID == customerID && i.InvoiceType == type).ToList()
                );

            return filteredInvoices;
        }

        public ObservableCollection<Item>? GetAllItems()
        {
            try
            {
                string jsonContent = File.ReadAllText(ItemsFilePath);
                return JsonSerializer.Deserialize<ObservableCollection<Item>>(jsonContent);
            }
            catch (Exception)
            {
                return new ObservableCollection<Item>();
            }
        }

        public int GetNextItemID()
        {
            ObservableCollection<Item>? items = GetAllItems() ?? new ObservableCollection<Item>();
            int id = items.Count > 1 ? items.Max(i => i.ItemId) + 1 : 1;
            return id;
        }

        public Item CreateItem(int id, string name, decimal price)
        {
            ObservableCollection<Item> items = GetAllItems() ?? new ObservableCollection<Item>();
            Item item = new Item(id, name, price);
            items.Add(item);
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
            return item;
        }

        public Item UpdateItem(int id, string newName, decimal newPrice)
        {
            ObservableCollection<Item> items = GetAllItems() ?? new ObservableCollection<Item>();
            Item? item = items.FirstOrDefault(i => i.ItemId == id);
            item.Name = newName;
            item.Price = newPrice;
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
            return item;
        }
        public void DeleteItem(int id)
        {
            ObservableCollection<Item> items = GetAllItems() ?? new ObservableCollection<Item>();
            Item? item = items.FirstOrDefault(i => i.ItemId == id);
            items.Remove(item);
            File.WriteAllText(ItemsFilePath, JsonSerializer.Serialize(items));
        }

        public ObservableCollection<Invoice> GetAllInvoicesByItem(int itemId)
        {
            ObservableCollection<Invoice> Allinvoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();

            ObservableCollection<Invoice> filteredInvoices = new ObservableCollection<Invoice>(
                Allinvoices.Where(i => i.Items.Keys.Contains(itemId)).ToList()
                );

            return filteredInvoices;
        }

        public ObservableCollection<Invoice> GetAllInvoicesByItem(int itemId, int customerId)
        {
            ObservableCollection<Invoice> Allinvoices = GetAllInvoices() ?? new ObservableCollection<Invoice>();

            ObservableCollection<Invoice> filteredInvoices = new ObservableCollection<Invoice>(
                Allinvoices.Where(i => i.CustomerID == customerId && i.Items.Keys.Contains(itemId)).ToList()
                );

            return filteredInvoices;
        }

        public void InitializeFromDataFile()
        {
            if (!File.Exists(CustomersFilePath))
            {
                string CustomerInitialData = "[{\"CustomerID\":0,\"FullName\":\"General Customer\",\"Email\":\"MyLittleBusiness@daniel.com\",\"Phone\":\"0500000000\",\"Invoices\":[],\"Orders\":{}}]";
                File.WriteAllText(CustomersFilePath, CustomerInitialData);
            }
            if (!File.Exists(ItemsFilePath))
            {
                string ItemsInitialData = "{\r\n  \"ItemId\": 0,\r\n  \"Name\": \"General Item\",\r\n  \"Price\": 0\r\n}";
                File.WriteAllText(ItemsFilePath, ItemsInitialData);
            }
            if (!File.Exists(CustomersFilePath))
            {
                File.WriteAllText(InvoicesFilePath, "");
            }
        }
    }
}
