using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DynamicButtons.Models
{
    public class Product : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Display
        {
            get
            {
                return $"{Name} x {Units}\n ${Price}";
            }
        }
        public double UnitPrice { get; set; }
        private int units;
        public int Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Display");
            }
        }

        public virtual double Price { get; }

        public Guid Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class ProductByWeight : Product
    {
        public double weight;
        public override double Price
        {
            get
            {
                return Units * UnitPrice;
            }
        }
    }
    class ProductByQuantity : Product
    {
        public int quantity;
        public override double Price
        {
            get
            {
                return Units * UnitPrice;
            }
        }
    }
}
