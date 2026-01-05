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
    public partial class Form3 : Form
    {
        private System.Windows.Media.MediaPlayer bgMusicPlayer = new System.Windows.Media.MediaPlayer();
        public Form3()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0; // Hide initial draw
            
            button1.Click += (s, e) => 
            {
                PlayClickSound();
                bgMusicPlayer.Stop();
                this.Hide();
                var gameForm = new firstform();
                gameForm.ShowDialog();
                this.Close(); 
            };

            this.Load += (s, e) =>
            {
                // Styling button1
                button1.Text = "START GAME";
                button1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
                button1.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(255, 69, 0); // OrangeRed
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 0;
                button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 99, 71); // Tomato
                button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 20, 60); // Crimson
                button1.Cursor = Cursors.Hand;
                button1.Size = new Size(300, 80);

                // Center button at bottom
                button1.Left = (this.ClientSize.Width - button1.Width) / 2;
                button1.Top = this.ClientSize.Height - button1.Height - 100;
                
                
                // Show form smoothly with a Timer
                System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
                fadeTimer.Interval = 10;
                fadeTimer.Tick += (sender, args) =>
                {
                    if (this.Opacity < 1)
                    {
                        this.Opacity += 0.05;
                    }
                    else
                    {
                        fadeTimer.Stop();
                    }
                };
                fadeTimer.Start();

                InitializeAudio();
            };
        }


        private void InitializeAudio()
        {
            try
            {
               string bgMusicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "form1music.wav");
               if (System.IO.File.Exists(bgMusicPath))
               {
                   bgMusicPlayer.Open(new Uri(bgMusicPath));
                   bgMusicPlayer.MediaEnded += (s, ev) => 
                   { 
                       bgMusicPlayer.Position = TimeSpan.Zero; 
                       bgMusicPlayer.Play(); 
                   };
                   bgMusicPlayer.Volume = 0.5;
                   bgMusicPlayer.Play();
               }
            } catch { }
        }

        private void PlayClickSound()
        {
             try
             {
                 string clickPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "computer-mouse-click-02-383961 (1).wav");
                 if (System.IO.File.Exists(clickPath))
                 {
                     using (var player = new System.Media.SoundPlayer(clickPath))
                     {
                         player.Play();
                     }
                 }
             } catch { }
        }
    }
}
