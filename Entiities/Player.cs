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
    public class Player : GameObject
    {
        // Movement strategy: demonstrates composition over inheritance.
        // Different movement behaviors can be injected (KeyboardMovement, PatrolMovement, etc.).
        public IMovement? Movement { get; set; }

        // Domain state
        // Jump State
        public int Health { get; set; } = 100;
        public int CoinCount { get; set; } = 0;
        public event Action CoinCollected;
        public bool IsJumping { get; private set; } = false;
        private int jumpTimer = 0;
        private SizeF originalSize;

        public override void Update(GameTime gameTime)
        {
            if (originalSize.IsEmpty) originalSize = Size;

            Movement?.Move(this, gameTime);
            base.Update(gameTime);

            if (IsJumping)
            {
                jumpTimer--;
                if (jumpTimer <= 0)
                {
                    IsJumping = false;
                    IsIntangible = false;
                    Size = originalSize; // Reset size
                }
            }
        }

        public void Jump()
        {
            if (CoinCount >= 5 && !IsJumping)
            {
                CoinCount -= 5;
                IsJumping = true;
                IsIntangible = true; // Use IsIntangible to prevent physics pushing
                jumpTimer = 100; // Increased jump duration
                Size = new SizeF(originalSize.Width * 1.5f, originalSize.Height * 1.5f); // Increased zoom effect
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                if (!IsJumping) // Invulnerable while jumping
                    Health = 0; 
            }

            if (other is Coin && other.IsActive)
            {
                CoinCount++;
                other.IsActive = false; // Collect coin
                CoinCollected?.Invoke();
            }

            if (other is PowerUp)
                Health += 20;
        }
    }

}