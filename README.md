
# OptiCube
A near-optimal 2x2 Rubik's Cube solver using a Breadth-First Search Algorithm and group theory.

## How does it work?
This program utilizes a breadth-first search algorithm and group theory to find the shortest path from a scrambled state (group G1) to a state with all corners oriented (subgroup G2). From here, we restrict the search space to moves that preserve corner orientation, then BFS again until we reach a solved state (G3).

## Instructions
- Download the latest release of OptiCube
- Navigate to the download location, and open a CMD window there
- Run `opticube.exe <2x2 scramble>`

## Example
    opticube R F' R' F' R2 F2 R2 U' F'

	[*]   - searching depth 1
    [*]   - searching depth 2
	[*]   - searching depth 3
	[*]   - searching depth 4
	[*]   - searching depth 5
	[*]   - searching depth 6

	[*]   checked 722113 states in 773ms (933824.208332805 per second)
	[*]   stage 1: R' F U2 F U' R

	[*]   - searching depth 1
	[*]   - searching depth 2
	[*]   - searching depth 3
	[*]   - searching depth 4
	[*]   - searching depth 5
	[*]   - searching depth 6
	[*]   - searching depth 7
	[*]   - searching depth 8
	[*]   - searching depth 9
	[*]   - searching depth 10

	[*]   checked 17033727 states in 21194ms (803700.977282363 per second)
	[*]   stage 2: R2 U F2 U' F2 U R2 U' F2 U

## Main Logic:

```
// desired state for forcing Sune CLL is nxxnxxnxYYxxWRGWOGWOBWRB
// where n = sticker must not be solved in the end
// and   x = sticker's end color is irrelevant

while (!issolved)
{
    depth++;
    log(" - searching depth " + depth);
    Variations<char> variations = new Variations<char>(validmoves, depth, GenerateOption.WithRepetition); //generate variations to test

    foreach (IList<char> test_sequence in variations)
    {
        foreach (var move in test_sequence)
        {
            test_sequence += move + " ";
            update_cube(move);
        }

        if (statesEqual(cornerstate_initial, cornerstate_desired))
        {
            solved();
            return seq;
        }
        seq = ""; //seq didn't work so reset
        Buffer.BlockCopy(corner_backup, 0, corner_init, 0, corner_init.Length * sizeof(char)); //restore original state pre-testing
    }
}
```
Even on mediocre hardware, this program is capable of searching about 1,800,000 branches per second. So solve times vary from only a few seconds to about two minutes depending on the move depth of the solution to the cube.

