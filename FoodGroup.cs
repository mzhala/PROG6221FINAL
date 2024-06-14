using System;
using System.Collections.Generic;
using PROG6221_FINAL;
// Base class representing an FoodGroup in a FoodGroups
namespace PROG6221_FINAL
{
    public class FoodGroup
    {
        public string Name { get; }
        public string Description { get; }
        public List<string> Examples { get; }

        public FoodGroup(string name, string description, List<string> examples)
        {
            Name = name;
            Description = description;
            Examples = examples;
        }
    }
}