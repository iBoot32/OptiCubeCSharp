using System;

namespace OptiCube
{
    class Program
    {
        public static char[] desired_state =
        { 
            'n', 'x', 'x', 
            'n', 'x', 'x', 
            'n', 'x', 'Y', 
            'Y', 'x', 'x', 
            'W', 'R', 'G', 
            'W', 'O', 'G', 
            'W', 'O', 'B', 
            'W', 'R', 'B' 
        };
        static void Main(string[] args)
        {
            Console.WriteLine(" [*]   stage 1: " + RubiksPathFinder.rubikspathfinder.path(cubestringcalc.str(args), desired_state , "false"));
            Console.WriteLine(" [*]   stage 2: " + cll.recogCase(RubiksPathFinder.rubikspathfinder.corner_init));
        }
    }
}
