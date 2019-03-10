using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExercise
{
    public class Peanut : IFood
    {
        public int Calories()
        {
            int calories = 50;
            Console.WriteLine($"Peanut contains {calories} calories.");
            return calories;
        }

        public int Fat()
        {
            int fat = 10;
            Console.WriteLine($"Peanut contains {fat} fat.");
            return fat;
        }

        public bool IsHealthy()
        {
            Console.WriteLine("Peanut is healthy");
            return true;
        }
    }
}
