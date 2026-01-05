using policechase.Core;
using policechase.Entiities;
using policechase.Movements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace policechase
{
    public partial class firstform : Form
    {
        private Game _game;
        private Player _player;
        private Image _backgroundImage;
        private float _backgroundOffset = 0;
        private float _backgroundSpeed = 15f;


        public firstform()
        {
            InitializeComponent();
            mainpanel.Paint += mainpanel_Paint; // Subscribe to paint event manually
            this.DoubleBuffered = true; // Reduce flickering
            this.KeyPreview = true; // Allow form to catch keys

            // Hide designer controls as we will draw manually
            playercar.Visible = false;
            enemycar1.Visible = false;
            enemycar2.Visible = false;
            enemycar3.Visible = false;
            fireimage.Visible = false;

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0; // Hide flicker
            
            // Fix "Glitch"/Flickering by enabling Double Buffering on the Panel via Reflection
            typeof(Panel).InvokeMember("DoubleBuffered", 
                System.Reflection.BindingFlags.SetProperty | 
                System.Reflection.BindingFlags.Instance | 
                System.Reflection.BindingFlags.NonPublic, 
                null, mainpanel, new object[] { true });

            // Capture background image for manual scrolling
            _backgroundImage = mainpanel.BackgroundImage;
            mainpanel.BackgroundImage = null;

            InitializeAudio();

            this.Load += (s, e) => StartGame();
            this.KeyDown += firstform_KeyDown;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Opacity = 1; // Show when ready
        }


        private System.Windows.Media.MediaPlayer bgMusicPlayer = new System.Windows.Media.MediaPlayer();
        private System.Windows.Media.MediaPlayer crashSoundPlayer = new System.Windows.Media.MediaPlayer();
        private System.Windows.Media.MediaPlayer explosionSoundPlayer = new System.Windows.Media.MediaPlayer();
        private System.Windows.Media.MediaPlayer coinSoundPlayer = new System.Windows.Media.MediaPlayer();
        
        // Announcement Controls
        private string _announcementMessage = "";
        private Color _announcementColor = Color.Yellow;
        private float _announcementAlpha = 0; // 0 to 255
        private int _announcementTick = 0; // For animations
        private int lastLevel = 0;
        private Random _random = new Random();

        private void StartGame()
        {
            _game = new Game();
            _game.ScreenWidth = mainpanel.Width;
            _game.ScreenHeight = mainpanel.Height;

            // Initialize Player
            _player = new Player();
            _player.Position = new PointF(mainpanel.Width / 2 - 25, mainpanel.Height - 100);
            _player.Size = new SizeF(130, 200); // Even bigger size

            _player.Sprite = playercar.BackgroundImage;
            _player.Movement = new KeyboardMovement()
            {
                Speed = 30, // Extreme Speed
                Bounds = new RectangleF(0, 0, mainpanel.Width, mainpanel.Height)
            };

            // Hardcode size to be reasonable on screen
            // _player.Size = new SizeF(100, 160); // Scaled up for big screen

            _game.AddObject(_player);
            _player.CoinCollected += () => {
                try 
                {
                    coinSoundPlayer.Position = TimeSpan.Zero;
                    coinSoundPlayer.Play();
                } catch { }
            };

            // Pass enemy images
            if (enemycar1.BackgroundImage != null) _game.Spawner.EnemyImages.Add(enemycar1.BackgroundImage);
            if (enemycar2.BackgroundImage != null) _game.Spawner.EnemyImages.Add(enemycar2.BackgroundImage);
            if (enemycar3.BackgroundImage != null) _game.Spawner.EnemyImages.Add(enemycar3.BackgroundImage);

            Enemy.DefaultSprite = enemycar1.BackgroundImage;

            labelscore.Text = "Score: 0";
            labellevel.Text = "Level: 1";

            // Announcement state will be handled via variables and drawing
            // instead of a Label for better beauty/fading.


            // Use existing timer
            timer1.Interval = 10;
            timer1.Start();
            mainpanel.Invalidate();

            // Audio - Play
            try 
            {
               bgMusicPlayer.Position = TimeSpan.Zero;
               bgMusicPlayer.Play();
            } catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_game == null) return;

            if (_game.IsGameOver)
            {
                timer1.Stop();
                try 
                {
                    bgMusicPlayer.Stop();
                    
                    crashSoundPlayer.Position = TimeSpan.Zero;
                    crashSoundPlayer.Play();

                    explosionSoundPlayer.Position = TimeSpan.Zero;
                    explosionSoundPlayer.Play();

                } catch { }

                string medal = GetMedal(_game.Score);
                using (var gameOverForm = new form2(_game.Score, medal))
                {
                    var result = gameOverForm.ShowDialog();
                    
                    if (result == DialogResult.Retry)
                    {
                        StartGame();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                return;
            }

            // Update Game
            _game.Update(new GameTime());

            // Update UI & Difficulty
            UpdateLevelState();
            
            // Update scrolling background
            // Background speed starts at 25 and increases VERY aggressively with score
            float targetBackgroundSpeed = 25f + (_game.Score / 10f); 
            // Clamp speed at a very high limit for maximum intensity
            _backgroundSpeed = Math.Min(targetBackgroundSpeed, 120f);

            _backgroundOffset += _backgroundSpeed;
            if (_backgroundImage != null && _backgroundOffset >= mainpanel.Height)
            {
                _backgroundOffset -= mainpanel.Height;
            }

            // Handle announcement fading
            if (_announcementAlpha > 0)
            {
                _announcementAlpha -= 2; // Fade out slowly
                _announcementTick++;
                if (_announcementAlpha < 0) _announcementAlpha = 0;
            }

            labelscore.Text = $"Score:{_game.Score} Coins:{_player.CoinCount} Best:{_game.HighScore}";

            // Redraw
            mainpanel.Invalidate();
        }

        // Restoring event handlers required by Designer
        private void firstform_Load(object sender, EventArgs e) { }
        private void labelscore_Click(object sender, EventArgs e) { }
        private void pictureBox5_Click(object sender, EventArgs e) { }

        private string GetMedal(int score)
        {
            if (score >= 1000) return "Gold";
            if (score >= 500) return "Silver";
            if (score >= 200) return "Bronze";
            return "Nothing";
        }

        // Handle Painting
        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {
            if (_game != null)
            {
                // ENHANCE IMAGE QUALITY
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Draw Scrolling Background
                if (_backgroundImage != null)
                {
                    // Draw two copies of the background image to create seamless scrolling
                    // First copy
                    e.Graphics.DrawImage(_backgroundImage, 0, _backgroundOffset, mainpanel.Width, mainpanel.Height);
                    // Second copy (above the first)
                    e.Graphics.DrawImage(_backgroundImage, 0, _backgroundOffset - mainpanel.Height, mainpanel.Width, mainpanel.Height);
                }
                else
                {
                    e.Graphics.Clear(Color.Black);
                }

                _game.Draw(e.Graphics);

                // Draw Announcement (Level Up Message)
                if (_announcementAlpha > 0 && !string.IsNullOrEmpty(_announcementMessage))
                {
                    using (Font font = new Font("Segoe UI Black", 48, FontStyle.Bold | FontStyle.Italic))
                    {
                        string[] lines = _announcementMessage.Split('\n');
                        float startY = mainpanel.Height / 4;
                        
                        foreach (var line in lines)
                        {
                            SizeF size = e.Graphics.MeasureString(line, font);
                            float x = (mainpanel.Width - size.Width) / 2;
                            float y = startY;

                            // GLITCH EFFECT: Draw RGB Split
                            if (_random.Next(10) > 7) // Occasional glitch
                            {
                                float jitterX = _random.Next(-5, 6);
                                float jitterY = _random.Next(-2, 3);

                                using (SolidBrush cyanBrush = new SolidBrush(Color.FromArgb((int)(_announcementAlpha * 0.6f), Color.Cyan)))
                                {
                                    e.Graphics.DrawString(line, font, cyanBrush, x - jitterX, y);
                                }
                                using (SolidBrush magentaBrush = new SolidBrush(Color.FromArgb((int)(_announcementAlpha * 0.6f), Color.Magenta)))
                                {
                                    e.Graphics.DrawString(line, font, magentaBrush, x + jitterX, y);
                                }
                            }

                            // Draw shadow
                            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb((int)(_announcementAlpha * 0.4f), Color.Black)))
                            {
                                e.Graphics.DrawString(line, font, shadowBrush, x + 4, y + 4);
                            }

                            // SHINY EFFECT: Draw with moving LinearGradient
                            float shineOffset = (_announcementTick * 10) % (mainpanel.Width + 400);
                            RectangleF textRect = new RectangleF(x, y, size.Width, size.Height);
                            
                            using (var lgb = new System.Drawing.Drawing2D.LinearGradientBrush(
                                new PointF(x + shineOffset - 200, y), 
                                new PointF(x + shineOffset, y),
                                Color.FromArgb((int)_announcementAlpha, _announcementColor),
                                Color.White))
                            {
                                // Create a shiny metallic pulse
                                System.Drawing.Drawing2D.ColorBlend cb = new System.Drawing.Drawing2D.ColorBlend();
                                cb.Positions = new[] { 0f, 0.5f, 1f };
                                cb.Colors = new[] { 
                                    Color.FromArgb((int)_announcementAlpha, _announcementColor), 
                                    Color.White, 
                                    Color.FromArgb((int)_announcementAlpha, _announcementColor) 
                                };
                                lgb.InterpolationColors = cb;

                                e.Graphics.DrawString(line, font, lgb, x, y);
                            }
                            
                            startY += size.Height + 10;
                        }
                    }
                }

                if (_game.IsGameOver)
                {
                    if (_player != null && fireimage.BackgroundImage != null)
                    {
                        e.Graphics.DrawImage(fireimage.BackgroundImage,
                           _player.Position.X, _player.Position.Y, 50, 50);
                    }
                    // Manual drawing of posters, medals, and score removed as per user request (added to pictureBox instead)
                }
            }
        }

        private void firstform_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _player?.Jump();
            }
        }

        private void mainpanel_Paint_1(object sender, PaintEventArgs e)
        {
        }



        private void InitializeAudio()
        {
            try
            {
               string bgMusicPath = ExtractResource(Properties.Resources.old_car_racing_game_71720, "bgmusic.mp3");
               if (bgMusicPath != null)
               {
                   bgMusicPlayer.Open(new Uri(bgMusicPath));
                   bgMusicPlayer.MediaEnded += (s, ev) => 
                   { 
                       bgMusicPlayer.Position = TimeSpan.Zero; 
                       bgMusicPlayer.Play(); 
                   };
                   bgMusicPlayer.Volume = 0.5;
               }

               string crashPath = ExtractResource(Properties.Resources.car_crash_sound_effect_376874, "crash.mp3");
               if (crashPath != null) crashSoundPlayer.Open(new Uri(crashPath));

               string explosionPath = ExtractResource(Properties.Resources.explosion_fx_343683, "explosion.mp3");
               if (explosionPath != null) explosionSoundPlayer.Open(new Uri(explosionPath));

               string coinPath = ExtractResource(Properties.Resources.coin_recieved_230517, "coin.mp3");
               if (coinPath != null) coinSoundPlayer.Open(new Uri(coinPath));

            } catch (Exception ex) { 
                System.Diagnostics.Debug.WriteLine("Audio Init Error: " + ex.Message);
            }
        }


        private void UpdateLevelState()
        {
            if (_game.Level == lastLevel) return;
            lastLevel = _game.Level;

            string mode = "Easy Mode";
            int basePlayerSpeed = 30;
            int baseEnemySpeed = 30;
            Color msgColor = Color.LightGreen;

            if (_game.Level == 1)
            {
                mode = "FAST MODE";
                basePlayerSpeed = 60;
                baseEnemySpeed = 65;
                msgColor = Color.Cyan; 
            }
            else if (_game.Level == 2)
            {
                mode = "SONIC MODE";
                basePlayerSpeed = 90; 
                baseEnemySpeed = 95; 
                msgColor = Color.DeepPink; 
            }
            else if (_game.Level >= 3)
            {
                mode = "HYPER-DRIVE MODE";
                basePlayerSpeed = 120; 
                baseEnemySpeed = 130; 
                msgColor = Color.Red; 
            }

            // Show Announcement
            _announcementMessage = $"LEVEL {_game.Level}\n{mode}";
            _announcementColor = msgColor;
            _announcementAlpha = 255; // Reset to full opacity

            labellevel.Text = $"Level {_game.Level}";
            
            // Dynamic Speed Adjustment
            if (_player.Movement is KeyboardMovement km)
            {
                 km.Speed = basePlayerSpeed;
            }
            _game.Spawner.EnemySpeed = baseEnemySpeed;
            
            // Sync background speed with difficulty (Faster for intense feeling)
            _backgroundSpeed = baseEnemySpeed * 0.7f;
        }

        private string ExtractResource(System.IO.Stream stream, string fileName)
        {
            try
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileName);
                // Check if file exists and is not locked, or just overwrite?
                // For simplicity, if it exists, we assume it's good, or we overwrite.
                // But if the previous instance of the app or player has it locked, it might fail.
                // Let's try to overwrite.
                
                using (var fileStream = System.IO.File.Create(path))
                {
                    stream.CopyTo(fileStream);
                }
                return path;
            }
            catch 
            {
                // If we can't write (e.g. valid path but locked), return the path anyway if it exists
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileName);
                if (System.IO.File.Exists(path)) return path;
                return null;
            }
        }
    }
}
