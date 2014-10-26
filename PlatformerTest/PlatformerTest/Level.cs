using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest
{
    public class Level
    {
        int[,] _sourceTiles = new int[14, 16] { 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3 },
            { 0, 0, 2, 3, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 3, 0, 0, 0, 2, 0, 0, 0, 0, 0, 6, 0, 3 },
            { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 3 },
            { 1, 1, 1, 1, 1, 1, 0, 0, 0, 6, 1, 1, 1, 1, 1, 1 }
        };
        int[,] _tiles;
        public int[,] Tiles
        {
            get
            {
                return _tiles;
            }
        }
        public static int TILE_SIZE = 32;
        Texture2D _tileset;
        int _tilesetHeight;
        int _tilesetWidth;

        public Level()
        {
            _tiles = new int[_sourceTiles.GetLength(1), _sourceTiles.GetLength(0)];
            for (int i = 0, len_i = _sourceTiles.GetLength(0); i < len_i; ++i)
            {
                for (int j = 0, len_j = _sourceTiles.GetLength(1); j < len_j; ++j)
                {
                    // i = y
                    // j = x
                    _tiles[j, i] = _sourceTiles[i, j];
                }
            }
        }

        public void Update(GameTime gameTime, KeyboardState keyState)
        {
            //_player.IsStandingOn(_obstacles);
        }

        public void SetTileset(Texture2D texture)
        {
            _tileset = texture;
            _tilesetWidth = _tileset.Width / TILE_SIZE;
            _tilesetHeight = _tileset.Height / TILE_SIZE;
        }

        public Rectangle GetTileSprite(int tileNo)
        {
            int tileX = tileNo % _tilesetWidth;
            int tileY = tileNo / _tilesetWidth;
            return new Rectangle(tileX * TILE_SIZE, tileY * TILE_SIZE, TILE_SIZE, TILE_SIZE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int tileNo, tileX, tileY;
            for (int x = 0, len_x = _tiles.GetLength(0); x < len_x; ++x)
            {
                for (int y = 0, len_y = _tiles.GetLength(1); y < len_y; ++y)
                {
                    // i = y
                    // j = x
                    tileNo = _tiles[x, y];
                    tileX = tileNo % _tilesetWidth;
                    tileY = tileNo / _tilesetWidth;
                    spriteBatch.Draw(
                        _tileset,
                        new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        new Rectangle(tileX * TILE_SIZE, tileY * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        Color.White);
                }
            }
        }

        public void DrawGrid(SpriteBatch spriteBatch)
        {
            int tileX = 1,
                tileY = 1;

            Rectangle tileTextureRectangle = new Rectangle(tileX * TILE_SIZE, tileY * TILE_SIZE, TILE_SIZE, TILE_SIZE);

            for (int x = 0, len_x = _tiles.GetLength(0); x < len_x; ++x)
            {
                for (int y = 0, len_y = _tiles.GetLength(1); y < len_y; ++y)
                {
                    spriteBatch.Draw(
                        _tileset,
                        new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        tileTextureRectangle,
                        Color.White);
                }
            }
        }

        public void DrawIntersectingGrid(SpriteBatch spriteBatch, Player player)
        {
            int tileX = 2,
                tileY = 1;
            
            Rectangle tileTextureRectangle = new Rectangle(tileX * TILE_SIZE, tileY * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            Rectangle playerBoundingBox = player.BoundingBox;

            int gridFromX = playerBoundingBox.Left / TILE_SIZE,
                gridFromY = playerBoundingBox.Top / TILE_SIZE,
                gridToX = (playerBoundingBox.Right - 1) / TILE_SIZE,
                gridToY = (playerBoundingBox.Bottom - 1) / TILE_SIZE;

            for (int x = gridFromX, len_x = gridToX; x <= len_x; ++x)
            {
                for (int y = gridFromY, len_y = gridToY; y <= len_y; ++y)
                {
                    spriteBatch.Draw(
                        _tileset,
                        new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        tileTextureRectangle,
                        Color.White);
                }
            }
        }
    }
}
