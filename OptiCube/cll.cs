using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCube
{
    class cll
    {
        public static bool caseFound = false;
        public static string moves;
        public static int numberOfU = 0;
        public static string cstring;
        public static string[] c = new string[24]; //holds current csrare

        public static string[] backup = new string[24]; //holds current csrare
        public static string[] c_temp = new string[c.Length]; //needed when making moves to the c as a temp array

        public static string recogCase(string[] cube)
        {
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = cube[i];
            }

            while (!caseFound)
            {
                if (c[9] == "Y" && c[0] != "Y" && c[3] != "Y" && c[6] != "Y" && c[8] == "Y") //sune set
                {
                    //now we recog from each individual sune case via checking which colors are opposites or not
                    if (opp(c[0], c[6]) && opp(c[3], c[7]))
                    {
                        moves += "R U R' U R U2 R'";
                        caseFound = true;
                    }
                    if (opp(c[3], c[6]) && c[0] == c[5])
                    {
                        moves += "L' U2 L U2 L F' L' F";
                        caseFound = true;
                    }
                    if (c[3] == c[7] && !opp(c[0], c[6]))
                    {
                        moves += "F R' F' R U2 R U2 R'";
                        caseFound = true;
                    }
                    if (opp(c[3], c[6]) && c[0] != c[5])
                    {
                        moves += "R U R' U' R' F R F' R U R' U R U2 R'";
                        caseFound = true;
                    }
                    if (c[0] == c[7] && !opp(c[3], c[6]))
                    {
                        moves += "L F' L' F L' U' L";
                        caseFound = true;
                    }
                    if (c[0] == c[5] && opp(c[0], c[6]))
                    {
                        moves += "U' R' F R2 F' R U2 R' U' R2";
                        caseFound = true;
                    }
                    AUF(c);
                }
            }
            return moves;
        }

        public static void AUF(string[] c)
        {
            string[] mov = moves.Split(' ');

            foreach (var move in mov)
            {
                manipulate(move);
            }

            if (c[8] != c[14])
            {
                if (opp(c[8], c[14]))
                {
                    moves += " U2";
                }
                else
                {
                    manipulate("U");
                    if (c[8] == c[14])
                    {
                        moves += " U";
                    }
                    else
                    {
                        moves += " U'";
                    }
                }
            }
            return;
        }

        public static void manipulate(string move)
        {
            switch (move)
            {
                case "R":
                    resetcorner();
                    put(c, 3, 20); put(c, 4, 19); put(c, 5, 18); //put corner 2 into corner 7
                    put(c, 18, 17); put(c, 19, 16); put(c, 20, 15); //put corner 7 into corner 6
                    put(c, 15, 8); put(c, 16, 7); put(c, 17, 6); //put corner 6 into corner 3
                    put(c, 6, 5); put(c, 7, 4); put(c, 8, 3); //put corner 3 into corner 2
                    for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    break;
                case "R'":
                    for (int z = 0; z < 3; z++) //R' is the same as R R R
                    {
                        resetcorner();
                        put(c, 3, 20); put(c, 4, 19); put(c, 5, 18); //put corner 2 into corner 7
                        put(c, 18, 17); put(c, 19, 16); put(c, 20, 15); //put corner 7 into corner 6
                        put(c, 15, 8); put(c, 16, 7); put(c, 17, 6); //put corner 6 into corner 3
                        put(c, 6, 5); put(c, 7, 4); put(c, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "R2":
                    for (int z = 0; z < 2; z++) //R2 is the same as R R
                    {
                        resetcorner();
                        put(c, 3, 20); put(c, 4, 19); put(c, 5, 18); //put corner 2 into corner 7
                        put(c, 18, 17); put(c, 19, 16); put(c, 20, 15); //put corner 7 into corner 6
                        put(c, 15, 8); put(c, 16, 7); put(c, 17, 6); //put corner 6 into corner 3
                        put(c, 6, 5); put(c, 7, 4); put(c, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "L":
                    resetcorner();
                    put(c, 0, 11); put(c, 1, 10); put(c, 2, 9);
                    put(c, 9, 14); put(c, 10, 13); put(c, 11, 12);
                    put(c, 12, 23); put(c, 13, 22); put(c, 14, 21);
                    put(c, 21, 2); put(c, 22, 1); put(c, 23, 0);
                    for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    break;
                case "L'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(c, 0, 11); put(c, 1, 10); put(c, 2, 9);
                        put(c, 9, 14); put(c, 10, 13); put(c, 11, 12);
                        put(c, 12, 23); put(c, 13, 22); put(c, 14, 21);
                        put(c, 21, 2); put(c, 22, 1); put(c, 23, 0);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "L2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(c, 0, 11); put(c, 1, 10); put(c, 2, 9);
                        put(c, 9, 14); put(c, 10, 13); put(c, 11, 12);
                        put(c, 12, 23); put(c, 13, 22); put(c, 14, 21);
                        put(c, 21, 2); put(c, 22, 1); put(c, 23, 0);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "U":
                    resetcorner();
                    put(c, 0, 3); put(c, 1, 5); put(c, 2, 4);
                    put(c, 3, 6); put(c, 4, 8); put(c, 5, 7);
                    put(c, 6, 9); put(c, 7, 11); put(c, 8, 10);
                    put(c, 9, 0); put(c, 10, 2); put(c, 11, 1);
                    for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    break;
                case "U'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(c, 0, 3); put(c, 1, 5); put(c, 2, 4);
                        put(c, 3, 6); put(c, 4, 8); put(c, 5, 7);
                        put(c, 6, 9); put(c, 7, 11); put(c, 8, 10);
                        put(c, 9, 0); put(c, 10, 2); put(c, 11, 1);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "U2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(c, 0, 3); put(c, 1, 5); put(c, 2, 4);
                        put(c, 3, 6); put(c, 4, 8); put(c, 5, 7);
                        put(c, 6, 9); put(c, 7, 11); put(c, 8, 10);
                        put(c, 9, 0); put(c, 10, 2); put(c, 11, 1);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "F":
                    resetcorner();
                    put(c, 9, 7); put(c, 10, 6); put(c, 11, 8);
                    put(c, 6, 16); put(c, 7, 15); put(c, 8, 17);
                    put(c, 15, 13); put(c, 16, 12); put(c, 17, 14);
                    put(c, 12, 10); put(c, 13, 9); put(c, 14, 11);
                    for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    break;
                case "F'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(c, 9, 7); put(c, 10, 6); put(c, 11, 8);
                        put(c, 6, 16); put(c, 7, 15); put(c, 8, 17);
                        put(c, 15, 13); put(c, 16, 12); put(c, 17, 14);
                        put(c, 12, 10); put(c, 13, 9); put(c, 14, 11);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
                case "F2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(c, 9, 7); put(c, 10, 6); put(c, 11, 8);
                        put(c, 6, 16); put(c, 7, 15); put(c, 8, 17);
                        put(c, 15, 13); put(c, 16, 12); put(c, 17, 14);
                        put(c, 12, 10); put(c, 13, 9); put(c, 14, 11);
                        for (int i = 0; i < c.Length; i++) { c[i] = c_temp[i]; }
                    }
                    break;
            }

            cstring = null;
            for (int i = 0; i < c.Length; i++)
            {
                cstring += c[i];
            }
        }

        static void put(string[] array, int positionA, int positionB)
        {
            c_temp[positionB] = c[positionA];
        }

        public static void resetcorner()
        {
            for (int i = 0; i < c.Length; i++) //reset c_temp
            {
                c_temp[i] = c[i];
            }
        }

        public static bool opp(string x, string y)
        {
            if (x == "G" & y == "B")
            {
                return true;
            }
            if (x == "B" & y == "G")
            {
                return true;
            }
            if (x == "R" & y == "O")
            {
                return true;
            }
            if (x == "O" & y == "R")
            {
                return true;
            }

            return false;
        }
        public static void log(string text)
        {
            Console.WriteLine(" [*]  " + text);
        }
    }
}
