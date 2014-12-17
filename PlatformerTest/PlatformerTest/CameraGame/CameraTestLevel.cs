using System;
using System.Linq;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestLevel : Base.Level
    {
        Map map;

        public CameraTestLevel(Base.Camera camera)
            : base(camera)
        { }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            map = content.Load<Map>("tmx/xaton12test2");
            ProgramConfig.LevelTiles = GetLevelTiles();
            ProgramConfig.CurrentLevel = this;
        }

        public override void Initialize()
        {
            Width = 120;
            Height = 120;
            //showGrid = true;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            map.DrawScrollingLayer(spritebatch, 0, camera, Vector2.Zero);
            map.DrawScrollingLayer(spritebatch, 1, camera, new Vector2(0.5f, 1f));
            map.DrawScrollingLayer(spritebatch, 2, camera, Vector2.One);
            //map.DrawScrollingLayer(spritebatch, 3, camera, Vector2.One);
            map.DrawScrollingLayer(spritebatch, 4, camera, Vector2.One);

            base.Draw(spritebatch);
        }

        public int[,] GetLevelTiles()
        {
            var collisionTiles = map.TileLayers["collision"].Tiles;
            var lengthI = collisionTiles.Length;
            var result = new int[lengthI, collisionTiles[0].Length];
            for (var i = 0; i < lengthI; ++i)
            {
                var tilesRow = collisionTiles[i];
                var lengthJ = tilesRow.Length;
                for (var j = 0; j < lengthJ; ++j)
                {
                    result[i, j] = tilesRow[j] == null ? 0 : map.SourceTiles[tilesRow[j].SourceID].TilesetID;
                }
            }
            return result;
        }

        public override int[][] GetCollisionTiles()
        {
            var collisionTiles = map.TileLayers["collision"].Tiles;
            var lengthI = collisionTiles.Length;
            var result = new int[lengthI][];
            for (var i = 0; i < lengthI; ++i)
            {
                var tilesRow = collisionTiles[i];
                var lengthJ = tilesRow.Length;
                result[i] = new int[lengthJ];
                for (var j = 0; j < lengthJ; ++j)
                {
                    result[i][j] = tilesRow[j] == null ? 0 : map.SourceTiles[tilesRow[j].SourceID].TilesetID;
                }
            }
            return result;
        }
    }
}
