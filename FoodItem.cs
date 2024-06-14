using System;
using PROG6221_FINAL;
// Base class representing a food item
namespace PROG6221_FINAL
{
    public class FoodItem
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        // Constructor for the FoodItem class.
        public FoodItem(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }
}