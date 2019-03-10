using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"(?<=ool).*(?=wet)");
            string origSentence =
                "1 cat jumped thrice into the same pool, therefore it kept getting wet over and over again. A sad story     indeed.5602 24The end.";
            Console.WriteLine(regex.Match(origSentence));
            Console.ReadKey();
        }
    }
}
