using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PriceFinder
{
    public class CustomFileManager
    {

        private string _givenFileName = "";
        public string GivenFileName { get { return _givenFileName; } }

       
        public List<ShopItem> Items { get { return ReadFromFile(_givenFileName); } }



        public CustomFileManager(string fileName) {

            _givenFileName = fileName;

        }

        public List<ShopItem> ReadFromFile(string fileName)
        {
            List<ShopItem> shopItems = new List<ShopItem>();
            string FileContents = File.ReadAllText(fileName);
            string ItemTitle = "";
            string ItemPrice = "";
            bool TitleReady = false;
            for (int i = 0; i < FileContents.Length; i++)
            {
                if (FileContents[i] == '\n')
                {
                    shopItems.Add(new ShopItem(ItemTitle, Int32.Parse(ItemPrice)));
                    
                    TitleReady = false;
                    ItemPrice = "";
                    ItemTitle = "";
                }
                else if (FileContents[i] == '|')
                {
                    TitleReady = true;
                }
                else if (TitleReady && FileContents[i] != '\n')
                {
                    ItemPrice += FileContents[i];
                }
                else if (FileContents[i] != '|' && !TitleReady)
                {
                    ItemTitle += FileContents[i];
                }
            }
            return shopItems;
        }


        public int LinearPriceFind(int prompt)
        {
            int k = -1;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Price == prompt) { k = i; break; };
            }
            return k;
        }

        public int BinPriceFind(int prompt)
        {
            int k;
            int L = 0; 
            int R = Items.Count - 1;    
            k = (R + L) / 2;
            while (L < R - 1)
            {
                k = (R + L) / 2;
                if (Items[k].Price == prompt)
                    return k; 
                if (Items[k].Price < prompt)
                    L = k;
                else
                    R = k;
            }
            if (Items[k].Price != prompt)
            {
                if (Items[L].Price == prompt)
                    k = L;
                else
                {
                    if (Items[R].Price == prompt)
                        k = R;
                    else
                        k = -1;
                };
            }
            return k;
        }


    }
}
