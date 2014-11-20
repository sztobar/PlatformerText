using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest.Base
{
    public static class MapExtension
    {
        public static void DrawScrollingLayer(this Map map, SpriteBatch spriteBatch, int layerId, Camera camera, Vector2 scrolling, float layerDepth = 1f)
        {
            var txMin = (int)(camera.X * scrolling.X) / map.TileWidth;
            var txMax = ((int)(camera.X * scrolling.X) + ProgramConfig.windowWidth) / map.TileWidth;
            var tyMin = (int)(camera.Y * scrolling.Y) / map.TileHeight;
            var tyMax = ((int)(camera.Y * scrolling.Y) + ProgramConfig.windowHeight) / map.TileHeight;

            map.DrawScrollingLayer(spriteBatch, layerId, camera, scrolling, txMin, txMax, tyMin, tyMax, layerDepth);
        }

        private static void DrawScrollingLayer(this Map map, SpriteBatch spriteBatch, int layerId, Camera camera, Vector2 scrolling, int txMin, int txMax, int tyMin, int tyMax, float layerDepth = 1f)
        {
            var ds = ((Vector2.One - scrolling) * camera.Position).ToPoint();
            for (var y = tyMin; y <= tyMax; y++)
            {
                for (var x = txMin; x <= txMax; x++)
                {
                    if (x >= map.TileLayers[layerId].Tiles.Length || y >= map.TileLayers[layerId].Tiles[x].Length ||
                        map.TileLayers[layerId].Tiles[x][y] == null) continue;
                    var tileTarget = map.TileLayers[layerId].Tiles[x][y].Target;
                    tileTarget.Offset(ds);

                    spriteBatch.Draw(
                        map.Tilesets[map.SourceTiles[map.TileLayers[layerId].Tiles[x][y].SourceID].TilesetID].Texture,
                        tileTarget,
                        map.SourceTiles[map.TileLayers[layerId].Tiles[x][y].SourceID].Source,
                        map.TileLayers[layerId].OpacityColor,
                        map.TileLayers[layerId].Tiles[x][y].Rotation,
                        map.SourceTiles[map.TileLayers[layerId].Tiles[x][y].SourceID].Origin,
                        map.TileLayers[layerId].Tiles[x][y].Effects,
                        layerDepth);
                }
            }
        }
    }
}
