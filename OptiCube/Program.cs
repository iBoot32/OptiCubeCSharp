using System;

namespace OptiCube
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" [*]   stage 1: " + RubiksPathFinder.rubikspathfinder.path(cubestringcalc.str(args)));
            Console.WriteLine("");
            Console.WriteLine(" [*]   stage 2: " + RubiksPathFinder.stage2.path(RubiksPathFinder.rubikspathfinder.corner_init));
        }
    }
}
