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
        public int MaxHealth { get; set; } = 100;
        public int Energy { get; set; } = 50;
        public int MaxEnergy { get; set; } = 100;
        public int CoinCount { get; set; } = 0;
        public event Action CoinCollected;
        public bool IsJumping { get; private set; } = false;
        public bool IsNitroActive { get; set; } = false;
        private int jumpTimer = 0;
        private int iFrameTimer = 0; // Invulnerability frames after being hit
        private SizeF originalSize;

        public override void Update(GameTime gameTime)
        {
            if (originalSize.IsEmpty) originalSize = Size;

            // Handle Energy consumption for Nitro
            if (IsNitroActive && Energy > 0)
            {
                // Consume energy every frame while nitro is pushed
                Energy--; 
                if (Energy <= 0)
                {
                    Energy = 0;
                    IsNitroActive = false; // Force off
                }
            }

            Movement?.Move(this, gameTime);
            base.Update(gameTime);

            if (iFrameTimer > 0) iFrameTimer--;

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
                if (!IsJumping && !IsNitroActive) 
                {
                    Health = 0; // Immediate death on car crash
                    iFrameTimer = 0; 
                }
            }

            if (other is Coin && other.IsActive)
            {
                CoinCount++;
                other.IsActive = false; // Collect coin
                CoinCollected?.Invoke();
            }

            // Power-up logic will be handled by the PowerUp/Booster objects themselves
            // but we keep this as a fallback if needed.
            // if (other is PowerUp) Health = Math.Min(MaxHealth, Health + 20);
        }
    }

}