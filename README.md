# :jigsaw: :ice_cube: Cubism VR Level Editor :ice_cube: :jigsaw:
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/DevPika/cubismvr-level-editor)](https://github.com/DevPika/cubismvr-level-editor/releases/latest)

A simple puzzle viewer and editor for the minimalist VR puzzle game [Cubism](https://www.cubism-vr.com/). This makes use of the [custom puzzle format](https://github.com/cubismvr/Mods/blob/main/CustomPuzzles/Example.cube) supported by the game (JSON in .cube files) to save and load puzzles. Created using Unity 2019.4.11f1.

![Screenshot](/Images/Screenshot1.png)

<p align="center">:orange_square: :green_square: :blue_square: :red_square: :purple_square: :brown_square:</p>

## Features :sparkles:
### Loading existing puzzle files 
Just plop the .cube file in the `CubismVRLevelEditor_Data/StreamingAssets` folder, type in the name of the file (without the extension) in the editor and hit load! The Example puzzle has been already placed in the folder, all ready to be loaded! Puzzles show up with the same orientation and color as in the actual game. 

### Creating new puzzles
Create new puzzles anytime by clicking "Clear and start new puzzle".

### Viewing puzzles
The editor features a variety of controls to make viewing the puzzle in 3D a breeze. People familiar with the Unity Editor controls should feel right at home with the controls:
* Right click and drag to rotate
* Hold right click and use WASD keys to fly around (W and D take you closer or farther, A and D keys take you left or right)
* Zoom in / out using the mouse scroll wheel
* Middle / scroll wheel hold and drag to pan around the view

### Editing puzzles
* Adding segments/cells to the current piece is as easy as clicking on adjacent locations. A translucent cursor shows where the segment would be added after clicking.
* Change the color of the current piece by changing the RGB values using the sliders or typing in the hex code. A preview of the current color is visible near the color picker.
* Start a new piece using the "Start a new piece" button near the color picker and customize the color to your liking. The editor already suggests a random bright color for you!
* Meta data associated with the puzzle can be edited on the right
    * Difficulty
    * Puzzle angle (the initial angle it shows up as inside the Cubism game)
    * Author name
    * Puzzle name
    
The editor automatically generates other metadata like a unique ID, creation data etc. while saving the puzzle. Which leads us to...

### Saving puzzles
* After you are done adding and editing pieces, use the "Save as new puzzle" button to save to a new file
* Save over (overwrite) an existing puzzle (for example, previously saved) using the "Save modified puzzle to last file". The button shows the name of the file that would be overwritten inside brackets.
* Saved files are accessible in the same folder as before, i.e. `CubismVRLevelEditor_Data/StreamingAssets`

You can find instructions to sideload the puzzles and some additional puzzles not included in the main game [here](https://github.com/cubismvr/Mods/tree/main/CustomPuzzles#puzzle-examples-and-downloads).

### Realtime log/console
The editor shows a running log of load and save operations. You can continue working on your puzzle, knowing that it is has been saved!

<p align="center">:orange_square: :green_square: :blue_square: :red_square: :purple_square: :brown_square:</p>

## Download :arrow_down:
Get the latest build in the [releases section](https://github.com/DevPika/cubismvr-level-editor/releases/latest).

:orange_square: :green_square: :blue_square: :red_square: :purple_square: :brown_square:

## To Do :clipboard:
* Improved workflow to allow creation of an overall shape before assigning pieces
* Add / remove / recolor previous pieces in a puzzle
* Undo operations

<p align="center">:orange_square: :green_square: :blue_square: :red_square: :purple_square: :brown_square:</p>

## Support :hearts:
This editor is a way of showing my love for Cubism. If you like what you see here, consider supporting the game by buying it and getting DLCs ( coming soon according to the developer!). Let's create an active modding community for the game!

<p align="center">:orange_square: :green_square: :blue_square: :red_square: :purple_square: :brown_square:</p>
