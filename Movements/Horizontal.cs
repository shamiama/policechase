using policechase.Core;
using policechase.Entiities;
using policechase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policechase.Movements
{
    internal class Horizontal
    {
        public class HorizontalMovement : IMovement
        {
            private float leftBound;
            private float rightBound;
            private float speed;

            public HorizontalMovement(float left, float right, float speed = 2f)
            {
                leftBound = left;
                rightBound = right;
                this.speed = speed;
            }

            public void Move(GameObject obj, GameTime gameTime)
            {
                // Move left or right
                obj.Position = new PointF(obj.Position.X + speed, obj.Position.Y);

                // Check bounds and reverse direction
                if (obj.Position.X <= leftBound)
                {
                    obj.Position = new PointF(leftBound, obj.Position.Y);
                    speed = Math.Abs(speed); // move right
                }
                else if (obj.Position.X >= rightBound)
                {
                    obj.Position = new PointF(rightBound, obj.Position.Y);
                    speed = -Math.Abs(speed); // move left
                }
            }
        }
    }
}

