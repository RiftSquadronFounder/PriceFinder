using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PriceFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentMethod { get; set; } = "linear";
        public CustomFileManager _customFileManager { get; set; }
        public MainWindow()
        {
            
            _customFileManager = new CustomFileManager("Things.txt");
            InitializeComponent();
            ShopItemsListGUI.ItemsSource = _customFileManager.Items;
            ShopItemsListGUI.Items.Refresh();
        }

        private void ChangeSearchMethod(object sender, RoutedEventArgs e)
        {
            SearchMethodChanger searchMethodChanger = new SearchMethodChanger(CurrentMethod);
            searchMethodChanger.ShowDialog();
            if (searchMethodChanger.Change)
            {
                CurrentMethod = searchMethodChanger.NewMethod;
            }
        }







        private void ToFind(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (PromptGUI.Text == "")
                {
                    ShopItemsListGUI.ItemsSource = _customFileManager.Items;
                    TextLine111.Text = "Строка пуста; ";
                }
                else
                {
                    List<ShopItem> items = new List<ShopItem>();
                    TextLine111.Text = "Произведен поиск; ";
                    if (CurrentMethod == "linear")
                    {
                        TextLine111.Text += "Линейно; ";
                        if (_customFileManager.LinearPriceFind(Int32.Parse(PromptGUI.Text)) != -1)
                        {
                            items.Add((_customFileManager.Items[_customFileManager.LinearPriceFind(Int32.Parse(PromptGUI.Text))]));
                            ShopItemsListGUI.ItemsSource = items;
                        }
                        else {
                            TextLine111.Text += "Безрезультатно; ";
                        }
                    }
                    else
                    {
                        items = items.OrderByDescending(x=>x.Price).ToList();
                        for (int i = 0; i < items.Count; i++)
                        {
                            MessageBox.Show($"{items[i].Price}");
                        }
                        if (_customFileManager.BinPriceFind(Int32.Parse(PromptGUI.Text)) >= 0)
                        {
                            items.Add((_customFileManager.Items[_customFileManager.BinPriceFind(Int32.Parse(PromptGUI.Text))]));
                            ShopItemsListGUI.ItemsSource = items;
                        }
                        else
                        {
                            TextLine111.Text += "Безрезультатно; ";
                        }
                        TextLine111.Text += "Бинарно; ";

                    }
                }
            }
            catch
            {
                stopwatch.Stop();
                MessageBox.Show("Ашибке");
            }
            finally { 
                TextLine111.Text += $"Времени на поиск: {stopwatch.ElapsedMilliseconds} миллисекунд";
                stopwatch.Stop(); 
            }




            ShopItemsListGUI.Items.Refresh();
            }

      
    
    }
}