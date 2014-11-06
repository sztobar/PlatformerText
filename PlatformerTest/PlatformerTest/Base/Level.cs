using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest.Base
{
    public class Level : Component
    {
        #region variables

        private int width;
        private int height;

        protected Texture2D backgroundTexture;
        protected Texture2D gridTexture;
        /// <summary>
        /// Background texture is white dot so actual color
        /// is determined by backgroundColor
        /// </summary>
        protected Color backgroundColor = Color.SandyBrown;
        /// <summary>
        /// Determines if level should draw grid
        /// </summary>
        protected bool showGrid = false;

        #endregion

        #region properties

        public int Width
        {
            get { return width / ProgramConfig.tileSize; }
            set { width = value * ProgramConfig.tileSize; }
        }

        public int Height
        {
            get { return height / ProgramConfig.tileSize; }
            set { height = value * ProgramConfig.tileSize; }
        }

        #endregion

        #region override methods

        public override void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("base");
            gridTexture = content.Load<Texture2D>("grid");
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundTexture, new Rectangle(0, 0, width, height), backgroundColor);
            if (showGrid)
                for (int x = 0; x < Width; ++x)
                    for (int y = 0; y < Height; ++y)
                        spritebatch.Draw(gridTexture,
                            new Rectangle(x * ProgramConfig.tileSize,
                                y * ProgramConfig.tileSize,
                                ProgramConfig.tileSize,
                                ProgramConfig.tileSize),
                            Color.White);
        }

        #endregion
    }
}
