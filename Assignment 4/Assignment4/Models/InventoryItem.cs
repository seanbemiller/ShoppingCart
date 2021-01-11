using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Display //added display to show discription
        {
            get
            {
                return $"{Name} - ${Price}\n {Description}";
            }
        }
        public int type { get; set; } //0 for prodByWeight 1 for prodByQuant
        public Guid Id { get; private set; }

        public InventoryItem()
        {
            Id = System.Guid.NewGuid();
        }
    }
}
