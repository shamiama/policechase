using System.Drawing;
using policechase.Properties;

namespace policechase.Entiities
{
    public class Coin : GameObject
    {
        public Coin()
        {
            Sprite = Resources.coin; // Use the resource directly
            Size = new SizeF(30, 30); // Standard coin size
            Velocity = new PointF(0, 5); // Moves down at a standard speed
        }

        public override void Draw(Graphics g)
        {
            if (Sprite != null)
            {
                g.DrawImage(Sprite, Bounds);
            }
            else
            {
                g.FillEllipse(Brushes.Yellow, Bounds); // Fallback
            }
        }
    }
}
