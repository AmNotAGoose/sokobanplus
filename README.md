# sokobanplus
(new) video demo:
https://youtu.be/Ig1ghsAwlyM

(new) live demo:
https://amnotagoose.itch.io/sokoban-hmm

# what is it
it is a sokoban type math game where you have to use the 4 basic operations (add, sub, mul, div) to solve puzzles

# how to play 
the next level will automatically load after completing the current level (sorry only had time for 5 levels)

there are 3 types of blocks:
- wall (black): prevents movement
- number block (orange): variables/stores data
- operation block (red): push a number block underneath this and type a command (look below for instructions) to apply it
- goal block (green): push a number block with the same number goal block to satisfy the goal. when all goals have been completed, you win!
- subgoal block (light green) push a number block with the same number to satisfy the subgoal. some blocks may react to it.
- conditional wall block (light grey) checks in a 3x3 space around it. all subgoals in that area must be satisfied for it to become permeable, otherwise it is solid.

controls:
- R: restart level
- WASD: move player
- SHIFT + 'ADD' or 'SUB' or 'MUL' or 'DIV': for all number blocks on operation blocks, the number block's new value = (number block's value) (OPERATION) (operation block value)

ai discolsoeure: i used ai for the level string parser nothing else
