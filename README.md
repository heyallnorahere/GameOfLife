# Game of Life [![ci](https://img.shields.io/github/workflow/status/yodasoda1219/GameOfLife/ci?label=ci)](https://github.com/yodasoda1219/GameOfLife/actions/workflows/ci.yml)

This is my implementation of [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).

## Building

On any Unix-like platform, run `dotnet build` in the source directory with .NET Core to build.
On Windows, open `GameOfLife.sln` in Visual Studio and hit `Ctrl+Shift+B` to build.

## Controls

- `Q`: quit
- `W`: move screen up
- `S`: move screen down
- `A`: move screen left
- `D`: move screen right
- `-`: zoom out/enlarge console
- `=`/`+`: zoom in/shrink console (I couldn't make the font smaller)
- Up arrow: increase framerate
- Down arrow: decrease framerate
- Left arrow: go back a frame
- Right arrow: go forward a frame
- Space bar: pause/resume