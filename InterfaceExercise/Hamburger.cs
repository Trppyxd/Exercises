using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExercise
{
    public class Hamburger : IFood
    {
        public int Calories()
        {
            int calories = 1000;
            Console.WriteLine($"Hamburger contains {calories} calories.");
            return calories;
        }

        public int Fat()
        {
            int fat = 700;
            Console.WriteLine($"Hamburger contains {fat} fat.");
            return fat;
        }

        public bool IsHealthy()
        {
            Console.WriteLine("Hamburger is not healthy");
            return false;
        }
    }
}
