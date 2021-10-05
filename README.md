# Technalia

 - [Synopsis] (https://github.com/josharms00/TechnaliaVR/blob/main/README.md#synopsis)

## Synopsis

You crashland on an island inhabited by a mad scientist and his many inventions. Explore the island using gadgets you find in order to make it home.

![Start](/screenshots/start.PNG)

## Gameplay

As you go along you will be able to collect some gadgets that will assist you.

### Freeze Ray

![FoundFreezeRay](/screenshots/freezeray_found.PNG)

![FreezeRay](/screenshots/freezeray.PNG)

The terrain is generated with the Unity Terrain Tools package. The player can modify this terrain at runtime by using the freeze ray to add the ice texture to the scene. If the player steps on the ice they will gain momentum and they can use this momentum to make jumps they otherwise couldn't. The player will also need to use the freeze ray in order to complete certain tasks.

![FreezeRayShoot](/screenshots/freezeray_shooting.PNG)

### Diary Entries

![DiaryEntry](/screenshots/diaryentry.PNG)

There are pages from the mad scientist's dairy scattered throughout the island and they can give hints on how to approach certain obstacles.

![DiaryEntryRead](/screenshots/diaryentry_read.PNG)

### Spring Boots

![SpringBoots](/screenshots/springboots.PNG)

With these on the player can hold down the jump button in order to jump higher than before.

![SpringBoots](/screenshots/springboots_charging.PNG)

![ChargeJump](/screenshots/chargejump.PNG)

### Web Cannon

![WebCannon](/screenshots/WebCannon.PNG)

This can be used to shoot web balls that explode on impact.

![WebCannonShoot](/screenshots/webcannon_shoot.PNG)

![WebImpact](/screenshots/webimpact.PNG)

This works a similar way to the freeze ray as it will modify the terrain but place webs instead of ice. Once webs have been placed the player will become stuck
if they step on them. The only way to break out of the web is to either use the freeze ray to destroy the webs or use the spring boots and charge them enough to
break out. This can be used to your advantage to stick to walls and overcome certain obstacles.

![StuckToWall](/screenshots/stucktoawall.PNG)

## Assets Used

"Launcher" (https://skfb.ly/JIvQ) by Semih Parlayan.

"Simple Ray Gun" (https://skfb.ly/6XyDY) by Playblast.

"Lava Flowing Shader" (https://assetstore.unity.com/packages/vfx/shaders/lava-flowing-shader-33635) by Moonflowe Carnivore

"Coconut Palm Tree Pack" (https://assetstore.unity.com/packages/3d/vegetation/trees/coconut-palm-tree-pack-7888) by Baldinoboy

## How To Run

This project uses Unity's Open XR Toolkit so it technically should work will any headset but I only have an Oculus Quest to test with. I have included the .apk file in the releases. The easiest way to do it would be to use something like Side Quest to load the game onto the Quest. Once uploaded it will appear in the Unkown Sources section.
