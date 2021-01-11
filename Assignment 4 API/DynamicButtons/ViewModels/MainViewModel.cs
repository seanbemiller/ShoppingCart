using DynamicButtons.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DynamicButtons.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<InventoryItem> Products { get; set; }
        public InventoryItem SelectedProduct { get; set; }
        public ObservableCollection<Product> Cart { get; set; }
        public Product SelectedItem { get; set; }
        public string Total => $"Subtotal {Cart.Sum(i => i.Price):C}\nTax {Cart.Sum(i => i.Price) * 0.075:C}\nTotal {Cart.Sum(i => i.Price)*1.075:C} ";

        public MainViewModel()
        {
            Products = new ObservableCollection<InventoryItem>();
            Cart = new ObservableCollection<Product>();

            //var usePath = AppDataPaths.GetDefault().LocalAppData; //appdata path
            //var usePath = ".";

            var handler = new WebRequestHandler();
            var inventory = JsonConvert.DeserializeObject<List<InventoryItem>>(handler.Get("http://localhost/Assignment4/assignment4/getInvent").Result); //get inventory from webAPI
            var products = JsonConvert.DeserializeObject<List<Product>>(handler.Get("http://localhost/Assignment4/assignment4/getCart").Result); //get cart from webAPI

            foreach (var prod in inventory)
            {
                Products.Add(prod);
            }
            foreach (var prod in products)
            {
                Cart.Add(prod);
            }

        }

        public async void AddToCart()
        {
            var handler = new WebRequestHandler();
            if (SelectedProduct == null)
            {
                return;
            }
            if(Cart.Any(i => i.Id.Equals(SelectedProduct.Id))) //increment cart prod
            {
                //var prodToInc = JsonConvert.DeserializeObject<InventoryItem>(await handler.Post("http://localhost/Assignment4/assignment4/AddOrUpdate", SelectedItem));
                await handler.Post("http://localhost/Assignment4/assignment4/AddOrUpdate", SelectedProduct);
                Cart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)).Units++;
            } else
            {
                //var prodToInc = JsonConvert.DeserializeObject<InventoryItem>(await handler.Post("http://localhost/Assignment4/assignment4/AddOrUpdate", SelectedItem));
                await handler.Post("http://localhost/Assignment4/assignment4/AddOrUpdate", SelectedProduct);
                if (SelectedProduct.type == 0) // add new prodByWeight
                {
                    Cart.Add(new ProductByWeight { Name = SelectedProduct.Name, Description = SelectedProduct.Description, UnitPrice = SelectedProduct.Price, Units = 1, Id = SelectedProduct.Id });
                }
                else // add new prodByQuant
                {
                    Cart.Add(new ProductByQuantity { Name = SelectedProduct.Name, Description = SelectedProduct.Description, UnitPrice = SelectedProduct.Price, Units = 1, Id = SelectedProduct.Id });
                }
            }

            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("Total");
        }
        public async void RemoveCart()
        {
            if (SelectedItem == null)
            {
                return;
            }
            if (Cart.Any(i => i.Id.Equals(SelectedItem.Id)))
            {
                var handler = new WebRequestHandler();
                //var prodToRemove = JsonConvert.DeserializeObject<Product>(await handler.Post("http://localhost/Assignment4/assignment4/Delete", SelectedItem)); 
                await handler.Post("http://localhost/Assignment4/assignment4/Delete", SelectedItem.Name);
                if (SelectedItem.Units == 1)
                {
                    Cart.Remove(SelectedItem);
                    //Cart.FirstOrDefault(i => i.Id.Equals(SelectedItem.Id)).Units--;
                }
                else //decrement
                {
                    Cart.FirstOrDefault(i => i.Id.Equals(SelectedItem.Id)).Units--;
                }
            }

            SelectedItem = null;
            NotifyPropertyChanged("SelectedItem");
            NotifyPropertyChanged("Total");
        }
        public void PrintAndQuit()
        {

            var output = "";
            foreach (var thing in Cart){
                output += $"Product - {thing.Name} - ({thing.Units}) - Price: ${thing.Price}\n";
            }

            var usePath = AppDataPaths.GetDefault().LocalAppData; //appdata path
            //var usePath = ".";
            File.WriteAllText($"{usePath}\\recipt.txt", $"-=-=-Thanks for Shopping-=-=-\n\n" + output + "\n" + Total);
            
            //File.WriteAllText($"recipt.txt", $"-=-=-Thanks for Shopping-=-=-\n\n" + output + "\n" + Total);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
