//using Assignment4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4
{
    public class DataContext
    {
        public static List<InventoryItem> Inventory = new List<InventoryItem> { 
            new InventoryItem {type=0,Name="Ham",Price=.42,Description="Pork product"},
            new InventoryItem {type=0,Name="Carrot",Price=0.25,Description="Orange veggie"},
            new InventoryItem {type=0,Name="Chicken",Price=.13,Description="Poultry"},
            new InventoryItem {type=0,Name="Cheese",Price=0.34,Description="Used to be milk"},
            new InventoryItem {type=0,Name="Potato",Price=0.13,Description="Root veggie"},
            new InventoryItem {type=0,Name="Beef",Price=.74,Description="Comes from cows"},
            new InventoryItem {type=0,Name="Pork",Price=0.23,Description="Comes from pigs"},
            new InventoryItem {type=0,Name="Fish",Price=0.50,Description="Salmon"},
            new InventoryItem {type=0,Name="Beans",Price=0.11,Description="Leggum product"},
            new InventoryItem {type=0,Name="Grapes",Price=0.12,Description="Grow on a vine"},
            new InventoryItem {type=1,Name="Box of Cereal",Price=3.8,Description="Tricks are for kids"},
            new InventoryItem {type=1,Name="Orange Juice",Price=1.30,Description="Rich in vitamin C"},
            new InventoryItem {type=1,Name="Bag of Coffee",Price=8.99,Description="A college must have"},
            new InventoryItem {type=1,Name="Avocado",Price=.99,Description="Great with toast"},
            new InventoryItem {type=1,Name="Candy Bar",Price=1.29,Description="Lots of sugar"},
            new InventoryItem {type=1,Name="Potato Chips",Price=4.29,Description="Lots of salt"},
            new InventoryItem {type=1,Name="Dozen Eggs",Price=1.30,Description="Suprisingly nutrient dense"},
            new InventoryItem {type=1,Name="Loaf of Bread",Price=1.89,Description="Made from wheat"},
            new InventoryItem {type=1,Name="2 litre Soda",Price=2.99,Description="Sprite Zero"},
            new InventoryItem {type=1,Name="$50 Gift Card",Price=50,Description="Visa card redemable anywhere"},
        };
        public static List<Product> Cart = new List<Product>
        {
        };
    }
}
