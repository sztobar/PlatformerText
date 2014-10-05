using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformerTest
{
    class QuadTree
    {
        const int MAX_OBJECTS = 10;
        const int MAX_LEVELS = 5;

        int _level;
        List<Rectangle> _objects;
        Rectangle _bounds;
        QuadTree[] _nodes;

        public QuadTree(int level, Rectangle bounds)
        {
            _level = level;
            _objects = new List<Rectangle>();
            _bounds = bounds;
            _nodes = new QuadTree[4];
        }

        public void Clear()
        {
            _objects.Clear();

            for (int i = 0, len = _nodes.Length; i < len; ++i)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }

        void Split()
        {
            int subWidth = _bounds.Width / 2;
            int subHeight = _bounds.Height / 2;
            int x = _bounds.X;
            int y = _bounds.Y;

            _nodes[0] = new QuadTree(_level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new QuadTree(_level + 1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new QuadTree(_level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new QuadTree(_level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        int GetIndex(Rectangle rectangle)
        {
            int index = -1;
            int verticalMidpoint = _bounds.X + (_bounds.Width / 2);
            int horizontalMidpoint = _bounds.Y + (_bounds.Height / 2);

            bool topQuadrant = rectangle.Y < horizontalMidpoint && rectangle.Y + rectangle.Height < horizontalMidpoint;
            bool bottomQuadrant = rectangle.Y >= horizontalMidpoint;

            if (rectangle.X < verticalMidpoint && rectangle.X + rectangle.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            else if (rectangle.X >= verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 3;
                }
                else if (bottomQuadrant)
                {
                    index = 4;
                }
            }

            return index;
        }

        public void Insert(Rectangle rectangle)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(rectangle);

                if (index != -1)
                {
                    _nodes[index].Insert(rectangle);

                    return;
                }
            }

            _objects.Add(rectangle);

            if (_objects.Count > MAX_OBJECTS && _level < MAX_LEVELS)
            {
                if (_nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < _objects.Count())
                {
                    int index = GetIndex(_objects[i]);
                    if (index != -1)
                    {
                        _nodes[index].Insert(_objects[i]);
                        _objects.RemoveAt(i);
                    }
                    else
                    {
                        ++i;
                    }
                }
            }
        }

        public List<Rectangle> Retrieve(List<Rectangle> returnObjects, Rectangle rectangle)
        {
            int index = GetIndex(rectangle);
            if (index != -1 && _nodes[0] != null)
            {
                _nodes[index].Retrieve(returnObjects, rectangle);
            }

            returnObjects.AddRange(_objects);

            return returnObjects;
        }
    }
}
