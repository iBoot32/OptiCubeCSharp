using Facet.Combinatorics;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RubiksPathFinder
{
    class rubikspathfinder
    {
        /*
         * RubiksPathFinder
         * 
         *   A program designed to find the optimal
         *   path between two states on a Rubik's cube
         *  
         *  The best part is that this logic is super easy
         *  to implement in any program. Simply add this class
         *  file to your project, call it like:
         *  
         *      string solution = rubikspathfinder.path(current_cube_state_corner, current_cube_state_edge, desired_cube_state_corner, desired_cube_state_edge, show_all_sltns);
         * 
         *  And the program will return the optimal 
         *  path to your desired final cube state.
         *  
         *  
         *  IMPORTANT:
         *      This program relies on a breadth-first search algorithm
         *      As a result, most simple solutions (cross, one f2l pair, etc). 
         *      should take only a few seconds to find. However, complex problems
         *      could take a large number of minutes to generate.
         * 
         *          by Tom O'Donnell (iBoot32)
         */

        public static char[] validmoves =
        {
            'a', 'b', 'c',
            'e', 'g', 'h',
            'i', 'j', 'k',
            'p', 'q', 's'
        };

        public static string[] stdNotation =
        {
            "R", "R'", "R2",
            "L", "L'", "L2",
            "U", "U'", "U2",
            "F", "F'", "F2",
        };

        public static char[] solvedcube =
        {
            'Y', 'R', 'B',
            'Y', 'O', 'B',
            'Y', 'O', 'G',
            'Y', 'R', 'G',
            'W', 'R', 'G',
            'W', 'O', 'G',
            'W', 'O', 'B',
            'W', 'R', 'B'
        };

        public static char[] desired_state = 
            //put your desired state of the cube here in the form of an array
            //using x symbolizing the sticker's color is irrelevant,
            //and n symbolizing the sticker must not be solved
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

        public static bool issolved = false;
        public static int done = 0;
        public static int depth = 0;
        public static string seq;

        public static char[] corner_init = new char[24];

        public static char[] corner_backup = new char[24];
        public static char[] othertemp = new char[24];

        public static Stopwatch st = new Stopwatch();


        public static string path(char[] ci)
        {
            Buffer.BlockCopy(ci, 0, corner_init, 0, corner_init.Length * sizeof(char));
            Buffer.BlockCopy(corner_init, 0, corner_backup, 0, corner_init.Length * sizeof(char));

            while (!issolved)
            {
                depth++;
                log(" - searching depth " + depth);
                Variations<char> variations = new Variations<char>(validmoves, depth, GenerateOption.WithRepetition); //in this case, a variation is simply a permutation with an upper bound on the number of moves to take

                st.Start();
                foreach (IList<char> v in variations)
                {
                    foreach (var mov in v)
                    {
                        seq += mov + " ";
                        Buffer.BlockCopy(corner_init, 0, othertemp, 0, corner_init.Length * sizeof(char)); //make temp array equal current array
                        manipulate(mov);
                        Buffer.BlockCopy(othertemp, 0, corner_init, 0, corner_init.Length * sizeof(char)); //commit changes back to normal array fromp temp array
                    }
                    done++;

                    if (EndStateReached())
                    {
                        st.Stop();
                        solved();
                        return seq;
                    }
                    seq = "";
                    Buffer.BlockCopy(corner_backup, 0, corner_init, 0, corner_init.Length * sizeof(char));
                }
            }
            return seq;
        }

        //checks if the desired state can be reached. This function can be rewritten to define any "desired end state," not at all requiring the use of the desired_state array.
        //for example, you could set all corners being oriented as a "desired end state". The only constraint is you return True once this state is reached, and false otherwise.
        public static bool EndStateReached()
        {
            for (int i = 0; i < corner_init.Length; i++)
            {
                //"x" can be used as "null," meaning the color of that sticker does not matter in the end
                if (desired_state[i] != 'x') //if desired sticker doesn't matter, we good no need to check
                {
                    if (corner_init[i] != desired_state[i] && desired_state[i] != 'n') //need `&& fin[i] != "n"` or else it'll count fin[i] == "n" cases as unsolved
                    {
                        return false;
                    }
                }
                if (desired_state[i] == 'n' && corner_init[i] == solvedcube[i]) //use "n" to specify you want this sticker to NOT be solved in the end
                {
                    return false;
                }
            }
            return true;
        }



        public static void manipulate(char move)
        {
            switch (move)
            {
                case 'a':
                    put(3, 20); put(4, 19); put(5, 18); //put corner 2 into corner 7 on the temp array
                    put(18, 17); put(19, 16); put(20, 15); //put corner 7 into corner 6
                    put(15, 8); put(16, 7); put(17, 6); //put corner 6 into corner 3
                    put(6, 5); put(7, 4); put(8, 3); //put corner 3 into corner 2
                    break;
                case 'b':
                    put(20, 3); put(19, 4); put(18, 5); //put corner 2 into corner 7
                    put(17, 18); put(16, 19); put(15, 20); //put corner 7 into corner 6
                    put(8, 15); put(7, 16); put(6, 17); //put corner 6 into corner 3
                    put(5, 6); put(4, 7); put(3, 8); //put corner 3 into corner 2
                    break;
                case 'c':
                    put(4, 16); put(16, 4); put(7, 19); //put corner 2 into corner 7
                    put(19, 7); put(3, 15); put(15, 3); //put corner 7 into corner 6
                    put(6, 18); put(18, 6); put(8, 20); //put corner 6 into corner 3
                    put(20, 8); put(5, 17); put(17, 5); //put corner 3 into corner 2
                    break;
                case 'e':
                    put(0, 11); put(1, 10); put(2, 9);
                    put(9, 14); put(10, 13); put(11, 12);
                    put(12, 23); put(13, 22); put(14, 21);
                    put(21, 2); put(22, 1); put(23, 0);
                    break;
                case 'g':
                    put(11, 0); put(10, 1); put(9, 2);
                    put(14, 9); put(13, 10); put(12, 11);
                    put(23, 12); put(22, 13); put(21, 14);
                    put(2, 21); put(1, 22); put(0, 23);
                    break;
                case 'h':
                    put(0, 12); put(1, 13); put(2, 14);
                    put(9, 21); put(10, 22); put(11, 23);
                    put(12, 0); put(13, 1); put(14, 2);
                    put(21, 9); put(22, 10); put(23, 11);
                    break;
                case 'i':
                    put(0, 3); put(1, 5); put(2, 4);
                    put(3, 6); put(4, 8); put(5, 7);
                    put(6, 9); put(7, 11); put(8, 10);
                    put(9, 0); put(10, 2); put(11, 1);
                    break;
                case 'j':
                    put(3, 0); put(5, 1); put(4, 2);
                    put(6, 3); put(8, 4); put(7, 5);
                    put(9, 6); put(11, 7); put(10, 8);
                    put(0, 9); put(2, 10); put(1, 11);
                    break;
                case 'k':
                    put(0, 6); put(1, 7); put(2, 8);
                    put(3, 9); put(4, 10); put(5, 11);
                    put(6, 0); put(7, 1); put(8, 2);
                    put(9, 3); put(10, 4); put(11, 5);
                    break;
                case 'p':
                    put(9, 7); put(10, 6); put(11, 8);
                    put(6, 16); put(7, 15); put(8, 17);
                    put(15, 13); put(16, 12); put(17, 14);
                    put(12, 10); put(13, 9); put(14, 11);
                    break;
                case 'q':
                    put(7, 9); put(6, 10); put(8, 11);
                    put(16, 6); put(15, 7); put(17, 8);
                    put(13, 15); put(12, 16); put(14, 17);
                    put(10, 12); put(9, 13); put(11, 14);
                    break;
                case 's':
                    put(9, 15); put(10, 16); put(11, 17);
                    put(6, 12); put(7, 13); put(8, 14);
                    put(15, 9); put(16, 10); put(17, 11);
                    put(12, 6); put(13, 7); put(14, 8);
                    break;
            }
        }

        static void put(int positionA, int positionB)
        {
            othertemp[positionB] = corner_init[positionA];
        }

        public static void solved()
        {
            for (int i = 0; i < validmoves.Length; i++)
            {
                seq = seq.Replace(validmoves[i].ToString(), stdNotation[i]);
            }
            Console.WriteLine("");
            log(" checked " + done + " states in " + st.ElapsedMilliseconds + "ms (" + done / (st.Elapsed.TotalMilliseconds / 1000) + " per second)");
        }

        public static void log(string text)
        {
            Console.WriteLine(" [*]  " + text);
        }
    }
}
