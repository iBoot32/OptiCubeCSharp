using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCube
{
    class cubestringcalc
    {
        /** 
         * 
         *     cubestringcalc
         *   
         *     calculate a cornerstate and 
         *     edgestate when given a scramble.
         *     
         *     used in tons of my programs.
         *     
         *     by Tom O'Donnell 2021
         *     
         */

        public static string corner = "YRBYOBYOGYRGWRGWOGWOBWRB"; //white on bottom for 2x2
        public static string cornerstring;
        public static string[] cornerarray = new string[24];
        public static string[] othertemp = new string[24];

        public static void calc (string[] args)
        {
            for (int i = 0; i < 24; i++)
            {
                cornerarray[i] = corner.Substring(i, 1);
            }


            foreach (var move in args)
            {
                manipulate(move);
            }
            //log(cornerstring);
        }

        public static void manipulate(string move)
        {
            switch (move)
            {
                case "R":
                    resetcorner();
                    put(cornerarray, 3, 20); put(cornerarray, 4, 19); put(cornerarray, 5, 18); //put corner 2 into corner 7
                    put(cornerarray, 18, 17); put(cornerarray, 19, 16); put(cornerarray, 20, 15); //put corner 7 into corner 6
                    put(cornerarray, 15, 8); put(cornerarray, 16, 7); put(cornerarray, 17, 6); //put corner 6 into corner 3
                    put(cornerarray, 6, 5); put(cornerarray, 7, 4); put(cornerarray, 8, 3); //put corner 3 into corner 2
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }

                    break;
                case "R'":
                    for (int z = 0; z < 3; z++) //R' is the same as R R R
                    {
                        resetcorner();
                        put(cornerarray, 3, 20); put(cornerarray, 4, 19); put(cornerarray, 5, 18); //put corner 2 into corner 7
                        put(cornerarray, 18, 17); put(cornerarray, 19, 16); put(cornerarray, 20, 15); //put corner 7 into corner 6
                        put(cornerarray, 15, 8); put(cornerarray, 16, 7); put(cornerarray, 17, 6); //put corner 6 into corner 3
                        put(cornerarray, 6, 5); put(cornerarray, 7, 4); put(cornerarray, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "R2":
                    for (int z = 0; z < 2; z++) //R2 is the same as R R
                    {
                        resetcorner();
                        put(cornerarray, 3, 20); put(cornerarray, 4, 19); put(cornerarray, 5, 18); //put corner 2 into corner 7
                        put(cornerarray, 18, 17); put(cornerarray, 19, 16); put(cornerarray, 20, 15); //put corner 7 into corner 6
                        put(cornerarray, 15, 8); put(cornerarray, 16, 7); put(cornerarray, 17, 6); //put corner 6 into corner 3
                        put(cornerarray, 6, 5); put(cornerarray, 7, 4); put(cornerarray, 8, 3); //put corner 3 into corner 2
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "L":
                    resetcorner();
                    put(cornerarray, 0, 11); put(cornerarray, 1, 10); put(cornerarray, 2, 9);
                    put(cornerarray, 9, 14); put(cornerarray, 10, 13); put(cornerarray, 11, 12);
                    put(cornerarray, 12, 23); put(cornerarray, 13, 22); put(cornerarray, 14, 21);
                    put(cornerarray, 21, 2); put(cornerarray, 22, 1); put(cornerarray, 23, 0);
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }

                    break;
                case "L'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 11); put(cornerarray, 1, 10); put(cornerarray, 2, 9);
                        put(cornerarray, 9, 14); put(cornerarray, 10, 13); put(cornerarray, 11, 12);
                        put(cornerarray, 12, 23); put(cornerarray, 13, 22); put(cornerarray, 14, 21);
                        put(cornerarray, 21, 2); put(cornerarray, 22, 1); put(cornerarray, 23, 0);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "L2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 11); put(cornerarray, 1, 10); put(cornerarray, 2, 9);
                        put(cornerarray, 9, 14); put(cornerarray, 10, 13); put(cornerarray, 11, 12);
                        put(cornerarray, 12, 23); put(cornerarray, 13, 22); put(cornerarray, 14, 21);
                        put(cornerarray, 21, 2); put(cornerarray, 22, 1); put(cornerarray, 23, 0);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "U":
                    resetcorner();
                    put(cornerarray, 0, 3); put(cornerarray, 1, 5); put(cornerarray, 2, 4);
                    put(cornerarray, 3, 6); put(cornerarray, 4, 8); put(cornerarray, 5, 7);
                    put(cornerarray, 6, 9); put(cornerarray, 7, 11); put(cornerarray, 8, 10);
                    put(cornerarray, 9, 0); put(cornerarray, 10, 2); put(cornerarray, 11, 1);
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    break;
                case "U'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 3); put(cornerarray, 1, 5); put(cornerarray, 2, 4);
                        put(cornerarray, 3, 6); put(cornerarray, 4, 8); put(cornerarray, 5, 7);
                        put(cornerarray, 6, 9); put(cornerarray, 7, 11); put(cornerarray, 8, 10);
                        put(cornerarray, 9, 0); put(cornerarray, 10, 2); put(cornerarray, 11, 1);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "U2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 3); put(cornerarray, 1, 5); put(cornerarray, 2, 4);
                        put(cornerarray, 3, 6); put(cornerarray, 4, 8); put(cornerarray, 5, 7);
                        put(cornerarray, 6, 9); put(cornerarray, 7, 11); put(cornerarray, 8, 10);
                        put(cornerarray, 9, 0); put(cornerarray, 10, 2); put(cornerarray, 11, 1);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "D":
                    resetcorner();
                    put(cornerarray, 12, 15); put(cornerarray, 13, 17); put(cornerarray, 14, 16);
                    put(cornerarray, 15, 18); put(cornerarray, 16, 20); put(cornerarray, 17, 19);
                    put(cornerarray, 18, 21); put(cornerarray, 19, 23); put(cornerarray, 20, 22);
                    put(cornerarray, 21, 12); put(cornerarray, 22, 14); put(cornerarray, 23, 13);
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }

                    break;
                case "D'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(cornerarray, 12, 15); put(cornerarray, 13, 17); put(cornerarray, 14, 16);
                        put(cornerarray, 15, 18); put(cornerarray, 16, 20); put(cornerarray, 17, 19);
                        put(cornerarray, 18, 21); put(cornerarray, 19, 23); put(cornerarray, 20, 22);
                        put(cornerarray, 21, 12); put(cornerarray, 22, 14); put(cornerarray, 23, 13);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "D2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(cornerarray, 12, 15); put(cornerarray, 13, 17); put(cornerarray, 14, 16);
                        put(cornerarray, 15, 18); put(cornerarray, 16, 20); put(cornerarray, 17, 19);
                        put(cornerarray, 18, 21); put(cornerarray, 19, 23); put(cornerarray, 20, 22);
                        put(cornerarray, 21, 12); put(cornerarray, 22, 14); put(cornerarray, 23, 13);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "F":
                    resetcorner();
                    put(cornerarray, 9, 7); put(cornerarray, 10, 6); put(cornerarray, 11, 8);
                    put(cornerarray, 6, 16); put(cornerarray, 7, 15); put(cornerarray, 8, 17);
                    put(cornerarray, 15, 13); put(cornerarray, 16, 12); put(cornerarray, 17, 14);
                    put(cornerarray, 12, 10); put(cornerarray, 13, 9); put(cornerarray, 14, 11);
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    break;
                case "F'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(cornerarray, 9, 7); put(cornerarray, 10, 6); put(cornerarray, 11, 8);
                        put(cornerarray, 6, 16); put(cornerarray, 7, 15); put(cornerarray, 8, 17);
                        put(cornerarray, 15, 13); put(cornerarray, 16, 12); put(cornerarray, 17, 14);
                        put(cornerarray, 12, 10); put(cornerarray, 13, 9); put(cornerarray, 14, 11);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "F2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(cornerarray, 9, 7); put(cornerarray, 10, 6); put(cornerarray, 11, 8);
                        put(cornerarray, 6, 16); put(cornerarray, 7, 15); put(cornerarray, 8, 17);
                        put(cornerarray, 15, 13); put(cornerarray, 16, 12); put(cornerarray, 17, 14);
                        put(cornerarray, 12, 10); put(cornerarray, 13, 9); put(cornerarray, 14, 11);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "B":
                    resetcorner();
                    put(cornerarray, 0, 22); put(cornerarray, 1, 21); put(cornerarray, 2, 23);
                    put(cornerarray, 21, 19); put(cornerarray, 22, 18); put(cornerarray, 23, 20);
                    put(cornerarray, 18, 4); put(cornerarray, 19, 3); put(cornerarray, 20, 5);
                    put(cornerarray, 3, 1); put(cornerarray, 4, 0); put(cornerarray, 5, 2);
                    for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }

                    break;
                case "B'":
                    for (int z = 0; z < 3; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 22); put(cornerarray, 1, 21); put(cornerarray, 2, 23);
                        put(cornerarray, 21, 19); put(cornerarray, 22, 18); put(cornerarray, 23, 20);
                        put(cornerarray, 18, 4); put(cornerarray, 19, 3); put(cornerarray, 20, 5);
                        put(cornerarray, 3, 1); put(cornerarray, 4, 0); put(cornerarray, 5, 2);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
                case "B2":
                    for (int z = 0; z < 2; z++)
                    {
                        resetcorner();
                        put(cornerarray, 0, 22); put(cornerarray, 1, 21); put(cornerarray, 2, 23);
                        put(cornerarray, 21, 19); put(cornerarray, 22, 18); put(cornerarray, 23, 20);
                        put(cornerarray, 18, 4); put(cornerarray, 19, 3); put(cornerarray, 20, 5);
                        put(cornerarray, 3, 1); put(cornerarray, 4, 0); put(cornerarray, 5, 2);
                        for (int i = 0; i < cornerarray.Length; i++) { cornerarray[i] = othertemp[i]; }
                    }
                    break;
            }

            cornerstring = "";
            for (int i = 0; i < cornerarray.Length; i++)
            {
                cornerstring += cornerarray[i];
            }
        }

        public static void resetcorner()
        {
            for (int i = 0; i < cornerarray.Length; i++) //reset othertemp
            {
                othertemp[i] = cornerarray[i];
            }
        }

        static void put(string[] array, int positionA, int positionB)
        {
            othertemp[positionB] = array[positionA];
        }

        public static void log(string text)
        {
            Console.WriteLine("  " + text);
        }
    }
}
