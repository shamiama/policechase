using policechase.Core;
using policechase.Extensions;
using policechase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policechase.Entiities
{
    public class Enemy : GameObject
    {
        public static Image? DefaultSprite { get; set; }

        // Optional movement behavior: demonstrates composition and allows testable movement logic.
        public IMovement? Movement { get; set; }

        // Default enemy velocity is set in constructor to give basic movement out-of-the-box.
        public Enemy()
        {
            Velocity = new PointF(-2, 0); // Spawner usually overwrites this
            Sprite = DefaultSprite;
        }

        /// Update will call movement behavior (if any) and then apply base update to more by velocity.
        public override void Update(GameTime gameTime)
        {
            Movement?.Move(this, gameTime); // movement must be called
            base.Update(gameTime);
        }

        /// Custom draw: demonstrates polymorphism (override base draw to provide enemy visuals).
        public override void Draw(Graphics g)
        {
            if (Sprite != null)
            {
                g.DrawImage(Sprite, Bounds);
            }
            else
            {
                g.FillRectangle(Brushes.Red, Bounds);
            }
        }

        /// On collision, enemy deactivates when hit by bullets (encapsulation of reaction logic inside the entity).
        public override void OnCollision(GameObject other)
        {
            if (other is Bullet) // Bullet usage example
                IsActive = false;
        }
    }
}