namespace KinectPong
{
    partial class menuScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(menuScreen));
            this.btnEasy = new System.Windows.Forms.RadioButton();
            this.pnlDifficulty = new System.Windows.Forms.Panel();
            this.btnHard = new System.Windows.Forms.RadioButton();
            this.btnMedium = new System.Windows.Forms.RadioButton();
            this.btn1Plyr = new System.Windows.Forms.RadioButton();
            this.btn2Plyr = new System.Windows.Forms.RadioButton();
            this.pnlPlayers = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlDifficulty.SuspendLayout();
            this.pnlPlayers.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEasy
            // 
            this.btnEasy.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnEasy.Font = new System.Drawing.Font("OCR A Extended", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEasy.Location = new System.Drawing.Point(3, 15);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(85, 41);
            this.btnEasy.TabIndex = 1;
            this.btnEasy.Text = "Easy";
            this.btnEasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEasy.UseVisualStyleBackColor = true;
            this.btnEasy.CheckedChanged += new System.EventHandler(this.btnEasy_CheckedChanged);
            // 
            // pnlDifficulty
            // 
            this.pnlDifficulty.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDifficulty.BackColor = System.Drawing.Color.Transparent;
            this.pnlDifficulty.Controls.Add(this.btnHard);
            this.pnlDifficulty.Controls.Add(this.btnMedium);
            this.pnlDifficulty.Controls.Add(this.btnEasy);
            this.pnlDifficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDifficulty.Location = new System.Drawing.Point(25, 59);
            this.pnlDifficulty.Name = "pnlDifficulty";
            this.pnlDifficulty.Size = new System.Drawing.Size(318, 118);
            this.pnlDifficulty.TabIndex = 2;
            // 
            // btnHard
            // 
            this.btnHard.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnHard.Font = new System.Drawing.Font("OCR A Extended", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHard.Location = new System.Drawing.Point(169, 15);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(83, 41);
            this.btnHard.TabIndex = 1;
            this.btnHard.Text = "Hard";
            this.btnHard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHard.UseVisualStyleBackColor = true;
            this.btnHard.CheckedChanged += new System.EventHandler(this.btnHard_CheckedChanged);
            // 
            // btnMedium
            // 
            this.btnMedium.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnMedium.Font = new System.Drawing.Font("OCR A Extended", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedium.Location = new System.Drawing.Point(86, 15);
            this.btnMedium.Name = "btnMedium";
            this.btnMedium.Size = new System.Drawing.Size(119, 41);
            this.btnMedium.TabIndex = 1;
            this.btnMedium.Text = "Medium";
            this.btnMedium.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMedium.UseVisualStyleBackColor = true;
            this.btnMedium.CheckedChanged += new System.EventHandler(this.btnMedium_CheckedChanged);
            // 
            // btn1Plyr
            // 
            this.btn1Plyr.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn1Plyr.Font = new System.Drawing.Font("OCR A Extended", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1Plyr.Location = new System.Drawing.Point(14, 36);
            this.btn1Plyr.Name = "btn1Plyr";
            this.btn1Plyr.Size = new System.Drawing.Size(123, 41);
            this.btn1Plyr.TabIndex = 1;
            this.btn1Plyr.TabStop = true;
            this.btn1Plyr.Text = "1 Player";
            this.btn1Plyr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn1Plyr.UseVisualStyleBackColor = true;
            this.btn1Plyr.CheckedChanged += new System.EventHandler(this.btn1Plyr_CheckedChanged);
            // 
            // btn2Plyr
            // 
            this.btn2Plyr.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn2Plyr.Font = new System.Drawing.Font("OCR A Extended", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2Plyr.Location = new System.Drawing.Point(131, 36);
            this.btn2Plyr.Name = "btn2Plyr";
            this.btn2Plyr.Size = new System.Drawing.Size(137, 41);
            this.btn2Plyr.TabIndex = 1;
            this.btn2Plyr.TabStop = true;
            this.btn2Plyr.Text = "2 Player";
            this.btn2Plyr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn2Plyr.UseVisualStyleBackColor = true;
            this.btn2Plyr.CheckedChanged += new System.EventHandler(this.btn2Plyr_CheckedChanged);
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlPlayers.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayers.Controls.Add(this.btn1Plyr);
            this.pnlPlayers.Controls.Add(this.btn2Plyr);
            this.pnlPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPlayers.Location = new System.Drawing.Point(28, 183);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(378, 143);
            this.pnlPlayers.TabIndex = 3;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtons.Controls.Add(this.btnOptions);
            this.pnlButtons.Controls.Add(this.btnStart);
            this.pnlButtons.Controls.Add(this.pnlPlayers);
            this.pnlButtons.Controls.Add(this.pnlDifficulty);
            this.pnlButtons.Location = new System.Drawing.Point(206, 60);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(534, 406);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnOptions
            // 
            this.btnOptions.Font = new System.Drawing.Font("OCR A Extended", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptions.Location = new System.Drawing.Point(450, 274);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 5;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("OCR A Extended", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(410, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // frmStartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1075, 652);
            this.Controls.Add(this.pnlButtons);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStartMenu";
            this.Text = "KinectPong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlDifficulty.ResumeLayout(false);
            this.pnlPlayers.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton btnEasy;
        private System.Windows.Forms.Panel pnlDifficulty;
        private System.Windows.Forms.RadioButton btnHard;
        private System.Windows.Forms.RadioButton btnMedium;
        private System.Windows.Forms.RadioButton btn1Plyr;
        private System.Windows.Forms.RadioButton btn2Plyr;
        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnStart;
    }
}

