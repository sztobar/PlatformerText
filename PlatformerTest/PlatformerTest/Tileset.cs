using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformerTest
{
    class Tileset
    {
        string _textureSource;
        public string TextureSource
        {
            get { return _textureSource; }
        }
        /// <summary>
        /// Array that defines our tileset configuration.
        /// Index of this array are tileNo of level
        /// </summary>
        Tile[] _tileMapping;

        public Tileset(string textureSource, Tile[] tileMapping)
        {
            _textureSource = textureSource;
            _tileMapping = tileMapping;
        }
    }
}
