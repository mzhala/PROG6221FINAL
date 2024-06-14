using System;
using PROG6221_FINAL;
// Derived class representing an ingredient
namespace PROG6221_FINAL
{
    public class Ingredient : FoodItem
    {
        public int CalorieCount { get; set; } // New property
        public int FoodGroupIndex { get; set; } // New property

        // Constructor for the Ingredient class.
        public Ingredient(string name, double quantity, string unit, int calorieCount, int foodGroupIndex) : base(name, quantity, unit)
        {
            CalorieCount = calorieCount;
            FoodGroupIndex = foodGroupIndex;
        }
    }
}
