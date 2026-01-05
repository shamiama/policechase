namespace policechase
{
    partial class firstform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(firstform));
            mainpanel = new Panel();
            labelscore = new Label();
            labellevel = new Label();
            enemycar2 = new PictureBox();
            enemycar1 = new PictureBox();
            pictureBox4 = new PictureBox();
            playercar = new PictureBox();
            fireimage = new PictureBox();
            enemycar3 = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timer1 = new System.Windows.Forms.Timer(components);
            mainpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enemycar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemycar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playercar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fireimage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemycar3).BeginInit();
            SuspendLayout();
            // 
            // mainpanel
            // 
            mainpanel.BackColor = SystemColors.ActiveCaptionText;
            mainpanel.BackgroundImage = (Image)resources.GetObject("mainpanel.BackgroundImage");
            mainpanel.BackgroundImageLayout = ImageLayout.Stretch;
            mainpanel.Controls.Add(labelscore);
            mainpanel.Controls.Add(labellevel);
            mainpanel.Controls.Add(enemycar2);
            mainpanel.Controls.Add(enemycar1);
            mainpanel.Controls.Add(pictureBox4);
            mainpanel.Controls.Add(playercar);
            mainpanel.Controls.Add(fireimage);
            mainpanel.Controls.Add(enemycar3);
            mainpanel.Dock = DockStyle.Fill;
            mainpanel.Location = new Point(0, 0);
            mainpanel.Name = "mainpanel";
            mainpanel.Size = new Size(493, 764);
            mainpanel.TabIndex = 0;
            mainpanel.Paint += mainpanel_Paint_1;
            // 
            // labelscore
            // 
            labelscore.BackColor = Color.Transparent;
            labelscore.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelscore.ForeColor = Color.Yellow;
            labelscore.Location = new Point(0, 0);
            labelscore.Name = "labelscore";
            labelscore.Size = new Size(mainpanel.Width, 80);
            labelscore.TabIndex = 1;
            labelscore.Text = "SCORE:0";
            labelscore.TextAlign = ContentAlignment.TopRight;
            labelscore.Click += labelscore_Click;
            // 
            // labellevel
            // 
            labellevel.AutoSize = true;
            labellevel.BackColor = Color.Transparent;
            labellevel.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labellevel.ForeColor = Color.Lime;
            labellevel.Location = new Point(12, 10);
            labellevel.Name = "labellevel";
            labellevel.Size = new Size(180, 65);
            labellevel.TabIndex = 4;
            labellevel.Text = "Level:1";
            // 
            // enemycar2
            // 
            enemycar2.BackColor = Color.Transparent;
            enemycar2.BackgroundImage = (Image)resources.GetObject("enemycar2.BackgroundImage");
            enemycar2.BackgroundImageLayout = ImageLayout.Stretch;
            enemycar2.Location = new Point(336, 50);
            enemycar2.Name = "enemycar2";
            enemycar2.Size = new Size(78, 85);
            enemycar2.TabIndex = 1;
            enemycar2.TabStop = false;
            // 
            // enemycar1
            // 
            enemycar1.BackColor = Color.Transparent;
            enemycar1.BackgroundImage = (Image)resources.GetObject("enemycar1.BackgroundImage");
            enemycar1.BackgroundImageLayout = ImageLayout.Stretch;
            enemycar1.Location = new Point(79, 50);
            enemycar1.Name = "enemycar1";
            enemycar1.Size = new Size(87, 85);
            enemycar1.TabIndex = 0;
            enemycar1.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.Location = new Point(143, 195);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(216, 89);
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            pictureBox4.Visible = false;
            // 
            // playercar
            // 
            playercar.BackColor = Color.Transparent;
            playercar.BackgroundImage = (Image)resources.GetObject("playercar.BackgroundImage");
            playercar.BackgroundImageLayout = ImageLayout.Stretch;
            playercar.Location = new Point(192, 407);
            playercar.Name = "playercar";
            playercar.Size = new Size(101, 103);
            playercar.TabIndex = 5;
            playercar.TabStop = false;
            // 
            // fireimage
            // 
            fireimage.BackColor = Color.Transparent;
            fireimage.BackgroundImage = (Image)resources.GetObject("fireimage.BackgroundImage");
            fireimage.BackgroundImageLayout = ImageLayout.Stretch;
            fireimage.Location = new Point(57, 319);
            fireimage.Name = "fireimage";
            fireimage.Size = new Size(109, 82);
            fireimage.TabIndex = 4;
            fireimage.TabStop = false;
            fireimage.Visible = false;
            fireimage.Click += pictureBox5_Click;
            // 
            // enemycar3
            // 
            enemycar3.BackColor = Color.Transparent;
            enemycar3.BackgroundImage = (Image)resources.GetObject("enemycar3.BackgroundImage");
            enemycar3.BackgroundImageLayout = ImageLayout.Stretch;
            enemycar3.Location = new Point(352, 305);
            enemycar3.Name = "enemycar3";
            enemycar3.Size = new Size(102, 96);
            enemycar3.TabIndex = 2;
            enemycar3.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;
            // 
            // firstform
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 764);
            Controls.Add(mainpanel);
            KeyPreview = true;
            Name = "firstform";
            Text = "firstform";
            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)enemycar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemycar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)playercar).EndInit();
            ((System.ComponentModel.ISupportInitialize)fireimage).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemycar3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainpanel;
        private Label labelscore;
        private PictureBox playercar;
        private PictureBox fireimage;
        private PictureBox pictureBox4;
        private PictureBox enemycar3;
        private PictureBox enemycar2;
        private PictureBox enemycar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label labellevel;
        private System.Windows.Forms.Timer timer1;
    }
}