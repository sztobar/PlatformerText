using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest.Base
{
    public abstract class Level : Component
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

        protected bool drawBackground = false;

        /// <summary>
        /// List for in-level object for checking collisions between them
        /// </summary>
        protected List<LevelSprite> components = new List<LevelSprite>();

        /// <summary>
        /// For level sprites to invoke their LoadContent methods
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// Because level must know which regions should be drawed
        /// </summary>
        protected readonly Base.Camera camera;

        #endregion

        #region constructor

        public Level(Base.Camera camera)
        {
            this.camera = camera;
        }

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

        public Rectangle View { get { return new Rectangle((int)camera.Position.X, (int)camera.Position.Y, ProgramConfig.windowWidth, ProgramConfig.windowHeight); } }

        #endregion

        #region override methods

        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            backgroundTexture = content.Load<Texture2D>("base");
            gridTexture = content.Load<Texture2D>("grid");
        }

        public override void Update(float dt, UserInputState inputState)
        {
            base.Update(dt, inputState);
            // for iterates backwards because components can destroy themselves during this method
            for (var i = components.Count - 1; i >= 0; --i)
            {
                components[i].Update(dt, inputState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (drawBackground) spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, width, height), backgroundColor);

            if (showGrid)
                for (var x = 0; x < Width; ++x)
                    for (var y = 0; y < Height; ++y)
                        spriteBatch.Draw(gridTexture,
                            new Rectangle(x * ProgramConfig.tileSize,
                                y * ProgramConfig.tileSize,
                                ProgramConfig.tileSize,
                                ProgramConfig.tileSize),
                            Color.White);

            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public abstract int[][] GetCollisionTiles();

        #endregion

        #region components

        /// <summary>
        /// Method for adding new components to level
        /// </summary>
        /// <param name="component">component to add</param>
        public void AddComponent(LevelSprite component)
        {
            components.Add(component);
            component.Initialize();
            component.LoadContent(content);
        }

        public void ForEachComponent(Func<LevelSprite, bool> callback)
        {
            // for iterates backwards because components can destroy themselves during this method
            for (var i = components.Count - 1; i >= 0; --i)
            {
                if (callback(components[i]) == false) break;
            }
        }

        public void DestroyComponent(LevelSprite component)
        {
            components.Remove(component);
        }

        /// <summary>
        /// Checks if sprite collides with tiles considered obstacle
        /// </summary>
        /// <param name="component"></param>
        public bool IsCollidingObstacle(LevelSprite component)
        {
            var fromX = (int)component.Position.X/ProgramConfig.tileSize;
            var toX = (int)((component.Position.X + component.Width) / ProgramConfig.tileSize);
            var fromY = (int)component.Position.Y/ProgramConfig.tileSize;
            var toY = (int)((component.Position.Y + component.Height) / ProgramConfig.tileSize);
            var collisionTiles = GetCollisionTiles();
            var collision = false;
            const int collisionTile = 4;
            var tileSize = ProgramConfig.tileSize;
            var sprite = component.Rectangle;

            for (var x = fromX; x <= toX; ++x)
            {
                for (var y = fromY; y <= toY; ++y)
                {
                    if (x >= collisionTiles.Length || y >= collisionTiles[x].Length || collisionTiles[x][y] != collisionTile) continue;
                    var collisitonRectangle = new Rectangle(x*tileSize, y*tileSize, tileSize, tileSize);
                    if (!sprite.Intersects(collisitonRectangle)) continue;
                    collision = true;
                    break;
                }
                if (collision) break;
            }
            return collision;
        }

        /// <summary>
        /// Checks if component is in game viewport(camera position with window size)
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool IsInView(LevelSprite component)
        {
            var sprite = component.Rectangle;
            var view = View;

            return sprite.Top >= view.Top && sprite.Left >= view.Left && sprite.Right <= view.Right &&
                   sprite.Bottom <= view.Bottom;
        }

        #endregion
    }
}
