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
        public string CurrentSortMethod { get; set; } = "none";
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
            SearchMethodChanger searchMethodChanger = new SearchMethodChanger(CurrentMethod, CurrentSortMethod);
            searchMethodChanger.ShowDialog();
            if (searchMethodChanger.Change)
            {
                CurrentMethod = searchMethodChanger.NewMethod;
                CurrentSortMethod = searchMethodChanger.NewSortMethod;
            }
        }







        private void ToFind(object sender, RoutedEventArgs e)
        {
            _customFileManager.RewindCasted();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                List<ShopItem> items = new List<ShopItem>();
                if (CurrentSortMethod == "bubble")
                {
                    items = _customFileManager.BubbleSort(_customFileManager.Items);
                }
                else if (CurrentSortMethod == "two")
                {
                    items = _customFileManager.TwoThingsSort(_customFileManager.Items);
                }
                else if (CurrentSortMethod == "swap")
                {
                    items = _customFileManager.BubbleSort(_customFileManager.SwapSort(_customFileManager.Items));
                }


                if (PromptGUI.Text == "")
                {
                    if(CurrentSortMethod == "none")
                    {
                        ShopItemsListGUI.ItemsSource = _customFileManager.Items;

                    }
                    else
                    {
                        ShopItemsListGUI.ItemsSource = items;
                    }
                    TextLine111.Text = "Строка пуста; ";
                }
                else
                {
                    
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
            catch (Exception ex)
            {
                stopwatch.Stop();
                MessageBox.Show($"{ex.Message}");
            }
            finally { 
                TextLine111.Text += $"Времени на поиск: {stopwatch.ElapsedMilliseconds} миллисекунд";
                stopwatch.Stop(); 
            }




            ShopItemsListGUI.Items.Refresh();
            }

        
        private void PromptGUI_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PromptGUI.Text != "")
            {
                SearchButtonGUI.Content = "Поиск";
            }
            else
            {
                SearchButtonGUI.Content = "Обновить";
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Кол-во проверок: {_customFileManager.ChecksCasted}\nКол-во перемещений: {_customFileManager.SwapsCasted}\n\nДоп. - {TextLine111.Text}", "Справка");
        }
    }
}