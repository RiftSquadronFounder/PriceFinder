using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceFinder
{
    public class ShopItem
    {
        

        public string Title { get; set; }
        public int Price { get; set; }
        public ShopItem(string title = "blank", int price = -1)
        {
            Title = title;
            Price = price;
        }



    }
}
