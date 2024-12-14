# cs283-f24-assignments
Assignment framework for CS283 Game Programming
# Final Game project
## Nomad Life Simulator

![GamePoster](https://github.com/user-attachments/assets/59916187-96b0-461b-84de-3f6f07cb5f9f)

In the game you play as a shepherd. When you spawn you find out your carriage is broken so you need to go to the blacksmith to get it fixed.
Eventually you find yourself working in the farm

### Controls and how to play
WASD for movement

Pickup mechanics is used a lot in the game.

Press I key to pickup and item when you're in close vicinity of it. 

Press K to drop an item. You can't have more than 1 item in hand, if you press K while holding something else, it will get dropped.

![image](https://github.com/user-attachments/assets/bd0e1d40-f5ca-4c47-82f3-1f7c459a506c)


https://github.com/user-attachments/assets/923cd3c2-a60c-456a-90bb-71d31738e32d

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/Farmingvids.gif)

Game steps:
1. Go to the blacksmith in the woods
2. Interact in a dialogue
3. Go to the farm and come up to the farmer
4. Engage in dialogue
5. Go into the barn and pick up the seed on the table
6. Go to the empty plot of dirt and press K to plant the seed there
7. Go back to the barn and pick up the watering can
8. Go to the plot of dirt with sprouts and Press Space to water
9. Drop the watering can by pressing K and go to the plot with the vegetables
10. Press Space to initiate the picking cabbage Motion
11. Place the cabbage in the barn by pressing K
12. Get coins from the farmer
13. Run to the blacksmith

### Features
1. Implemented pickup and drop mechanics using mesh collider onStay method. CollectionGame.cs file
2. UI with Game tips and directions pops up depending on the Current State of the game and relevant locations. CollectionGame.cs
3. 2 NPCs using Behavior tree. Blacksmith and Farmer interact with the player once he's close enough and depending on the game progression, the interactions vary. I also implemented communication between the scripts to send info about the game progression. BehaviorFarmer.cs and BehaviorBlacksmith.cs files
4. Farming features: player needs to progress through the game in order so I use an enum to keep track of all actions including individual farming steps.
5. Planting the seed: Detecting if you're in the empty plot of dirt and drop the seed. CollectionGame.cs
6. Watering: detecting player position and enabling watering particle effect when Space is pressed. CollectionGame.cs
7. Cabbage picking: Pressing Space activates an animation, and then I make the player pick up a cabbage and have it in hand. CollectionGame.cs

### Assets
Huge thanks to the artist, Quaternius. Almost all assets in the game are from here https://quaternius.com/index.html

Assignment 10. BT and NPC controls

Horse jumps on you when you get too close to it and then retreats back when you get closer to your home hut.
The sheep runs away from you when you go towards it. If you can get close enough to the sheep and press "K" key, you can kill it and it will disappear(tried to do sophisticated animations to demonstrate dying but rotation controls kept getting messed up).
Link to video https://drive.google.com/file/d/1WnG_OkLOYLHCuvdiG1hIYVNG6ZK77EdN/view?usp=sharing


Assignment 9. NPC mechanics and Navmesh
Link to video https://drive.google.com/file/d/1ofy6TFcA5frSQcS6H7liIpSHGVLgkiGH/view?usp=sharing

Assignment 8. Coin collection and spawner
Link to video https://drive.google.com/file/d/1_QL-neLHN6wO4JT7qUlfdkMiPgLscIm2/view?usp=sharing

Assignment 7. Animated character controls and collider implementation

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/CharacterControlAnimated.gif)


Assignment 6. 
Follow Path Linear

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/A06FollowLinear.gif)

Follow Path Cubic

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/A06FollowCubic.gif)

Gaze control

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/A06JointFollow.gif)

New Model used: https://poly.pizza/m/5EGWBMpuXq

Assignment 5: Rigid Follow and Spring Follow camera scripts

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/RigidFollow.gif)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/SpringFollow.gif)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/ParticleEffect.gif)

Assignment 4: Flythrough camera and POI Tour

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/POI_Tour.gif)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/Wholeview.png)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/Trees.png)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/Cat_on_mountain.png)

![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/Pond_mountain.png)

New models used:
https://assetstore.unity.com/packages/3d/characters/animals/lowpoly-toon-cat-lite-66083

Assignment 3. Game level prototype for a nomad lifestyle simulation game.
![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/whole_scene.jpeg)
![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/sheeps_and_horse.jpeg)
![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/bridge_and_trees.jpeg)
![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/sheep_animation.gif)
![](https://github.com/saniya05m/cs283-f24-assignments/blob/main/sceneview.gif)

Models used:
https://sketchfab.com/3d-models/stone-bridge-a5d380cd08654b508b4b643056038605
https://sketchfab.com/3d-models/low-poly-horse-d1ca14c1befe47c8a4686d2f64c82f45
https://sketchfab.com/3d-models/mongolian-hunting-yurt-433219c2c7b14c37ab74b430f462ce5c
https://poly.pizza/m/0GzMb32v3oH




