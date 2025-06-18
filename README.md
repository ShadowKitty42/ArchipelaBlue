# ArchipelaBlue

## What is this?

This is a development project intended to make Blue Prince work on [Archipelago Multiworld Randomizer](https://archipelago.gg)
Nothing is currently working as we are still figuring out how the game works internally.

## For Developers

If you want to contribute to development, you're welcome to do so. We are collaborating in the [Blue Prince](https://discord.com/channels/731205301247803413/1362478224604397739) thread in the [Archipelago Discord](https://discord.gg/8Z65BR2)

Setting up the development environment is fairly simple:
1. Download and install the latest version of [MelonLoader](https://github.com/LavaGang/MelonLoader), obviously selecting Blue Prince as the game.
2. Clone the repo
3. Rename the `Directory.Build.props.default` file to `Directory.Build.props.user`
4. Open `Directory.Build.props.user` using a text editor
5. Edit the `GameFolder` property to match your game installation

Your dev tools should automatically find the libraries and will copy the `.dll` to the game's mod folder after building the project. Pressing Start in VS also opens the game (First start always closes though, no idea why. It runs a second time though and that works.)