Design choices:
I implemented a simple pool object system so that the game runs smoothly without any performance issue

I ended up using a combination of UnityUI and physics system because i wanted to utilize the automatic layout system that present in UnityUI,
and by using that automatic layout system i don't really need to code a system to put each block in their correct position because the automatic layout system has already done that.

I also incorporated a color changing feature when you hit a block as a hit feedback and differentiate between which block that are going to be destroyed 
and the one that still need to be hit multiple times


Challenges:
I didn't face any challenges other than thinking on how to implement the different block pattern feature everytime the level resets
and so i just keep it simple and implement a randomized pattern based on the emergence behavior of multiple BlockRow using the automatic layout system
by essentially randomized a number of blocks that we want to "spawn" and just activate that set amount of blocks in each of the BlockRow,
and the result is a nice randomized block pattern everytime the level reset
