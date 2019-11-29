# Gyroscope Maze - Gyroll
A maze game made in unity with a custom made gyroscope controller.

![Demo](demo/demo.gif "Playing with the gyroscope maze")

# Components and Dependencies
- Elegoo Ardiuno nano clone
- MPU6050 Gyroscope
- Arduino nano complier
- Unity version 2018.2.2f1
- [i2cdevlib](https://github.com/jrowberg/i2cdevlib) library for the gyroscope interrupter
- [Elegoo CH340 chip/driver](https://www.elegoo.com/download/)
- ATmega328P chip/driver for the ardiuno



# Introduction

Our group started with the idea of taking advantage of the wide array of Arduino attachments to make a video game with a unique controller. We decided to take a simple and recognizable game and make it more engaging, by implementing a virtual version of the marble maze game controlled with a gyroscope. We thought that this unique controller would allow for natural navigation of the maze. To increase excitement and replayability, we added several obstacles and implemented a maze generation algorithm to create a new maze every time. Our approach to problem solving was a mix between the “divide and conquer” and “pair programming” methods. For every part of the project, there were at least two people working on it in parallel and both had an understanding of that part. This allowed us to be efficient because we were able to work independently, and if we ran into problems we always had at least one other person to review the code and help debug. 

# Background Research

The main bulk of our research pertained to the Unity game. We referred to basic video tutorials to create a base structure for games in Unity, (as several of our group members had relatively little experience with Unity) specifically with physics and user-interface elements. Additionally, we needed to conduct research using the official Unity documentation to aid with the creation of Prefabs —  game objects with complete components that can be instantiated at runtime. While many game developers might never need to use this more complex tool, it was necessary for our group in order to integrate the gyroscope and maze generation algorithm into the game. We also had to examine code that helped with the implementation of our depth-first search maze generation algorithm, which we then adapted and altered for our project.

For the gyroscope and arduino implementation we originally followed instructables.com tutorial (LINK TO REFERENCE GOES HERE) which allowed us to get a simple Processing 3 project working with gyroscope input. Unfortunately this tutorial had several issues so we ended up using an example project on the github project, i2cdevlib (LINK TO REFERENCE GOES HERE). We modified this example project and ported the corresponding Processing 3 Project to Unity. ADD MORE ABOUT RESEARCH FOR BUTTON 

# Implementation

## Hardware
For the hardware components of this project we used an Arduino Nano, an MPU6050 Gyroscope, a button, all connected together via manual wiring on a compact breadboard. The controller sends data packets containing a prefix key and the information to the Unity game where the packets are identified appropriately and parsed. This is all done by uploading an arduino script that continuously reads rotational change and angular position from the pins offered by the gyroscope. The built-in digital motion processor in the gyroscope is then used as the interrupter to filter out garbage data that would cause the yaw drift. We then output the raw data of the gyroscope in terms of quaternions using the library, i2cdevlib (LINK).  Finally, we write the data to the serial that the arduino is connected to. The output to the serial port is then read by the game to be processed. Similarly, the button input is implemented in the same way with a different prefix token to differentiate it from angular data.

## Software
We built our game using the Unity Game Engine. Within Unity, a majority of the functionality is through our C# scripts. We implemented a flexible object-oriented structure to aid in organization and clarity, and to facilitate Agile development. Within this structure, we created classes to handle each of the main game systems, and sub-classes within those classes for each additional feature we added. We also made extensive use of Prefabs rather than drag-and-drop objects, as it was necessary for integration with the gyroscope and allowed for more flexibility and control. Additionally, we used downloaded assets from the Unity Asset Store for some of the graphics and user-interface elements. For the gameplay aspect of our product, we implemented a score-tracker, timer, obstacles, and a win screen. We also made a settings menu, where the user can customize the game difficulty, timer, sensitivity, and allowed for the option to play using the arrow keys rather than the gyroscope. 

# Group members’ contribution

Kevin Li:
Started the Unity project and created the basic elements of the game (Ball, platform, walls) 
Created a base for the user-interface on Unity  
Worked on graphics and game-object design 
Used the built-in Unity engine to create the game scenes
Miscellaneous tasks working with Unity C# scripts (e.g Implementing the quit button)

Robert Craig:
Implemented several obstacles in the maze.
Partially designed the code refactor that allowed us to implement the various obstacles. 
Helped create the script that is running on the arduino 
Helped create the initial version of the script that parses the arduino input in Unity
Helped implement the automatic maze generation algorithm, which uses the output of the Depth First Search algorithm to create walls in a 3d space.

Ibraheem Aboulnaga:
Implemented core game systems (level building, collisions, etc.)
Designed the in-game physics system, forking off of the built-in engine
Worked on integration between gyroscope/algorithm and the core game/menus
Implemented systems for the menu and level handling
Worked on implementing in-game obstacles

Ri Xin Yang:
Reworked gyroscope controller (hardware and software) by adding a physical button, reworked wiring design for a more transparent flow
Worked on the integration of the gyroscope and the game with the arduino script
Helped design major refactor of that allowed the team to work easier in agile and  OOP development environment
Maintained git repositories to ensure a smooth workflow throughout the development process
Worked on parts of the backends for the controller, such as auto port connections, cross platform port filtering and button input processing

# Final Product Evaluation

Overall, our final product was quite successful relative to our original design goals. We successfully implemented a timer, a score function, a settings screen, and custom obstacles (which was an additional feature not initially thought of). Furthermore, we allowed for the user to control the game difficulty, control sensitivity, and recalibrate the gyroscope if needed. The graphics also turned out well, where we made thorough use of the Unity Asset Store, custom shaders, and Unity UI features. Finally, our gyroscope integration with Unity exceeded expectations, working seamlessly to create a smooth gameplay experience. However, there were also a few features we initially planned on adding that never made it to the final product. Some examples include: Bluetooth compatibility, a physical case for the controller, and gameplay continuity after a player finishes a level (currently, finishing a level brings the player to another menu screen, rather than immediately to another level). 

# Design Trade-offs
One main trade-off we made was that we decided that a fully featured controller and case was an unnecessary amount of work. Instead we were able to focus more on the features of the game such as the complete game UI and several obstacles. Another trade-off we decided on was making our platform floor completely opaque rather than translucent. We decided that although this would inhibit the user from seeing the maze with the gyroscope tilted upside-down, we wanted to discourage this behaviour anyway as it would cause the gyroscope inputs to behave funnily. Additionally, an opaque floor made the overall lighting more pleasant. 

A very important change was our complete refactor of the physics in the project. Originally, we had the game’s maze object move along with the gyroscope, as a direct mapping of the gyroscope’s location. However, the Unity engine had difficulties with accurately calculating physics while using this method, causing game-breaking issues like the ball clipping through the maze entirely. We spent a very significant amount of time trying to get around this poor interaction, trying everything from changing the way we did collision detection, all the way to writing our own proprietary scripts to calculate collisions for specific game objects. All the solutions produced substandard results, however, and eventually, we completely refactored our physics around a unique idea: instead of rotating the platform, moving the gyroscope instead rotates both the direction of gravity and the position of the camera relative to the platform, while the background remains fixed. This gives the illusion that the platform is rotating, when in fact, everything else is. This improved the physics significantly, but had the unfortunate side effect of making some of our other features unachievable - for example, we can no longer have the ball fall out of the maze.

In another notable change, our original plan for the game had the mazes generating dynamically; instead of being sent to a score screen after completing one maze, the player would be able to complete many mazes in a row, each with increasing difficulty. However, this proved to be very difficult to implement smoothly and would block off what we wanted to do with our physics refactor. So instead, in our final product, after completing each maze, the player is presented with their score and is returned to the main menu, and the player can set the difficulty however they want.

# Future Work

In the future, we may continue to work on the current product to make it a more polished game. Because of time restraints, there are still many things we can do to improve the product. Specifically, we could improve the UI of the menu and how the game looks in general. Something else that could be done is to make the controller more user friendly, encapsulate it in a case and make the attached button easier to press.
We had thought of distributing the product, but there are some conflicts that are stopping us from doing that. For example, the game is dependent on our custom arduino controller, which we cannot easily distribute. 
We want to add variable sized mazes that change based on difficulty. The current state of the game almost supports it but there are various bugs that we have spent many days trying to resolve. We will leave this feature as a future plan. 
