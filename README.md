# OptiCube
A near-optimal 2x2 Rubik's Cube solver using a Breadth-First Search Algorithm

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

     [*]   searched 33253 branches in 101ms (328146.617483653 per second)
     [*]   cube solution: R U R' F' R L F' L' F L' U' L

## How does it work?
This program utilizes a breadth-first search algorithm to find the shortest path from a scrambled state to a Sune CLL case, then subsequently solves the CLL.

## Main Logic:

```
// desired state for forcing Sune CLL is nxxnxxnxYYxxWRGWOGWOBWRB
// where n = sticker must not be solved in the end
// and   x = sticker's end color is irrelevant

while (!issolved)
{
    depth++;
    log(" - searching depth " + depth);
    Variations<char> variations = new Variations<char>(validmoves, depth, GenerateOption.WithRepetition);

    foreach (IList<char> test_sequence in variations)
    {
        foreach (var move in test_sequence)
        {
            seq += move + " ";
            update_cube(move);
        }

        if (statesEqual(cornerstate_initial, cornerstate_desired))
        {
            solved();
            return seq;
        }
        seq = ""; //seq didn't work so reset

        for (int z = 0; z < corner_initial.Length; z++)
        {
            corner_initial[z] = corner_backup[z]; //restore original cube state after testing
        }
    }
}
```

Yes, forcing a Sune CLL is significantly more computation-heavy than just forcing a solved layer, but sorry I'm too lazy to hardcode every CLL.

Even on mediocre hardware, this program is capable of searching about 300,000 branches per second. So solve times vary from only a few seconds to about two minutes depending on the move depth of the solution to the cube.


