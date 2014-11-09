using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestLevel : Base.Level
    {
        Map map;
        Base.Camera camera;

        public CameraTestLevel(Base.Camera camera)
        {
            this.camera = camera;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            map = content.Load<Map>("tmx/xaton12test2");
        }

        public override void Initialize()
        {
            Width = 120;
            Height = 120;
            //showGrid = true;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            var cameraPos = camera.Position;
            var rec = new Rectangle((int)cameraPos.X, (int)cameraPos.Y, ProgramConfig.windowWidth, ProgramConfig.windowHeight);

            DrawLayer(spritebatch, map, 1, rec, 0f);
            DrawLayer(spritebatch, map, 2, rec, 0f);
            DrawLayer(spritebatch, map, 3, rec, 0f);
            DrawLayer(spritebatch, map, 4, rec, 0f);
        }

        private void DrawLayer(SpriteBatch spriteBatch, Map map, int layerId, Rectangle region, float layerDepth)
        {
            DrawLayer(spriteBatch, map, layerId, ref region, layerDepth);
        }

        private void DrawLayer(SpriteBatch spriteBatch, Map map, int layerId, ref Rectangle region, float layerDepth)
        {
            var txMin = region.X / map.TileWidth;
            var txMax = (region.X + region.Width) / map.TileWidth;
            var tyMin = region.Y / map.TileHeight;
            var tyMax = (region.Y + region.Height) / map.TileHeight;

            if (map.Orientation == MapOrientation.Isometric)
            {
                tyMax = tyMax * 2 + 1;
                txMax = txMax * 2 + 1;
            }

            DrawLayer(spriteBatch, map, layerId, ref region, txMin, txMax, tyMin, tyMax, layerDepth);
        }

        private void DrawLayer(SpriteBatch spriteBatch, Map map, int layerId, ref Rectangle region, int txMin, int txMax, int tyMin, int tyMax, float layerDepth)
        {
            var cameraPosition = camera.Position;
            for (var y = tyMin; y <= tyMax; y++)
            {
                for (var x = txMin; x <= txMax; x++)
                {
                    if (x < map.TileLayers[layerId].Tiles.Length && y < map.TileLayers[layerId].Tiles[x].Length && map.TileLayers[layerId].Tiles[x][y] != null)
                    {
                        var tileTarget = map.TileLayers[layerId].Tiles[x][y].Target;
                        //tileTarget.X = tileTarget.X - region.X;
                        //tileTarget.Y = tileTarget.Y - region.Y;
                        //tileTarget.Offset(new Point(-(int)cameraPosition.X, -(int)cameraPosition.Y));

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
}
