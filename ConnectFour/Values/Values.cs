using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This file handles any enumerations and constants used for the game.
/// </summary>
namespace ConnectFour
{
    /// <summary>
    /// ComputerDifficulty defines the level of difficulty of the Computer player.
    /// </summary>
    public enum ComputerDifficulty : byte
    {
        Random = 0,
        Easy,
        Normal,
        Advanced
    }

    /// <summary>
    /// Pertains to which part of the board the display needs to display.
    /// </summary>
    public enum BoardBorder : byte
    {
        Top = 0,
        Middle,
        Bottom
    }

    public enum Direction: byte
    {
        Up = 0,
        Left,
        Right,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
}
