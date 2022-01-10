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
    class stage2
    {

        public static char[] validmoves =
        {
            'c',
            'h',
            'i', 'j', 'k',
            'l'
        };

        public static string[] stdNotation =
        {
            "R2",
            "L2",
            "U", "U'", "U2",
            "F2"
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

            if (endStateReached())
            {
                st.Stop();
                solved();
                return seq;
            }

            while (true)
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

                    if (endStateReached())
                    {
                        st.Stop();
                        solved();
                        return seq;
                    }
                    seq = "";
                    Buffer.BlockCopy(corner_backup, 0, corner_init, 0, corner_init.Length * sizeof(char));
                }
            }
        }

        public static bool endStateReached()
        {
            for (int i = 0; i < solvedcube.Length; i++)
            {
                if (corner_init[i] != solvedcube[i])
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
                case 'c':
                    put(4, 16); put(16, 4); put(7, 19); //put corner 2 into corner 7
                    put(19, 7); put(3, 15); put(15, 3); //put corner 7 into corner 6
                    put(6, 18); put(18, 6); put(8, 20); //put corner 6 into corner 3
                    put(20, 8); put(5, 17); put(17, 5); //put corner 3 into corner 2
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
                case 'l':
                    put(9, 15); put(10, 16); put(11, 17);
                    put(6, 12); put(7, 13); put(8, 14);
                    put(15, 9); put(16, 10); put(17, 11);
                    put(12, 6); put(13, 7); put(14, 8);
                    break;
                    /*case 'm':
                        put(0, 18); put(1, 19); put(2, 20);
                        put(3, 21); put(4, 22); put(5, 23);
                        put(18, 0); put(19, 1); put(20, 2);
                        put(21, 3); put(22, 4); put(23, 5);
                        break; */ //B2 move
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
