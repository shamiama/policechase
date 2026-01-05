using policechase.Entiities;
using policechase.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policechase.Core
{
    public class EnemySpawner
    {
        private Random random = new Random();
        private int spawnTimer = 0;
        private int spawnInterval = 8; // Spawn EXTREMELY frequently (initially)
        private Game game;

        public int EnemySpeed { get; set; } = 40; // Extreme Base Speed!

        public EnemySpawner(Game game)
        {
            this.game = game;
        }

        public void Update()
        {
            spawnTimer++;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            // 40% Chance to spawn a Coin (Slightly less, more enemies)
            if (random.NextDouble() < 0.4)
            {
                var coin = new Coin();
                coin.Position = new PointF(random.Next(50, game.ScreenWidth - 100), -30);
                coin.Size = new SizeF(100, 100); // Bigger Coins
                coin.Velocity = new PointF(0, EnemySpeed * 0.4f); // Coins scale with enemy speed
                game.AddObject(coin);
                return;
            }

            var enemy = new Enemy();
            enemy.Position = new PointF(random.Next(50, game.ScreenWidth - 150), -180); 
            enemy.Size = new SizeF(120, 190); // Bigger enemies
            
            // Randomly assign movement patterns
            int pattern = random.Next(4);
            switch (pattern)
            {
                case 0:
                    enemy.Movement = new Zigzag(verticalSpeed: EnemySpeed, amplitude: 100);
                    break;
                case 1:
                    enemy.Movement = new Jumping(verticalSpeed: EnemySpeed);
                    break;
                case 2:
                    enemy.Movement = new Vertical(speed: EnemySpeed);
                    break;
                default:
                    // Default downward movement (handled by velocity)
                    enemy.Velocity = new PointF(0, EnemySpeed);
                    break;
            }

            if (EnemyImages.Count > 0)
            {
                enemy.Sprite = EnemyImages[random.Next(EnemyImages.Count)];
            }
            else
            {
                 enemy.Sprite = Enemy.DefaultSprite;
            }
            
            game.AddObject(enemy);
        }

        public List<Image> EnemyImages { get; set; } = new List<Image>();


        public void IncreaseDifficulty()
        {
            // Allow interval to go lower, down to 4
            if (spawnInterval > 4)
                spawnInterval -= 1;
            
            EnemySpeed += 10; // Double the difficulty scaling per level
        }
    }
}
