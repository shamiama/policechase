using policechase.Core;
using policechase.Entiities;
using policechase.Interfaces;
using System.Drawing;

namespace policechase.Movements
{
    public class Vertical : IMovement
    {
        private float speed;
        private float topBound;
        private float bottomBound;
        private bool loop;

        public Vertical(float speed = 5f, float top = -500f, float bottom = 5000f, bool loop = true)
        {
            this.speed = speed;
            this.topBound = top;
            this.bottomBound = bottom;
            this.loop = loop;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            obj.Position = new PointF(obj.Position.X, obj.Position.Y + speed);

            if (obj.Position.Y > bottomBound)
            {
                if (loop)
                    obj.Position = new PointF(obj.Position.X, topBound);
                else
                    speed = -Math.Abs(speed);
            }
            else if (obj.Position.Y < topBound)
            {
                if (!loop)
                    speed = Math.Abs(speed);
            }
        }
    }
}
