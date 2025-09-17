using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PriceFinder
{
    /// <summary>
    /// Логика взаимодействия для SearchMethodChanger.xaml
    /// </summary>
    public partial class SearchMethodChanger : Window
    {
        public string NewMethod {
            get
            {
                if ((bool)BinaryRadioButton.IsChecked)
                {
                    return "binary";
                }
                else
                {
                    return "linear";
                }
            }
        }

        public bool Change = false;

        public SearchMethodChanger(string CurrentMethod)
        {
            
            

            InitializeComponent(); 
            if (CurrentMethod == "binary")
            {
                BinaryRadioButton.IsChecked = true;
            }
            else
            {
                LinearRadioButton.IsChecked = true;
            }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            Change = true;
            this.Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
