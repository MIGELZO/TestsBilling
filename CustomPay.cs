namespace kukiluli
{
    class CustomPay
    {
        public int TransactionID { get; set; }
        public DateTime TranDate { get; set; }
        public string Description { get; set; }
        public string Asmacta { get; set; }
        public decimal Sum { get; set; }

        public CustomPay(int transactionID, decimal sum)
        {
            TransactionID = transactionID;
            TranDate = DateTime.Now;
            Description = transactionID == 28 ? "Payment from Bit" : "Bank transfer";
            Asmacta = DateTime.Now.ToString();
            Sum = sum;
        }
    }
}
