﻿using System.Numerics;

namespace Data
{
    public class Ball
    {
        private Vector2 Position { get; set; }

        public Ball(Vector2 position)
        {
            this.Position = position;
        }
    }
}
