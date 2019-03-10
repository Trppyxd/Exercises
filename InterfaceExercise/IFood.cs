using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExercise
{
    public interface IFood
    {
        int Calories();
        int Fat();
        bool IsHealthy();
    }
}
