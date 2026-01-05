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
    public class ChasingMovement : IMovement
    {
        private GameObject target;
        private float speed;

        public ChasingMovement(GameObject target, float speed = 3f)
        {
            this.target = target;
            this.speed = speed;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            float dx = target.Position.X - obj.Position.X;
            float dy = target.Position.Y - obj.Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                obj.Position = new PointF(
                    obj.Position.X + (speed * dx / distance),
                    obj.Position.Y + (speed * dy / distance)
                );
            }
        }
    }
}

