using policechase.Core;
using policechase.Entiities;
using policechase.Interfaces;
using System;
using System.Drawing;

namespace policechase.Movements
{
    public class Zigzag : IMovement
    {
        private float horizontalSpeed;
        private float verticalSpeed;
        private float amplitude;
        private float startX;
        private float timer = 0;

        public Zigzag(float horizontalSpeed = 5f, float verticalSpeed = 5f, float amplitude = 100f)
        {
            this.horizontalSpeed = horizontalSpeed;
            this.verticalSpeed = verticalSpeed;
            this.amplitude = amplitude;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            if (startX == 0) startX = obj.Position.X;

            timer += 0.1f;
            float newX = startX + (float)Math.Sin(timer) * amplitude;
            
            obj.Position = new PointF(newX, obj.Position.Y + verticalSpeed);
        }
    }
}
