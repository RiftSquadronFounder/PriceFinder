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

        public int ChecksCasted { get; set; } = 0;
        public int SwapsCasted { get; set; } = 0;
        

        public CustomFileManager(string fileName) {

            _givenFileName = fileName;

        }


        public void RewindCasted()
        {
            ChecksCasted = 0;
            SwapsCasted = 0;
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



        public List<ShopItem> BubbleSort(List<ShopItem> shopItems)
        {
            bool changedInInstance = true;
            int rewindAt = 0;


            while (changedInInstance)
            {
                changedInInstance = false;
                ShopItem previousItem = new ShopItem();
                for (int i = 0; i < shopItems.Count - rewindAt; i++) {
                    if (i == 0) { previousItem = shopItems[i]; ChecksCasted++; }
                    else
                    {
                        ChecksCasted++;
                        previousItem = shopItems[i-1];
                    }
                    if (previousItem.Price != -1 && previousItem.Price > shopItems[i].Price && i != 0)
                    {
                        ChecksCasted++;
                        SwapsCasted++;
                        shopItems[i - 1] = shopItems[i];
                        shopItems[i] = previousItem;
                        changedInInstance = true;
                    }
                }

                rewindAt++;
            }
            return shopItems;
        }

        public List<ShopItem> TwoThingsSort(List<ShopItem> shopItems)
        {
            int rewindAt = 0;
            int startFrom = 0;
            while (shopItems.Count - rewindAt - startFrom >= 2)
            {
                int biggestIndex = -1;
                int smallestIndex = -1;
                ShopItem biggestItem = new ShopItem();
                ShopItem smallestItem = new ShopItem();
                for (int i = startFrom; i < shopItems.Count - rewindAt; i++)
                {
                    //MessageBox.Show($"{i}");
                    if (shopItems[i].Price < smallestItem.Price) { smallestItem = shopItems[i]; smallestIndex = i; ChecksCasted++; }
                    if (shopItems[i].Price > biggestItem.Price) { biggestItem = shopItems[i]; biggestIndex = i; ChecksCasted++; }
                }

                if (smallestIndex > -1)
                {
                    ChecksCasted++;
                    SwapsCasted++;
                    shopItems[smallestIndex] = shopItems[startFrom];
                    shopItems[startFrom] = smallestItem;
                    startFrom++;

                }
                if (biggestIndex > -1)
                {
                    ChecksCasted++;
                    SwapsCasted++;
                    shopItems[biggestIndex] = shopItems[shopItems.Count - rewindAt -1];
                    shopItems[shopItems.Count - rewindAt -1] = biggestItem;
                    rewindAt++;
                    smallestItem = biggestItem;
                }


            }
            
            return shopItems;
        }

        public List<ShopItem> SwapSort(List<ShopItem> shopItems)
        {
            bool changedInInstance = true;
            int rewindAt = 0;



            

            //MessageBox.Show("Entered");
            while (changedInInstance)
            {
                //MessageBox.Show($"rewinder: {rewindAt}");

                changedInInstance = false;
                ShopItem previousItem = new ShopItem();
                int mindedIndex = -1;
                bool swapped = false;

                for (int k = 0; k < 2; k++) { 
                    for (int i = 0; i < shopItems.Count - rewindAt; i++)
                    {
                        if (i == 0)
                        {
                            ChecksCasted++;
                            previousItem = shopItems[i]; mindedIndex = i;
                            changedInInstance = true;
                        }
                        if (previousItem.Price > shopItems[i].Price)
                        {
                            ChecksCasted++;
                            SwapsCasted++;
                            shopItems[mindedIndex] = shopItems[i];
                            shopItems[i] = previousItem;
                            previousItem = shopItems[i];
                            previousItem = new ShopItem();

                            mindedIndex = -1;
                            changedInInstance = true;
                            swapped = true;
                            break;
                        }
                    }
                    if (swapped == false && k == 1 && shopItems.Count - rewindAt - 1 > -1)
                    {
                        SwapsCasted++;
                        shopItems[mindedIndex] = shopItems[shopItems.Count - rewindAt - 1];
                        shopItems[shopItems.Count - rewindAt-1] = previousItem;

                        mindedIndex = -1;
                        previousItem = new ShopItem();
                        changedInInstance = true;
                        swapped = true;
                    }
                }

                rewindAt++;
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
