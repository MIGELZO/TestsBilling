using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace kukiluli
{
    public partial class MainWindow : Window
    {
        ApiManager AM = new ApiManager();
        FileManager FM = new FileManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void SendInvoiceButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //Validate Invoice 


        //    // get invoice type 
        //    int invoiceType = IsCharge.IsChecked == true ? 1 : 2;


        //    int sum = 0;
        //    List<Item> thisItems = new List<Item>();
        //    List<Cheque> thisCheques = new List<Cheque>();
        //    List<CustomPay> thisCustomPays = new List<CustomPay>();

        //    foreach (childInList in TableItemList)
        //    {
        //        thisItems.Add(new Item(childInList.ID.Text, childInList.Name.Text, childInList.Price.Text, childInList.Quan.Text));
        //        sum += int.Parse(childInList.TotalPrice.Text);
        //    };
        //    foreach (childInList in TableChequesList)
        //    {
        //        thisCheques.Add(new Item(childInList.cNumber.Text, childInList.bNumber.Text, childInList.sNumber.Text, childInList.cDate.Text, childInList.cSum.Text));
        //    };
        //    if (BitPay != 0)
        //    {
        //        thisCustomPays.Add(new CustomPay(int.Parse(BitPay.TID.text), decimal.Parse(BitPay.TSUM.Text)));
        //    };
        //    if (BankPay != 0)
        //    {
        //        thisCustomPays.Add(new CustomPay(int.Parse(BankPay.TID.text), decimal.Parse(BankPay.TSUM.Text)));
        //    };

        //    string response = AM.CreateNewInvoice(
        //        invoiceType: invoiceType,
        //        customerName: CustomerName.Text,
        //        email: CustomerEmail.Text,
        //        userMobile: CustomerPhone.Text,
        //        sendByEmail: true,
        //        language: "EN",
        //        items: thisItems,
        //        cardValidityMonth: int.Parse(CreditCardExpirationDateMonth.Text),
        //        cardValidityYear: int.Parse(CreditCardExpirationDateYear.Text),
        //        identityNum: long.Parse(CreditCardOwnerID.Text),
        //        cardNumber: long.Parse(CreditCardNumber.Text),
        //        paymentsAmount: int.Parse(Payments.Text),
        //        cardSum: sum,
        //        securityCVV: int.Parse(CreditCardSecurityDigits.Text),
        //        cheques: thisCheques,
        //        customPays: thisCustomPays
        //    );
        //    Dictionary<string, string> responseValues = AM.ParseResponseOfChargeInvoice(response);
        //    Dictionary<int, int> ItemsToSend = new Dictionary<int, int>()
        // {
        //     { int.Parse(ItemID.Text), int.Parse(ItemQuantity.Text) }
        // };
        //    Invoice thisInvoice = new Invoice(int.Parse(responseValues["InvoiceID"]), 1, int.Parse(responseValues["CustomerID"]), ItemsToSend, sum, responseValues["DealNumber"]);
        //    List<Customer> customers = FM.GetAllCustomers();
        //    Customer thisCustomer = customers.Where(c => c.CustomerID == int.Parse(responseValues["CustomerID"])).FirstOrDefault();
        //    if (thisCustomer != null)
        //    {
        //        FM.UpdateCustomer(int.Parse(responseValues["CustomerID"]), thisInvoice);
        //    }
        //    else
        //    {
        //        FM.CreateCustomer(int.Parse(responseValues["CustomerID"]), CustomerName.Text, CustomerEmail.Text, CustomerPhone.Text, thisInvoice);
        //    }

        //    List<Item> AllItems = FM.GetAllItems();
        //    foreach (Item I in thisItems)
        //    {
        //        bool itemExists = AllItems.Any(a => a.ItemId == I.ItemId);

        //        if (itemExists)
        //        {
        //            FM.UpdateItem(I.ItemId, I.Name, I.Price);
        //        }
        //        else
        //        {
        //            FM.CreateItem(I.Name, I.Price);
        //        }
        //    }

        //    // update message for user 

        //    // reset form 
        //}

        private void Button_Click_ManuNav(object sender, RoutedEventArgs e)
        {
            RadioButton clickedButton = sender as RadioButton;
            Style HiddenStyle = (Style)FindResource("HiddenStyle");
            Style VisibleStyle = (Style)FindResource("VisibleStyle");

            if (clickedButton != null)
            {
                switch (clickedButton.Name)
                {
                    case "chargingBUT":
                        {
                            ChargesNOrdersTabMenu.Visibility = Visibility.Visible;
                            ReportsTabMenu.Visibility = Visibility.Hidden;
                            ItemsTabMenu.Visibility = Visibility.Hidden;

                            chargeBUT.IsChecked = true;
                            RefundHeadTitle.Style = HiddenStyle;
                            OrderCreationScreen.Style = HiddenStyle;
                            ReportsTable.Style = HiddenStyle;
                            ChargingScreen.Style = VisibleStyle;
                            break;
                        }
                    case "reportsBUT":
                        {
                            ChargesNOrdersTabMenu.Visibility = Visibility.Hidden;
                            ReportsTabMenu.Visibility = Visibility.Visible;
                            ItemsTabMenu.Visibility = Visibility.Hidden;

                            //all doc = checked instead of ▼
                            RefundHeadTitle.Style = HiddenStyle;
                            OrderCreationScreen.Style = HiddenStyle;
                            ChargingScreen.Style = HiddenStyle;
                            ReportsTable.Style = VisibleStyle;
                            break;
                        }
                    case "itemsBUT":
                        {
                            ChargesNOrdersTabMenu.Visibility = Visibility.Hidden;
                            ReportsTabMenu.Visibility = Visibility.Hidden;
                            ItemsTabMenu.Visibility = Visibility.Visible;

                            //all prod = checked - instead of ▼
                            RefundHeadTitle.Style = HiddenStyle;
                            OrderCreationScreen.Style = HiddenStyle;
                            ChargingScreen.Style = HiddenStyle;
                            ReportsTable.Style = VisibleStyle;
                            break;
                        }
                }
            }
        }

        private void ButtonClick_PayMethods(object sender, RoutedEventArgs e)
        {
            RadioButton clickedButton = sender as RadioButton;
            Style HiddenStyle = (Style)FindResource("HiddenStyle");
            Style VisibleStyle = (Style)FindResource("VisibleStyle");

            if (clickedButton != null)
            {
                switch (clickedButton.Name)
                {
                    case "cashBUT":
                        {
                            if (CashForm == null) { break; }

                            CashForm.Style = VisibleStyle;
                            CreditCaForm.Style = HiddenStyle;
                            ChequeForm.Style = HiddenStyle;
                            BankForm.Style = HiddenStyle;
                            BitForm.Style = HiddenStyle;
                            break;
                        }
                    case "CredBUT":
                        {
                            CreditCaForm.Style = VisibleStyle;
                            CashForm.Style = HiddenStyle;
                            ChequeForm.Style = HiddenStyle;
                            BankForm.Style = HiddenStyle;
                            BitForm.Style = HiddenStyle;
                            break;
                        }
                    case "chequeBUT":
                        {
                            ChequeForm.Style = VisibleStyle;
                            CashForm.Style = HiddenStyle;
                            CreditCaForm.Style = HiddenStyle;
                            BankForm.Style = HiddenStyle;
                            BitForm.Style = HiddenStyle;
                            break;
                        }
                    case "bankBUT":
                        {
                            BankForm.Style = VisibleStyle;
                            CashForm.Style = HiddenStyle;
                            CreditCaForm.Style = HiddenStyle;
                            ChequeForm.Style = HiddenStyle;
                            BitForm.Style = HiddenStyle;
                            break;
                        }
                    case "bitBUT":
                        {
                            BitForm.Style = VisibleStyle;
                            CashForm.Style = HiddenStyle;
                            CreditCaForm.Style = HiddenStyle;
                            ChequeForm.Style = HiddenStyle;
                            BankForm.Style = HiddenStyle;
                            break;
                        }
                }
            }
        }


        private void Button_Click_ChargeTabNav(object sender, RoutedEventArgs e)
        {
            RadioButton clickedButton = sender as RadioButton;
            Style HiddenStyle = (Style)FindResource("HiddenStyle");
            Style VisibleStyle = (Style)FindResource("VisibleStyle");

            if (clickedButton != null)
            {
                switch (clickedButton.Name)
                {
                    case "chargeBUT":
                        {
                            if (CashForm == null) { break; }

                            ChargingScreen.Style = VisibleStyle;
                            RefundHeadTitle.Style = HiddenStyle;
                            ReportsTable.Style = HiddenStyle;
                            OrderCreationScreen.Style = HiddenStyle;
                            break;
                        }
                    case "refundBUT":
                        {
                            RefundHeadTitle.Style = VisibleStyle;
                            ReportsTable.Style = VisibleStyle;
                            ChargingScreen.Style = HiddenStyle;
                            OrderCreationScreen.Style = HiddenStyle;
                            break;
                        }
                    case "orderBUT":
                        {
                            OrderCreationScreen.Style = VisibleStyle;
                            RefundHeadTitle.Style = HiddenStyle;
                            ReportsTable.Style = HiddenStyle;
                            ChargingScreen.Style = HiddenStyle;

                            break;
                        }
                }
            }
        }

        private void RadioButton_ExitApp(object sender, RoutedEventArgs e)
        {
            // navigate out to home
        }

        private void Button_ClickNewProductLine(object sender, RoutedEventArgs e)
        {
            Grid lines = chargeBUT.IsChecked == true ? InvoiceItemLines : OrderItemLines;
            string kind = chargeBUT.IsChecked == true ? "I" : "O";

            // Increment the row count
            int newRow = lines.RowDefinitions.Count;

            // Add a new RowDefinition to the parent Grid
            lines.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Create a new Grid for the product line
            Grid newProductLine = new Grid { Margin = new Thickness(0, 0, 0, 5) };
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Star) });
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            newProductLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });

            // Set the row where this new Grid will be placed
            Grid.SetRow(newProductLine, newRow);

            // Add the Button
            Button deleteButton = new Button { HorizontalAlignment = HorizontalAlignment.Left, Style = (Style)FindResource("TopButton") };
            deleteButton.Content = new Image { Source = new BitmapImage(new Uri("/Accets/DeleteIcon.png", UriKind.Relative)), Width = 15, Height = 15 };
            deleteButton.Click += (sender, e) => DeleteProductLine(newProductLine); // Assign the event handler
            //Grid.SetColumn(deleteButton, 0);
            newProductLine.Children.Add(deleteButton);

            // Add TextBoxes for Product ID, Description, Price, Quantity, and Total Price
            TextBox productIDTextBox = new TextBox { Name = $"{kind}prodID", Margin = new Thickness(5, 0, 5, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(productIDTextBox, 1);
            newProductLine.Children.Add(productIDTextBox);

            TextBox descriptionTextBox = new TextBox { Name = $"{kind}prodDescription", Margin = new Thickness(5, 0, 5, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(descriptionTextBox, 2);
            newProductLine.Children.Add(descriptionTextBox);

            TextBox priceTextBox = new TextBox { Name = $"{kind}prodPrice", Margin = new Thickness(5, 0, 5, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            priceTextBox.TextChanged += InvoiceLine_TextChanged;
            Grid.SetColumn(priceTextBox, 3);
            newProductLine.Children.Add(priceTextBox);

            TextBox quantityTextBox = new TextBox { Name = $"{kind}prodQuantity", Text = "1", Margin = new Thickness(5, 0, 5, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            quantityTextBox.TextChanged += InvoiceLine_TextChanged;
            Grid.SetColumn(quantityTextBox, 4);
            newProductLine.Children.Add(quantityTextBox);

            TextBox totalPriceTextBox = new TextBox { Name = $"{kind}prodTotalPrice", Text = "0.00", Margin = new Thickness(5, 0, 5, 0), IsReadOnly = true, Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(totalPriceTextBox, 5);
            newProductLine.Children.Add(totalPriceTextBox);

            // Add the new Grid to the ItemsLines Grid
            lines.Children.Add(newProductLine);
        }

        private void DeleteProductLine(Grid productLine)
        {
            Grid lines = chargeBUT.IsChecked == true ? InvoiceItemLines : OrderItemLines;

            // Get the row index of the grid to be removed
            int rowIndex = Grid.GetRow(productLine);

            // Remove the grid from the ItemsLines grid
            lines.Children.Remove(productLine);

            // Adjust the rows of the grids below the deleted one
            for (int i = rowIndex + 1; i < lines.RowDefinitions.Count; i++)
            {
                foreach (UIElement child in lines.Children)
                {
                    if (Grid.GetRow(child) == i)
                    {
                        Grid.SetRow(child, i - 1); // Move the row up by one
                    }
                }
            }

            // Remove the corresponding RowDefinition
            lines.RowDefinitions.RemoveAt(rowIndex);
            UpdateInvoiceTotalPrice();
        }

        private void Button_ClickNewChequeLine(object sender, RoutedEventArgs e)
        {
            Grid lines = ChequeLines;  // Assuming the Grid is named "ChequeForm"

            // Increment the row count
            int newRow = lines.RowDefinitions.Count;

            // Add a new RowDefinition to the parent Grid
            lines.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Create a new Grid for the cheque line
            Grid newChequeLine = new Grid { Margin = new Thickness(0, 0, 0, 5) };
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            newChequeLine.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });

            // Set the row where this new Grid will be placed
            Grid.SetRow(newChequeLine, newRow);

            // Add the Delete Button
            Button deleteButton = new Button { HorizontalAlignment = HorizontalAlignment.Left, Style = (Style)FindResource("TopButton") };
            deleteButton.Content = new Image { Source = new BitmapImage(new Uri("/Accets/DeleteIcon.png", UriKind.Relative)), Width = 15, Height = 15 };
            deleteButton.Click += (sender, e) => DeleteChequeLine(newChequeLine); // Assign the event handler
            newChequeLine.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 0);

            // Add TextBoxes for Account Number, Branch Number, Bank Number, Cheque Number, Date, Cheque Amount
            TextBox accountNumberTextBox = new TextBox { Name = "AccountNumber", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(accountNumberTextBox, 1);
            newChequeLine.Children.Add(accountNumberTextBox);

            TextBox branchNumberTextBox = new TextBox { Name = "BranchNumber", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(branchNumberTextBox, 2);
            newChequeLine.Children.Add(branchNumberTextBox);

            TextBox bankNumberTextBox = new TextBox { Name = "BankNumber", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(bankNumberTextBox, 3);
            newChequeLine.Children.Add(bankNumberTextBox);

            TextBox chequeNumberTextBox = new TextBox { Name = "ChequeNumber", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(chequeNumberTextBox, 4);
            newChequeLine.Children.Add(chequeNumberTextBox);

            TextBox dateTextBox = new TextBox { Name = "ChequeDate", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            Grid.SetColumn(dateTextBox, 5);
            newChequeLine.Children.Add(dateTextBox);

            TextBox chequeAmountTextBox = new TextBox { Name = "ChequeAmount", Margin = new Thickness(5, 0, 10, 0), Style = (Style)FindResource("TextBoxFilterBar") };
            chequeAmountTextBox.TextChanged += PaymentSumUpdated;
            Grid.SetColumn(chequeAmountTextBox, 6);
            newChequeLine.Children.Add(chequeAmountTextBox);

            // Add the new Grid to the ChequeForm Grid
            lines.Children.Add(newChequeLine);
        }

        private void DeleteChequeLine(Grid ChequeLine)
        {
            Grid lines = ChequeLines; // Assuming the Grid is named "ChequeForm"

            // Get the row index of the grid to be removed
            int rowIndex = Grid.GetRow(ChequeLine);

            // Remove the grid from the ChequeForm grid
            lines.Children.Remove(ChequeLine);

            // Adjust the rows of the grids below the deleted one
            for (int i = rowIndex + 1; i < lines.RowDefinitions.Count; i++)
            {
                foreach (UIElement child in lines.Children)
                {
                    if (Grid.GetRow(child) == i)
                    {
                        Grid.SetRow(child, i - 1); // Move the row up by one
                    }
                }
            }

            // Remove the corresponding RowDefinition
            lines.RowDefinitions.RemoveAt(rowIndex);
            UpdateInvoiceBalance();
        }


        private void InvoiceLine_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox == null) return;

            Grid parentGrid = textBox.Parent as Grid;

            if (parentGrid == null) return;

            TextBox? priceTextBox = parentGrid.Children
                .OfType<TextBox>()
                .FirstOrDefault(tb => tb.Name.Contains("prodPrice"));

            TextBox? quantityTextBox = parentGrid.Children
                .OfType<TextBox>()
                .FirstOrDefault(tb => tb.Name.Contains("prodQuantity"));

            TextBox? totalPriceTextBox = parentGrid.Children
                .OfType<TextBox>()
                .FirstOrDefault(tb => tb.Name.Contains("prodTotalPrice"));

            if (priceTextBox == null || quantityTextBox == null || totalPriceTextBox == null) return;

            decimal price = 0;
            int quantity = 1;

            if (decimal.TryParse(priceTextBox.Text, out decimal parsedPrice))
            {
                price = parsedPrice;
            }
            if (int.TryParse(quantityTextBox.Text, out int parsedQuantity))
            {
                quantity = parsedQuantity;
            }

            decimal totalPrice = price * quantity;
            totalPriceTextBox.Text = totalPrice.ToString();
            UpdateInvoiceTotalPrice();
        }

        private void UpdateInvoiceTotalPrice()
        {
            Grid lines = chargeBUT.IsChecked == true ? InvoiceItemLines : OrderItemLines;
            decimal totalPrice = 0;

            foreach (Grid g in lines.Children)
            {
                if (g.Children[5] is TextBox totalPriceTextBox)
                {
                    if (decimal.TryParse(totalPriceTextBox.Text, out decimal lineTotal))
                    {
                        totalPrice += lineTotal;
                    }
                }
            }
            if (lines == InvoiceItemLines)
            {
                InvoiceTotalSum.Text = totalPrice.ToString();
                DealTotalSum.Text = $"Total Price: {totalPrice.ToString()}";
                UpdateInvoiceBalance();
            }
            else
            {
                OrderTotalSum.Text = totalPrice.ToString();
            }
        }

        private void UpdateInvoiceBalance()
        {
            decimal totalPriceToPay = decimal.Parse(InvoiceTotalSum.Text);
            decimal totalPayed = 0;

            decimal.TryParse(TotalCash?.Text ?? "0", out decimal cashValue);
            decimal.TryParse(CardSumToBill?.Text ?? "0", out decimal cardValue);
            decimal.TryParse(TotalBankSum?.Text ?? "0", out decimal bankValue);
            decimal.TryParse(TotalBitSum?.Text ?? "0", out decimal bitValue);

            totalPayed = cashValue + cardValue + bankValue + bitValue;

            if (ChequeLines != null)
            {
                foreach (Grid g in ChequeLines.Children)
                {
                    if (g.Children[6] is TextBox ChequeAmount)
                    {
                        if (decimal.TryParse(ChequeAmount.Text, out decimal lineTotal))
                        {
                            totalPayed += lineTotal;
                        }
                    }
                }
            }
            if (BalanceDueSum != null)
            {
                BalanceDueSum.Text = $"Balance Due: {totalPayed - totalPriceToPay}";
            }
        }

        private void PaymentSumUpdated(object sender, TextChangedEventArgs e)
        {
            UpdateInvoiceBalance();
        }
    }
}