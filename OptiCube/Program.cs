using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCube
{
    class Program
    {
        static void Main(string[] args)
        {
            cubestringcalc.calc(args);
            log("stage 1: " + RubiksPathFinder.rubikspathfinder.path(cubestringcalc.cornerstring, "nxxnxxnxYYxxWRGWOGWOBWRB", "false"));
            log("stage 2: " + cll.recogCase(RubiksPathFinder.rubikspathfinder.corner_init));
        }

        public static void log(string text)
        {
            Console.WriteLine(" [*]   " + text);
        }
    }
}
