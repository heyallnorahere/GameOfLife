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

## Config file

To specify a starting position among other things, create a `config.json` file in the executable's working directory. This will let you load a state on startup. For example, to create a beehive:
```json
{
    "InitialState": [
        [ 0, 1 ],
        [ 3, 1 ],
        [ 1, 0 ],
        [ 2, 0 ],
        [ 1, 2 ],
        [ 2, 2 ]
    ]
}
```
It's also possible to create custom rules. To create a custom rule, create a .NET assembly linked against `GameOfLife.dll` with a class like so:
```cs
using GameOfLife;

namespace MyRuleset
{
    [CustomRule("MyCustomRule")]
    public class MyCustomRuleContainerClass
    {
        public static MyCustomRule(BoardController controller, Vector cell)
        {
            // rule code goes here
        }
    }
}
```
Then, to apply your rule to the game, insert an entry into `RulesetAssemblies` like specified below:
```json
{
    "InitialState": [
        ...
    ],
    "RulesetAssemblies": [
        "MyRuleset.dll"
    ]
}
```