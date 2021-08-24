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
         *  Also, include the arg "true" after `desired_cube_state_edge` 
         *  to have the program show every found path to the desired state.
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
            //'m', 'n', 'o',
            'p', 'q', 's'
        };

        public static string[] stdNotation =
        {
            "R", "R'", "R2",
            "L", "L'", "L2",
            "U", "U'", "U2",
            //"D", "D'", "D2",
            "F", "F'", "F2",
        };

        public static string[] solvedcube =
        {
            "Y", "R", "B",
            "Y", "O", "B",
            "Y", "O", "G",
            "Y", "R", "G",
            "W", "R", "G",
            "W", "O", "G",
            "W", "O", "B",
            "W", "R", "B"
        };

        public static bool issolved = false;
        public static int done = 0;
        public static int depth = 0;
        public static string seq;

        public static string[] corner_init = new string[24];
        public static string[] corner_fin = new string[24];

        public static string[] corner_backup = new string[24];
        public static string[] othertemp = new string[24];

        public static bool doMultiple = false;

        public static Stopwatch st = new Stopwatch();


        public static string path(string ci, string cf, string mult)
        {
            if (mult == "true")
            {
                doMultiple = true;
            }
            Console.WriteLine("");

            for (int i = 0; i < 24; i++)
            {
                corner_init[i] += ci.Substring(i, 1);

                corner_fin[i] += cf.Substring(i, 1);

                corner_backup[i] += ci.Substring(i, 1); //need these backups so we can restore previous cubestate after testing moves
            }

            if (statesEqual(corner_init, corner_fin))
            {
                solved();
            }

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
                        manipulate(mov);
                    }
                    done++;
                    //log(seq);

                    if (statesEqual(corner_init, corner_fin))
                    {
                        st.Stop();
                        solved();
                        return seq;
                    }
                    seq = "";

                    for (int z = 0; z < 24; z++)
                    {
                        corner_init[z] = corner_backup[z];
                    }
                }
                //log("time elapsed is " + st.ElapsedMilliseconds);
            }


            return seq;
        }

        public static bool statesEqual(string[] init, string[] fin)
        {
            for (int i = 0; i < init.Length; i++)
            {
                //"x" can be used as "null," meaning the color of that sticker does not matter in the end
                if (fin[i] != "x") //if desired sticker doesn't matter, we good no need to check
                {
                    if (init[i] != fin[i] && fin[i] != "n") //need `&& fin[i] != "n"` or else it'll count fin[i] == "n" cases as unsolved
                    {
                        return false;
                    }
                }
                if (fin[i] == "n" && init[i] == solvedcube[i]) //use "n" to specify you want this sticker to NOT be solved in the end
                {
                    return false;
                }
            }
            return true;
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



        public static void manipulate(char move)
        {
            switch (move)
            {
                case 'a':
                    resetcorner(); //make temp array equal current array
                    put(corner_init, 3, 20); put(corner_init, 4, 19); put(corner_init, 5, 18); //put corner 2 into corner 7 on the temp array
                    put(corner_init, 18, 17); put(corner_init, 19, 16); put(corner_init, 20, 15); //put corner 7 into corner 6
                    put(corner_init, 15, 8); put(corner_init, 16, 7); put(corner_init, 17, 6); //put corner 6 into corner 3
                    put(corner_init, 6, 5); put(corner_init, 7, 4); put(corner_init, 8, 3); //put corner 3 into corner 2
                    for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; } //commit changes back to normal array fromp temp array
                    break;
                case 'b':
                    for (int z = 0; z < 3; z++) //sorry i'm too lazy to find direct swaps for R' so let's just do R three times
                    {
                        resetcorner();
                        put(corner_init, 3, 20); put(corner_init, 4, 19); put(corner_init, 5, 18); //put corner 2 into corner 7
                        put(corner_init, 18, 17); put(corner_init, 19, 16); put(corner_init, 20, 15); //put corner 7 into corner 6
                        put(corner_init, 15, 8); put(corner_init, 16, 7); put(corner_init, 17, 6); //put corner 6 into corner 3
                        put(corner_init, 6, 5); put(corner_init, 7, 4); put(corner_init, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'c':
                    for (int z = 0; z < 2; z++) //R2 is the same as R R
                    {
                        resetcorner();
                        put(corner_init, 3, 20); put(corner_init, 4, 19); put(corner_init, 5, 18); //put corner 2 into corner 7
                        put(corner_init, 18, 17); put(corner_init, 19, 16); put(corner_init, 20, 15); //put corner 7 into corner 6
                        put(corner_init, 15, 8); put(corner_init, 16, 7); put(corner_init, 17, 6); //put corner 6 into corner 3
                        put(corner_init, 6, 5); put(corner_init, 7, 4); put(corner_init, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'e':
                    resetcorner();
                    put(corner_init, 0, 11); put(corner_init, 1, 10); put(corner_init, 2, 9);
                    put(corner_init, 9, 14); put(corner_init, 10, 13); put(corner_init, 11, 12);
                    put(corner_init, 12, 23); put(corner_init, 13, 22); put(corner_init, 14, 21);
                    put(corner_init, 21, 2); put(corner_init, 22, 1); put(corner_init, 23, 0);
                    for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }

                    break;
                case 'g':
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(corner_init, 0, 11); put(corner_init, 1, 10); put(corner_init, 2, 9);
                        put(corner_init, 9, 14); put(corner_init, 10, 13); put(corner_init, 11, 12);
                        put(corner_init, 12, 23); put(corner_init, 13, 22); put(corner_init, 14, 21);
                        put(corner_init, 21, 2); put(corner_init, 22, 1); put(corner_init, 23, 0);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'h':
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(corner_init, 0, 11); put(corner_init, 1, 10); put(corner_init, 2, 9);
                        put(corner_init, 9, 14); put(corner_init, 10, 13); put(corner_init, 11, 12);
                        put(corner_init, 12, 23); put(corner_init, 13, 22); put(corner_init, 14, 21);
                        put(corner_init, 21, 2); put(corner_init, 22, 1); put(corner_init, 23, 0);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'i':
                    resetcorner();
                    put(corner_init, 0, 3); put(corner_init, 1, 5); put(corner_init, 2, 4);
                    put(corner_init, 3, 6); put(corner_init, 4, 8); put(corner_init, 5, 7);
                    put(corner_init, 6, 9); put(corner_init, 7, 11); put(corner_init, 8, 10);
                    put(corner_init, 9, 0); put(corner_init, 10, 2); put(corner_init, 11, 1);
                    for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    break;
                case 'j':
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(corner_init, 0, 3); put(corner_init, 1, 5); put(corner_init, 2, 4);
                        put(corner_init, 3, 6); put(corner_init, 4, 8); put(corner_init, 5, 7);
                        put(corner_init, 6, 9); put(corner_init, 7, 11); put(corner_init, 8, 10);
                        put(corner_init, 9, 0); put(corner_init, 10, 2); put(corner_init, 11, 1);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'k':
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(corner_init, 0, 3); put(corner_init, 1, 5); put(corner_init, 2, 4);
                        put(corner_init, 3, 6); put(corner_init, 4, 8); put(corner_init, 5, 7);
                        put(corner_init, 6, 9); put(corner_init, 7, 11); put(corner_init, 8, 10);
                        put(corner_init, 9, 0); put(corner_init, 10, 2); put(corner_init, 11, 1);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'm':
                    resetcorner();
                    put(corner_init, 12, 15); put(corner_init, 13, 17); put(corner_init, 14, 16);
                    put(corner_init, 15, 18); put(corner_init, 16, 20); put(corner_init, 17, 19);
                    put(corner_init, 18, 21); put(corner_init, 19, 23); put(corner_init, 20, 22);
                    put(corner_init, 21, 12); put(corner_init, 22, 14); put(corner_init, 23, 13);
                    for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }

                    break;
                case 'n':
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(corner_init, 12, 15); put(corner_init, 13, 17); put(corner_init, 14, 16);
                        put(corner_init, 15, 18); put(corner_init, 16, 20); put(corner_init, 17, 19);
                        put(corner_init, 18, 21); put(corner_init, 19, 23); put(corner_init, 20, 22);
                        put(corner_init, 21, 12); put(corner_init, 22, 14); put(corner_init, 23, 13);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'o':
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(corner_init, 12, 15); put(corner_init, 13, 17); put(corner_init, 14, 16);
                        put(corner_init, 15, 18); put(corner_init, 16, 20); put(corner_init, 17, 19);
                        put(corner_init, 18, 21); put(corner_init, 19, 23); put(corner_init, 20, 22);
                        put(corner_init, 21, 12); put(corner_init, 22, 14); put(corner_init, 23, 13);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 'p':
                    resetcorner();
                    put(corner_init, 9, 7); put(corner_init, 10, 6); put(corner_init, 11, 8);
                    put(corner_init, 6, 16); put(corner_init, 7, 15); put(corner_init, 8, 17);
                    put(corner_init, 15, 13); put(corner_init, 16, 12); put(corner_init, 17, 14);
                    put(corner_init, 12, 10); put(corner_init, 13, 9); put(corner_init, 14, 11);
                    for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    break;
                case 'q':
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(corner_init, 9, 7); put(corner_init, 10, 6); put(corner_init, 11, 8);
                        put(corner_init, 6, 16); put(corner_init, 7, 15); put(corner_init, 8, 17);
                        put(corner_init, 15, 13); put(corner_init, 16, 12); put(corner_init, 17, 14);
                        put(corner_init, 12, 10); put(corner_init, 13, 9); put(corner_init, 14, 11);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
                case 's':
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(corner_init, 9, 7); put(corner_init, 10, 6); put(corner_init, 11, 8);
                        put(corner_init, 6, 16); put(corner_init, 7, 15); put(corner_init, 8, 17);
                        put(corner_init, 15, 13); put(corner_init, 16, 12); put(corner_init, 17, 14);
                        put(corner_init, 12, 10); put(corner_init, 13, 9); put(corner_init, 14, 11);
                        for (int i = 0; i < corner_init.Length; i++) { corner_init[i] = othertemp[i]; }
                    }
                    break;
            }
        }

        public static void resetcorner()
        {
            for (int i = 0; i < corner_init.Length; i++) //reset othertemp
            {
                othertemp[i] = corner_init[i];
            }
        }

        static void put(string[] array, int positionA, int positionB)
        {
            othertemp[positionB] = array[positionA];
        }


        public static void log(string text)
        {
            Console.WriteLine(" [*]  " + text);
        }
    }
}
