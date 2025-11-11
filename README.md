# sokobanplus
video demo:

live demo:

# what is it
it is a sokoban type math game where you have to use the 4 basic operations (add, sub, mul, div) to solve puzzles

# how to play 
the next level will automatically load after completing the current level (sorry only had time for 5 levels)

there are 3 types of blocks:
- wall (black): prevents movement
- number block (orange): variables/stores data
- operation block (red): push a number block underneath this and type a command (look below for instructions) to apply it
- goal block (green): push a number block with the same goal block to satisfy the goal. when all goals have been completed, you win! 

controls:
- R: restart level
- WASD: move player
- SHIFT + 'ADD' or 'SUB' or 'MUL' or 'DIV': for all number blocks on operation blocks, the number block's new value = (number block's value) (OPERATION) (operation block value)

ai discolsoeure: i used ai for the level string parser nothing else
