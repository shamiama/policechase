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
        private int spawnInterval = 12; // Increased slightly from 8 to prevent overcrowding
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
            // 15% Chance to spawn an Energy Booster
            double rand = random.NextDouble();
            if (rand < 0.15)
            {
                var booster = new EnergyBooster(game);
                booster.Position = new PointF(random.Next(50, game.ScreenWidth - 100), -100);
                game.AddObject(booster);
                return;
            }

            // 35% Chance to spawn a Coin
            if (rand < 0.5)
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
            
            float speedVariation = (float)(random.NextDouble() * 10 - 5); // +/- 5 speed variation
            float currentSpeed = EnemySpeed + speedVariation;

            // Randomly assign movement patterns (more likely to be default straight)
            int pattern = random.Next(10); // 0-9
            switch (pattern)
            {
                case 0:
                    enemy.Movement = new Zigzag(verticalSpeed: currentSpeed, amplitude: 80);
                    enemy.Velocity = PointF.Empty; 
                    break;
                case 1:
                    enemy.Movement = new Jumping(verticalSpeed: currentSpeed);
                    enemy.Velocity = PointF.Empty; 
                    break;
                case 2:
                    enemy.Movement = new Vertical(speed: currentSpeed, bottom: 5000f); 
                    enemy.Velocity = PointF.Empty; 
                    break;
                default:
                    enemy.Velocity = new PointF(0, currentSpeed);
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
            // Allow interval to go lower, down to 8 (balanced for speed)
            if (spawnInterval > 8)
                spawnInterval -= 1;
            
            EnemySpeed += 10; 
        }
    }
}
