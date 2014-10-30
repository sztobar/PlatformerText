using System;

namespace PlatformerTest
{
    public enum Tile : byte
    {
        None,
        Obstacle,
        Platform,
        Ladder,
        Slope_0_64,
        Slope_64_0,
        Slope_0_16,
        Slope_16_32,
        Slope_32_48,
        Slope_48_64,
        Slope_16_0,
        Slope_32_16,
        Slope_48_32,
        Slope_64_48
    };
}
