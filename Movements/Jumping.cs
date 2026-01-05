using policechase.Core;
using policechase.Entiities;
using policechase.Interfaces;
using System;
using System.Drawing;

namespace policechase.Movements
{
    public class Jumping : IMovement
    {
        private float verticalSpeed;
        private float jumpPower;
        private float gravity = 0.5f;
        private float currentJumpVelocity = 0;
        private bool isJumping = false;
        private Random random = new Random();

        public Jumping(float verticalSpeed = 5f, float jumpPower = 15f)
        {
            this.verticalSpeed = verticalSpeed;
            this.jumpPower = jumpPower;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            // Regular downward movement
            float nextY = obj.Position.Y + verticalSpeed;

            // Occasional jump
            if (!isJumping && random.Next(100) < 2)
            {
                isJumping = true;
                currentJumpVelocity = -jumpPower;
                if (obj is Enemy enemy) engineJumpEffect(enemy);
            }

            if (isJumping)
            {
                nextY += currentJumpVelocity;
                currentJumpVelocity += gravity;

                if (currentJumpVelocity > jumpPower)
                {
                    isJumping = false;
                    currentJumpVelocity = 0;
                }
            }

            obj.Position = new PointF(obj.Position.X, nextY);
        }

        private void engineJumpEffect(Enemy enemy)
        {
            // Visual feedback could be added here if needed
        }
    }
}
