using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var burger = new Hamburger();
            var peanut = new Peanut();

            Calories(burger);
            Calories(peanut);

            Console.ReadKey();
        }

        static void Calories(IFood food)
        {
            food.Calories();
        }
    }
}
