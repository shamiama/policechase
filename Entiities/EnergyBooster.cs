using policechase.Core;
using policechase.Entiities;
using System;
using System.Drawing;

namespace policechase.Entiities
{
    public class EnergyBooster : GameObject
    {
        private Game _game;

        public EnergyBooster(Game game)
        {
            _game = game;
            Size = new SizeF(60, 60);
            Velocity = new PointF(0, 5); // Fall down slowly
        }

        public override void Draw(Graphics g)
        {
            // ... (rest of draw logic)
            g.FillEllipse(Brushes.Yellow, Bounds);
            g.DrawEllipse(Pens.Blue, Bounds);
            
            PointF[] bolt = new PointF[] {
                new PointF(Position.X + 30, Position.Y + 10),
                new PointF(Position.X + 15, Position.Y + 35),
                new PointF(Position.X + 30, Position.Y + 35),
                new PointF(Position.X + 20, Position.Y + 55),
                new PointF(Position.X + 45, Position.Y + 25),
                new PointF(Position.X + 30, Position.Y + 25)
            };
            g.FillPolygon(Brushes.Blue, bolt);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player player)
            {
                player.Energy = Math.Min(player.MaxEnergy, player.Energy + 30);
                _game.IncreaseScore(5); // Reduced Score Bonus
                IsActive = false;
            }
        }
    }
}
