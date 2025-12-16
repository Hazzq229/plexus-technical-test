
# Collect and Progression Mechanic Prototype

A mechanic prototype demo created for Plexus TechDev Studio internship application technical test. 


## Specification

In this playable demo you can
- Move the player using WASD
- Level up by collecting gems that spawned into the scene
- XP requirement also increased when player leveled up

Some programming patterns and used in this demo
- Singleton pattern in script ExperienceManager.cs to provide other script easy access when need to modify its variable value  
- State pattern implemented using Finite State Machine (UnityHFSM package) to control player character state in script PlayerController2D.cs
- Observer pattern used to notify script ProgressionUI.cs when player leveled up and xp increased
## Acknowledgements

Some assets and Unity Package used in this demo
 - [UnityHFSM by Inspiaaa](https://github.com/Inspiaaa/UnityHFSM)
 - [Mystic Woods by Game Endeavor](https://game-endeavor.itch.io/mystic-woods)
 - [Pixel Art Gem Pack - Animated by karsiori](https://assetstore.unity.com/packages/2d/environments/pixel-art-gem-pack-animated-277559)

