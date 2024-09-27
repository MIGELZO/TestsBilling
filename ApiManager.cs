using System.Globalization;
using System.Net.Http;
using System.Text;

namespace kukiluli
{
    internal class ApiManager
    {
        public static HttpClient client = new HttpClient();
        private readonly string baseUrl = "https://secure.cardcom.solutions/Interface";
        private readonly string? terminalNumber = Environment.GetEnvironmentVariable("TeminalNumber", EnvironmentVariableTarget.User);
        private readonly string? username = Environment.GetEnvironmentVariable("UserName", EnvironmentVariableTarget.User);
        private readonly string? password = Environment.GetEnvironmentVariable("UserPassword", EnvironmentVariableTarget.User);

        // Method to charge a card
        public async Task<HttpResponseMessage> ChargeCard(decimal sum, string cardNumber, int cardValidityYear, int cardValidityMonth, string identityNumber, string language)
        {
            string url = $"{baseUrl}/Direct2.aspx?TerminalNumber={terminalNumber}&Sum={sum}&cardnumber={cardNumber}&cardvalidityyear={cardValidityYear}&cardvaliditymonth={cardValidityMonth}&identitynumber={identityNumber}&username={username}&Languages={language}";
            return await client.PostAsync(url, null);
        }

        // Method to charge a card and create an invoice
        public async Task<HttpResponseMessage> ChargeCardWithInvoice(decimal sum, string cardNumber, int cardValidityYear, int cardValidityMonth, string identityNumber, string customerName, string email, string itemDescription, decimal price, int quantity)
        {
            string url = $"{baseUrl}/Direct2.aspx?TerminalNumber={terminalNumber}&Sum={sum}&cardnumber={cardNumber}&cardvalidityyear={cardValidityYear}&cardvaliditymonth={cardValidityMonth}&identitynumber={identityNumber}&username={username}&Languages=he&InvoiceType=1&InvoiceHead.CustName={customerName}&InvoiceHead.SendByEmail=true&InvoiceHead.Language=he&InvoiceHead.Email={email}&InvoiceLines.Description={itemDescription}&InvoiceLines.Price={price}&InvoiceLines.Quantity={quantity}";
            return await client.PostAsync(url, null);
        }

        // Method to cancel a deal
        public async Task<HttpResponseMessage> CancelDeal(int internalDealNumber, decimal? partialSum = null)
        {
            string url;
            if (partialSum != null)
            {
                url = $"{baseUrl}/CancelDeal.aspx?TerminalNumber={terminalNumber}&name={username}&pass{password}&InternalDealNumber{internalDealNumber}&PartialSum{partialSum}";

            }
            else
            {
                url = $"{baseUrl}/CancelDeal.aspx?TerminalNumber={terminalNumber}&name={username}&pass{password}&InternalDealNumber{internalDealNumber}";
            }
            return await client.PostAsync(url, null);
        }

        // Method to create an invoice for an existing deal
        public async Task<HttpResponseMessage> CreateInvoiceForExistingDeal(string customerName, string email, string itemDescription, decimal price, int quantity, string dealNumber)
        {
            string url = $"{baseUrl}/CreateInvoice.aspx?TerminalNumber={terminalNumber}&username={username}&InvoiceType=1&InvoiceHead.CustName={customerName}&InvoiceHead.SendByEmail=true&InvoiceHead.Language=he&InvoiceHead.Email={email}&InvoiceLines.Description={itemDescription}&InvoiceLines.Price={price}&InvoiceLines.Quantity={quantity}&CreditDealNum.DealNumber={dealNumber}";
            return await client.PostAsync(url, null);
        }

        // Method to download a PDF of the document
        public async Task<HttpResponseMessage> DownloadDocumentPDF(int documentNumber, int documentType, bool isOriginal)
        {
            string url = $"{baseUrl}/GetDocumentPDF.aspx?UserName={username}&UserPassword={password}&DocumentNumber={documentNumber}&DocumentType={documentType}&IsOriginal={isOriginal}";
            return await client.GetAsync(url);
        }

        // Method to download HTML of the invoice
        public async Task<HttpResponseMessage> DownloadDocumentHTML(int invoiceNumber, int invoiceType, bool getAsOriginal)
        {
            string url = $"{baseUrl}/InvoiceGetHtml.aspx?UserName={username}&UserPassword={password}&InvoiceNumber={invoiceNumber}&InvoiceType={invoiceType}&GetAsOriginal={getAsOriginal}";
            return await client.GetAsync(url);
        }

        // Method to get invoice information
        public async Task<HttpResponseMessage> GetInvoiceInfo(int invoiceNumber, int invoiceType)
        {
            string url = $"{baseUrl}/InvoiceGetInfo.aspx?username={username}&userpassword={password}&invoiceNumber={invoiceNumber}&invoiceType={invoiceType}";
            return await client.GetAsync(url);
        }

        // Method to send an invoice copy to an email
        public async Task<HttpResponseMessage> SendInvoiceToEmail(int invoiceNumber, int invoiceType, string emailAddress)
        {
            string url = $"{baseUrl}/SendInvoiceCopy.aspx?username={username}&userpassword={password}&InvoiceNumber={invoiceNumber}&invoiceType={invoiceType}&EmailAddress={emailAddress}";
            return await client.GetAsync(url);
        }

        // Method to get customer details
        public async Task<HttpResponseMessage> GetCustomerDetails(int accountId)
        {
            string url = $"{baseUrl}/GetAccount.aspx?TerminalNumber={terminalNumber}&UserName={username}&AccountID={accountId}";
            return await client.GetAsync(url);
        }


        public bool CheckUserInformationExist()
        {
            return (username != null && password != null && terminalNumber != null);
        }

        public async Task<HttpResponseMessage> CreateNewInvoice(int invoiceType, string customerName, string email, bool sendbyemail, string language, string ItemsDescription, decimal itemPrice, int quantity, int? cashPay = null, int? cardValidityMonth = null, int? cardValidityYear = null, int? identityNum = null, int? cardNumber = null, decimal? cardSum = null, List<Cheque>? cheques = null)
        {
            string url = $"{baseUrl}/CreateInvoice.aspx?TerminalNumber={terminalNumber}&UserName={username}&InvoiceType=0&InvoiceHead.CustName{customerName}&InvoiceHead.SendByEmail={sendbyemail}&InvoiceHead.Language{language}&InvoiceLines.Description{ItemsDescription}&InvoiceLines.Price{itemPrice}&InvoiceLines.Quantity{quantity}";
            switch (invoiceType)
            {
                case 0:
                    return await client.PostAsync(url, null);
                case 1:

                    if (cashPay != null) { url += $"&cash{cashPay}"; };
                    if (cardNumber != null) { url += $"&cardnumber{cardNumber}&cardvalidityyear{cardValidityYear}&cardvaliditymonth{cardValidityMonth}&identitynumber{identityNum}&Sum{cardSum}"; };
                    if (cheques != null)
                    {
                        string listCheques = FormatChequesForRequest(cheques);
                        StringContent content = new StringContent(listCheques, Encoding.UTF8, "application/x-www-form-urlencoded");
                        return await client.PostAsync(url, content);
                    };
                    return await client.PostAsync(url, null);
                default:
                    return await client.PostAsync(url, null);
            }
        }

        public async Task<HttpResponseMessage> GetInvoiceDetails(int invoiceId, int invoiceType)
        {
            string url = $"{baseUrl}/CreateInvoice.aspx?UserPassword={password}&UserName={username}&invoiceNumber{invoiceId}&invoiceType{invoiceType}";
            return await client.GetAsync(url);
        }

        public string FormatChequesForRequest(List<Cheque> cheques)
        {
            List<string> queryString = new List<string>();
            for (int i = 0; i < cheques.Count; i++)
            {
                Cheque cheque = cheques[i];
                string prefix = i == 0 ? "Cheque" : $"Cheque{i}";

                queryString.Add($"{prefix}.ChequeNumber={cheque.ChequeNumber}");
                queryString.Add($"{prefix}.DateCheque={cheque.Date.ToString("M/d/yyyy", CultureInfo.InvariantCulture)}");
                queryString.Add($"{prefix}.Sum={cheque.Sum}");
                queryString.Add($"{prefix}.SnifNumber={cheque.SnifNumber}");
                queryString.Add($"{prefix}.AccountNumber={cheque.AccountNumber}");
            }

            return string.Join("&", queryString);
        }
    }
}
