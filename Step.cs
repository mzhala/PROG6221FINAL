using System;
using PROG6221_FINAL;
namespace PROG6221_FINAL
{
    public class Step : RecipeItem
    {
        public string Instruction { get; set; }

        // Constructor for the Step class.
        public Step(string instruction) : base(instruction)
        {
            Instruction = instruction;
        }
    }
}
