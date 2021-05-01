# Final-Project
Final Project for Game Production
 

Game Notes & Bugs: 

How to Play:
Explore the Magician’s Meadows, Suspicious Sands and Powdery Peaks along with the Under-Cat Hero to find, defeat and undo the Dog Wizard’s spell he has placed on the slime enemies in each area. 
1.	The player must cure all the yellow infected trees and slimes within Magician’s Meadows.
2.	Find and obtain the “Purrr”culiar Pendant in the Suspicious Sands and cure all the slimes in the area.
3.	Defeat the Dog Wizard in the Powdery Peaks in a “ruff” battle and cure all the slimes in the area.

Getting Started:
●	Right click to switch between 3rd and 1st person camera views. If using a controller, press in the right analog stick.
●	Press “Q” to use your sword attack, “E” to use your ranged attack and “R” to use your magical restoration ability. If using a controller, press “X” for your sword attack, the right bumper for your ranged attack, and “Y” for your magical restoration ability.
●	Restoration ability only works on enemies that are at 0 hp.
●	Use “Esc” to quit the game, or if using a controller, the back button.
●	Use “P” to pause and unpause the game, or the start button if using a controller.

Build Notes: 
There are only a handful of bugs remaining, which happen occasionally.
●	Sometimes the sword will instantly make an enemy’s health 0, sometimes it doesn’t. This is unintentional and the cause is unknown.
●	Sometimes the cure ability goes right through the infected trees and sometimes it doesn’t. This is also unintentional, but isn’t a pressing concern because the player’s mana will just regenerate and they can try again.
●	If the player really tries, they can lure the desert slimes to the rocks around where the amulet is and the slimes will clip straight through the rocks, even though both the rocks and the slimes are Rigidbodies (not Kinesthetic), both have colliders (neither are triggers) attached, AND the rocks are also set to “Not Walkable” for the slime’s NavMesh.
○	Similarly the slimes in Magician’s Meadows can move through the tree stump props despite efforts to stop them, however, this does not enable the slimes to get into game-breaking areas.
The Magician’s Meadows has a particle effect used for each infected tree and also a particle effect used after each is cured as well as a particle effect used after each slime is cured (Accredited to Jean Moreno for the Cartoon FX Free Particle effects). 
The Suspicious Sands has a dust particle effect (accredited to Unity’s Standard Assets package).
The Powdery Peaks has a snow particle effect and a smoke particle effect created by Anna Branch.

Credits:
Jean Moreno for the Cartoon FX Free Particle https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-free-109565

Unity Standard Assets Unity Technologies 
unity3d.com



Basic Requirements: 

●	The game is played in 3rd person view in Unity 3D. It also has a First-Person mode that is toggleable by right clicking (pressing down the Right Analog Stick on Xbox One controller).
●	The game has both keyboard / mouse and Xbox One controller support.
●	The game has UI elements or a HUD system that indicates important information.
●	The game has an overall story and each stage has a story / narrative that contributes to the overall story / narrative of the game (more information on stages is found below).
●	The game has audio and sound effects.
●	Gameplay takes roughly 5 minutes to complete and it is relatively easy to successfully complete the game within that time frame.
●	The game has a tutorial / hub level and 3 individual stages that can be completed in any order; the player is able to return to the hub at any time.
●	The game has a "win state" that is achieved by reaching the "win condition" on each stage. Once all three stages are beaten, the game will automatically display the “You Win!” screen and will keep displaying it every time the game is opened again until the player returns to the main menu and deletes their save data.
●	The loss state for each level is that the player must lose all their health. The level will automatically restart and all of the player’s stats are reset to their maximum values.
●	The game actively manages the player’s progression. Not only does the game remember level progression while the game is being played, the game also saves your progress every time you use a teleporter, win a level, or exit the game by pressing ESC (the cascading windows button on the Xbox One controller) and remembers which levels you have beaten upon closing and opening the game up again.
●	The game is a refined and complete experience.
Level Requirements: 
●	The game has a hubworld as mentioned above.
●	The hub enables players to traverse the 3 levels in any order.
●	A “How to Play” segment is incorporated into the tutorial on bootup.
●	Each segment can be accessed via the hub and each level takes no more than a minute to complete.
●	Specific win conditions are in place for each level. Magician’s Meadow: Cure slimes and infected trees. Suspicious Sands: Collect the “Purrr”culiar Pendant and cure all slimes. Powdery Peaks: Defeat and cure the Dog Mage and all of his spawned minions.
●	The game has its own unified art style.
●	A minimum of 10 models are in each level.
●	Each level has a main animation.
●	Each level has a unique particle effect.
●	Each level has a unique mechanic.



Art Requirements: 

The art is fully finished within the game. 
The art segment of the rubric is completed and fulfilled as it includes the following:
●	The Player Character has 3 animations: Idle, hit, and walk.
●	10 models minimum are within each of the three levels.
●	One main animation is within each of the three levels: 
Magician’s Meadow has the slime enemy’s idle and chase animations. 
Suspicious Sands has a tumbleweed animation. 
Powdery Peaks has the Dog Wizard’s idle and hit animations.
●	Each of the three levels has a main particle effect: 
The Magician’s Meadows has a particle effect used for each infected tree and also a particle effect used after each is cured as well as a particle effect used after each slime is cured (Accredited to Jean Moreno for the Cartoon FX Free Particle effects). 
The Suspicious Sands has a dust particle effect (accredited to Unity’s Standard Assets package).
The Powdery Peaks has a snow particle effect and a smoke particle effect created by Anna Branch.

Credits:
Jean Moreno for the Cartoon FX Free Particle https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-free-109565

Unity Standard Assets Unity Technologies 
unity3d.com

●	Menus reflect the style of the models. Start screen image illustrated by Eric Savage and implemented by tech.
●	Original music arrangements for all three levels are created by Emma Jean Branch.

Tech Requirements:

Game’s level and menus were designed, in full, by the tech members. The requirements for the tech portions of the game are fulfilled and completed as follows:
The following menus are finalized:
●	Main menu with "Start Game," "How to Play”, and "Quit" buttons as well as the addition of a “Delete ALL Saved Data” button.
●	"How to Play" screens with controls for both PC and game controller, in addition to a credits section.
●	Pause Menu with information on how to play the game on both PC and controller as well as credits.
●	Win / Loss menus that return to the main menu.
●	No dead ends in the level or menus: the player does not get "stuck" anywhere in the levels or menus.


Code Requirements:

●	The requirements for the coding portions of the project are all fulfilled and completed as follows:
●	The player is able to use all three significant mechanics: Sword, Bow and Curing. The sword is for close combat, the bow is for distance combat and the curing mechanic is for curing slime enemies as well as infected trees.
●	The game is pausable by pressing the “P” button (and on Xbox One controller it is the “Start” button). Not only that, but the display text on the pause menu automatically changes based on whether or not an Xbox One controller is detected. If an Xbox One controller is detected, it will say “press Start to unpause”, but if there is no Xbox One controller it will say “Press P to unpause”.
●	The game does not crash or break due to programming errors from what we have tested.
●	Programming for the game is completely finalized.

