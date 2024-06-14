using System;

// Base class representing a food item
namespace PROG6221_FINAL
{
    public class FoodItem
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

        // Constructor for the FoodItem class.
        public FoodItem(string name, float quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }
}