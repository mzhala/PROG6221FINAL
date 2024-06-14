using System;
using System.Collections.Generic;

// Class managing a list of food groups
namespace PROG6221_FINAL
{
    class FoodGroups
    {
        // List to store food groups
        private List<FoodGroup> groups;

        // Constructor
        public FoodGroups()
        {
            // Initializing the list of food groups
            groups = new List<FoodGroup>
            {
                // Creating instances of FoodGroup and adding them to the list
                new FoodGroup(
                    "Starchy foods",
                    "These are our main source of carbohydrates. They have an important role in a healthy diet when eaten in moderation – carb counting can help for those with diabetes, particularly Type 1 diabetes. They are a good source of energy and the main source of a range of nutrients – but it’s important to eat them in moderation if you’re living with diabetes. Many people find a low carb diet helpful for managing diabetes. Besides starch, carbs also contain fibre, calcium, iron and B vitamins.",
                    new List<string> { "Pap", "Samp", "Brown rice", "Potatoes", "Whole wheat bread", "Whole wheat pasta" }
                ),
                new FoodGroup(
                    "Vegetables and fruits",
                    "Try to eat at least 5 portions of fruits and vegetables a day. People with diabetes should choose green, leafy vegetables (rather than starchy options) and lower carb fruits (berries, plums, apricots). Vegetables and fruit contain lots of vitamins and minerals that can help prevent diseases and they are also packed with fibre which can help lower cholesterol and helps digestion.",
                    new List<string> { "Apple", "Pear", "Peach", "Orange", "Mango", "Cabbage", "Pumpkin", "Carrots", "Spinach", "Broccoli", "Cauliflower", "Tomato" }
                ),
                new FoodGroup(
                    "Dry beans, peas, lentils and soya",
                    "These are a good source of fibre, vitamins and minerals and are naturally very low in fat. They can be used as alternatives for meat or chicken because they are a good source of protein and this reduces the amount of fat you’re consuming. Remember that many legumes (dry beans, peas, lentils and soya) are also carbohydrates, so include them in your carb counting.",
                    new List<string> { "Chickpeas", "Kidney beans", "Green peas", "Black beans", "Soy beans", "Split peas" }
                ),
                new FoodGroup(
                    "Chicken, fish, meat and eggs",
                    "A good source of protein, vitamins and minerals. They’re a good choice as part of a healthy balanced diet and there’s no recommended limit on the number of eggs you can eat in a week.Meat is one of the main sources of vitamin B12, an important vitamin which is only found in food from animals, like meat and milk. Some types of meat are higher in fat, especially saturated fat. Eating lots of saturated fat can increase blood cholesterol levels which increase the risk of developing heart disease and stroke. Always try to choose lean cuts of meat with less visible white fat.",
                    new List<string> { "Skinless chicken", "Lean meat", "Mince", "Canned fish", "Frozen fish" }
                ),
                new FoodGroup(
                    "Milk and dairy products",
                    "Milk and dairy products are good sources of protein and vitamins. They also contain calcium, which helps keep our bones healthy and strong. Dairy-free milk alternatives include soya milk and nut milks – if you choose dairy-free milk, then go for unsweetened varieties which have been fortified with calcium. Some dairy products like cheese and yoghurts can be high in salt, sugar or fat (especially saturated fat), so always check the label. And remember that dairy is also a carb!",
                    new List<string> { "Low fat milk", "Cottage cheese", "Plain yoghurt", "Amasi" }
                ),
                new FoodGroup(
                    "Fats and oil",
                    "Some fats are healthier than others, so it’s best to choose unsaturated fats which are plant-based instead of animal-based fats which are saturated fats. Plant-based fats can help lower cholesterol and reduce the risk of heart disease.",
                    new List<string> { "Avocado", "Olive oil", "Nuts and seeds", "Flax seed" }
                ),
                new FoodGroup(
                    "Water",
                    "The body constantly loses fluid through breathing, sweating or urination and therefore this needs to be replaced. Aim to drink 6 to 8 glasses of water each day to help keep your body hydrated.",
                    new List<string> { "Water" }
                )
            };
        }

        // Method to display names of all food groups
        public void DisplayFoodGroupNames()
        {
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {groups[i].Name}");
            }
        }

        // Method to get the name of a food group based on index
        public string GetFoodGroupName(int index)
        {
            if (index >= 0 && index < groups.Count)
            {
                return groups[index].Name;
            }
            else
            {
                return "Invalid index";
            }
        }

        // Method to display details of a food group based on index
        public void DisplayFoodGroupDetails(int index)
        {
            if (index >= 0 && index < groups.Count)
            {
                var group = groups[index];
                Console.WriteLine($"Food Group Name: {group.Name}");
                Console.WriteLine($"Description: {group.Description}");
                Console.WriteLine("Examples:");
                foreach (var example in group.Examples)
                {
                    Console.WriteLine($"- {example}");
                }
            }
            else
            {
                Console.WriteLine("Invalid index");
            }
        }

        // Method to display details of all food groups
        public void DisplayAllFoodGroupDetails()
        {
            for (int i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                Console.WriteLine($"Food Group {i + 1}:");
                Console.WriteLine($"Name: {group.Name}");
                Console.WriteLine($"\nDescription: {group.Description}");
                Console.WriteLine("\nExamples:");
                foreach (var example in group.Examples)
                {
                    Console.WriteLine($"- {example}");
                }
                Console.WriteLine();
            }
        }
    }
}
