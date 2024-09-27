using System.Windows;
using System.Windows.Controls;

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

        private void Button_ClickChargeNOrder(object sender, RoutedEventArgs e)
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

                            FilterBar.Style = HiddenStyle;
                            DataTable.Style = HiddenStyle;
                            ChargingScreen.Style = VisibleStyle;
                            break;
                        }
                    case "reportsBUT":
                        {
                            ChargesNOrdersTabMenu.Visibility = Visibility.Hidden;
                            ReportsTabMenu.Visibility = Visibility.Visible;
                            ItemsTabMenu.Visibility = Visibility.Hidden;

                            ChargingScreen.Style = HiddenStyle;
                            FilterBar.Style = VisibleStyle;
                            DataTable.Style = VisibleStyle;
                            break;
                        }
                    case "itemsBUT":
                        {
                            ChargesNOrdersTabMenu.Visibility = Visibility.Hidden;
                            ReportsTabMenu.Visibility = Visibility.Hidden;
                            ItemsTabMenu.Visibility = Visibility.Visible;

                            ChargingScreen.Style = HiddenStyle;
                            FilterBar.Style = VisibleStyle;
                            DataTable.Style = VisibleStyle;
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

        private void RadioButton_ExitApp(object sender, RoutedEventArgs e)
        {
            // navigate out to home
        }
    }
}