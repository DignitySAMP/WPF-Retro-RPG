# Retro RPG in C# WPF

This project was made ahead of my finals in my first year as a software developer. I used it as a way to test and showcase my current progress within C# WPF, as well as using it as a study method. This is VERY early work in progress, the basic framework has been added but has only been briefly tested.

It incorporates a lot of notable C# features, such as window management, classes, filestreams, programmed xaml, cross referencing, and more. It's inspired by Ultima 4, as well as a similar repository that I found a while ago but can no longer pin down. All assets are made by other parties and I take no credit for them. 

## Preview

![Main Menu](https://github.com/user-attachments/assets/440870da-58b8-4d82-8451-6442603c7f48)
![Introduction Cutscene](https://github.com/user-attachments/assets/fb723b0c-d53a-47a6-b0b0-c6ac0e98e3f3)
![Game window](https://github.com/user-attachments/assets/92d517d8-d26b-49b4-8d38-d6fa5796771d)
![Developer Map Editor](https://github.com/user-attachments/assets/81a656ed-383d-4e17-b340-12f5c45a3bec)

## Features

- Uses multiple windows: this is an inefficient approach but I haven't gotten around solving it yet.
- Gameframe XAML is built dynamically throughg C#, depending on game state.
- Sprites are loaded from a spliced Ultima 4 texture tilesheet. They're all loaded individually from the sprites/tiles folder.
- Tiles are rendered from a .txt file using FileStreams (see content/map.txt for an example)
- Simple "history" frame that tracks which tile you clicked on, or can be used to give Quest or Interior information (unfinished)
- All tiles are selectable and the game remembers which tile was clicked.
- There's a simple player interface frame at the top right. The framework that allows you to switch between them is done and are ready for additional functionality.
- There are a few interaction buttons available, these can be clicked and have distinction in the code but don't have any functionality yet. The framework for this is done.
- The introduction cutscene is voicelined using an AI text to speech and uses a retro typing effect.
- A simple developer map builder included, showcases all tiles and allows you to place them down, save or load a new map
- A work in progress "savegame" window built, but this has no functionality yet and is merely a design, proof of concept.
- A simple audio handler that can be used to play music in the main menu, cutscene dialogues or provide area specific music.

**Note**: as of right now, this is a work in progress framework. The base foundation is there and has been written in a way to easily add functionality. 
