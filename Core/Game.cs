using policechase.Entiities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policechase.Core
{

    public partial class Game
    {
        private List<GameObject> objects = new List<GameObject>();
        private CollisionSystem collisionSystem = new CollisionSystem();
        public EnemySpawner Spawner { get; private set; }

        public List<GameObject> Objects => objects;

        public int Score { get; private set; } = 0;
        public int HighScore { get; private set; } = 0;
        public int Level { get; private set; } = 1;
        public bool IsGameOver { get; private set; } = false;
        
        // Define screen bounds for spawning/cleanup
        public int ScreenWidth { get; set; } = 500;
        public int ScreenHeight { get; set; } = 600;

        public Game()
        {
             Spawner = new EnemySpawner(this);
             HighScore = HighScoreManager.LoadHighScore();
        }

        /// Add a game object to the scene.
        /// Encapsulation: Game manages the collection so external code doesn't manipulate the list directly.
        public void AddObject(GameObject obj)
        {
            objects.Add(obj);
        }

        /// Update all active objects. The Game is responsible for iterating objects and orchestrating the update sequence.
        /// This separation of concerns keeps the game loop logic central and simple.
        public void Update(GameTime gameTime)
        {
            if (IsGameOver) return;

            // Spawning
            Spawner.Update();

            // Update Objects
            foreach (var obj in objects.Where(o => o.IsActive).ToList()) // ToList to allow modification during iteration
            {
                obj.Update(gameTime);
                
                // Cleanup off-screen enemies and coins
                if (obj.Position.Y > ScreenHeight)
                {
                    if (obj is Enemy)
                    {
                        obj.IsActive = false;
                        IncreaseScore(10); 
                    }
                    else if (obj is Coin)
                    {
                        obj.IsActive = false; // Just remove, no penalty/score
                    }
                }
            }

            // Collisions
            collisionSystem.Check(objects);

            // Cleanup
            Cleanup();

            // Check Game Over condition
            if (objects.OfType<Player>().Any(p => p.Health <= 0))
            {
                IsGameOver = true;
                HighScoreManager.RecordScore(Score);
            }
        }

        public void IncreaseScore(int amount)
        {
            int oldScore = Score;
            Score += amount;

            if (Score > HighScore)
            {
                HighScore = Score;
            }

            // Level up every 300 points
            if (Score / 300 > oldScore / 300)
            {
                Level++;
                Spawner.IncreaseDifficulty();
            }
        }

        /// Draw all active objects by delegating to each object's Draw implementation (polymorphism).
        public void Draw(Graphics g)
        {
            foreach (var obj in objects.Where(o => o.IsActive))
            {
                obj.Draw(g);
            }
        }

        /// Remove objects that are no longer active; keeps the object list clean.
        public void Cleanup()
        {
            objects.RemoveAll(o => !o.IsActive);
        }
    }
}