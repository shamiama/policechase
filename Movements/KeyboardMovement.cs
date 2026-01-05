
using EZInput;
using policechase.Core;
using policechase.Entiities;
using policechase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Drawing;

namespace policechase.Movements
{
    public class KeyboardMovement : IMovement
    {
        public float Speed { get; set; } = 5f;
        public RectangleF? Bounds { get; set; }

        public void Move(GameObject obj, GameTime gameTime)
        {
            PointF newPos = obj.Position;
            float currentSpeed = Speed;

            if (obj is Player player)
            {
                // Check if Nitro is being used (Shift key)
                if (Keyboard.IsKeyPressed(Key.Shift) && player.Energy > 0)
                {
                    player.IsNitroActive = true;
                    currentSpeed *= 2.0f; // Double Speed!
                }
                else
                {
                    player.IsNitroActive = false;
                }
            }

            if (Keyboard.IsKeyPressed(Key.LeftArrow))
                newPos = new PointF(Math.Max(newPos.X - currentSpeed, Bounds?.Left ?? float.MinValue), newPos.Y);

            if (Keyboard.IsKeyPressed(Key.RightArrow))
                newPos = new PointF(Math.Min(newPos.X + currentSpeed, (Bounds?.Right ?? float.MaxValue) - obj.Size.Width), newPos.Y);

            if (Keyboard.IsKeyPressed(Key.UpArrow))
                newPos = new PointF(newPos.X, Math.Max(newPos.Y - currentSpeed, Bounds?.Top ?? float.MinValue));

            if (Keyboard.IsKeyPressed(Key.DownArrow))
                newPos = new PointF(newPos.X, Math.Min(newPos.Y + currentSpeed, (Bounds?.Bottom ?? float.MaxValue) - obj.Size.Height));

            obj.Position = newPos;
        }
    }

}