using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectPong
{
    public partial class optionsMenu : Form
    {
        private Form start;

        public GameOptions gameOptions;
        
        public optionsMenu()
        {
            InitializeComponent();
        }

        private void organizeOptions()
        {
            //this.Icon = KinectPong.Properties.Resources.KinectPong_Logo;
            

            //Start/back
            //startButton.Size = new Size(132, 47);
            //startButton.Location = new Point(1765, 9);
            backButton.Size = new Size(111, 47);
            backButton.Location = new Point(7, 9);

            //Labels
            label6.Size = new Size(688, 152);
            label6.Location = new Point(619, 9);
            label2.Size = new Size(143, 37);
            label2.Location = new Point(0, 220); 
            label3.Size = new Size(227, 37);
            label3.Location = new Point(0, 490);
            label4.Size = new Size(227, 37);
            label4.Location = new Point(0, 760);
        
            //Stages
            stageButton1.Size = new Size(320, 180);
            stageButton1.Location = new Point(92, 260);
            stageButton2.Size = new Size(320, 180);
            stageButton2.Location = new Point(446, 260);
            stageButton3.Size = new Size(320, 180);
            stageButton3.Location = new Point(800, 260);
            stageButton4.Size = new Size(320, 180);
            stageButton4.Location = new Point(1154, 260);
            stageButton5.Size = new Size(320, 180);
            stageButton5.Location = new Point(1508, 260);

            //Modes
            modeButton1.Size = new Size(320, 128);
            modeButton1.Location = new Point(128, 804);
            modeButton2.Size = new Size(320, 128);
            modeButton2.Location = new Point(556, 804);
            modeButton3.Size = new Size(320, 128);
            modeButton3.Location = new Point(984, 804);
            modeButton4.Size = new Size(320, 128);
            modeButton4.Location = new Point(1412, 804);

            //Ball 
            ballButtonWhite.Size = new Size(70, 70);
            ballButtonWhite.Location = new Point(75, 533);
            ballButtonOrange.Size = new Size(70, 70);
            ballButtonOrange.Location = new Point(245, 533);
            ballButtonGreen.Size = new Size(70, 70);
            ballButtonGreen.Location = new Point(415, 533);
            ballButtonPurple.Size = new Size(70, 70);
            ballButtonPurple.Location = new Point(585, 533);
            ballButtonRainbow.Size = new Size(70, 70);
            ballButtonRainbow.Location = new Point(755, 533);
            ballButtonBaseball.Size = new Size(70, 70);
            ballButtonBaseball.Location = new Point(925, 533);
            ballButtonTennis.Size = new Size(70, 70);
            ballButtonTennis.Location = new Point(1095, 533); 
            ballButtonBluePoke.Size = new Size(70, 70);
            ballButtonBluePoke.Location = new Point(1265, 533);
            ballButtonPurplePoke.Size = new Size(70, 70);
            ballButtonPurplePoke.Location = new Point(1435, 533); 
            ballButtonYin.Size = new Size(70, 70);
            ballButtonYin.Location = new Point(1605, 533);
            ballButtonSmiley.Size = new Size(70, 70);
            ballButtonSmiley.Location = new Point(1775, 533);

            ballButtonRed.Size = new Size(70, 70);
            ballButtonRed.Location = new Point(75, 623);
            ballButtonYellow.Size = new Size(70, 70);
            ballButtonYellow.Location = new Point(245, 623);
            ballButtonBlue.Size = new Size(70, 70);
            ballButtonBlue.Location = new Point(415, 623);
            ballButtonBlack.Size = new Size(70, 70);
            ballButtonBlack.Location = new Point(585, 623);
            ballButtonRainbowPattern.Size = new Size(70, 70);
            ballButtonRainbowPattern.Location = new Point(755, 623);
            ballButtonBasketball.Size = new Size(70, 70);
            ballButtonBasketball.Location = new Point(925, 623);
            ballButtonPoke.Size = new Size(70, 70);
            ballButtonPoke.Location = new Point(1095, 623);
            ballButtonYellowPoke.Size = new Size(70, 70);
            ballButtonYellowPoke.Location = new Point(1265, 623);
            ballButtonPeace.Size = new Size(70, 70);
            ballButtonPeace.Location = new Point(1435, 623);
            ballButtonEye.Size = new Size(70, 70);
            ballButtonEye.Location = new Point(1605, 623);
            ballButtonEarth.Size = new Size(70, 70);
            ballButtonEarth.Location = new Point(1775, 623);
        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Close();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }   

        private void Form1_Load(object sender, EventArgs e)
        {
            organizeOptions();
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        //stage buttons
        private void stageButton1_Click(object sender, EventArgs e)
        {
            this.gameOptions.background = "GalacticTitleBase";
        }

        private void stageButton2_Click(object sender, EventArgs e)
        {
            this.gameOptions.background = "DesertLandscape";
        }

        private void stageButton3_Click(object sender, EventArgs e)
        {
            this.gameOptions.background = "mountain";
        }

        private void stageButton4_Click(object sender, EventArgs e)
        {
            this.gameOptions.background = "nebula";
        }

        private void stageButton5_Click(object sender, EventArgs e)
        {
            this.gameOptions.background = "star";
        }


        //ball buttons
        private void ballButtonWhite_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 0;
        }

        private void ballButtonOrange_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 2;
        }

        private void ballButtonGreen_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 4;
        }

        private void ballButtonPurple_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 6;
        }

        private void ballButtonRainbow_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 8;
        }

        private void ballButtonBaseball_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 10;
        }

        private void ballButtonTennis_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 12;
        }

        private void ballButtonBluePoke_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 14;
        }

        private void ballButtonPurplePoke_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 16;
        }

        private void ballButtonYin_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 18;
        }

        private void ballButtonSmiley_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 20;
        }

        private void ballButtonRed_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 1;
        }

        private void button14_Click(object sender, EventArgs e) //yellow
        {
            this.gameOptions.ballColor = 3;
        }

        private void ballButtonBlue_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 5;
        }

        private void ballButtonBlack_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 7;
        }

        private void ballButtonRainbowPattern_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 9;
        }

        private void ballButtonBasketball_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 11;
        }

        private void ballButtonPoke_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 13;
        }

        private void ballButtonYellowPoke_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 15;
        }

        private void ballButtonPeace_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 17;
        }

        private void ballButtonEye_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 19;
        }

        private void ballButtonEarth_Click(object sender, EventArgs e)
        {
            this.gameOptions.ballColor = 21;
        }


        //mode buttons
        private void radioButton1_CheckedChanged(object sender, EventArgs e) //Classic
        {
            this.gameOptions.mode = "Classic";
        }
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e) //RNG
        {
            this.gameOptions.mode = "RNG";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) //GainSpeed
        {
            this.gameOptions.mode = "GainSpeed";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) //Practice
        {
            this.gameOptions.mode = "Practice";
        }

        //back and start
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Hide();
            start.Enabled = true;
            start.Show();
            Console.WriteLine("Hide the Options menu, open the Start menu!");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
        }

        public void optionsStart(Form start, GameOptions gameOptions)
        {
            this.gameOptions = gameOptions;
            this.start = start;
            start.Enabled = false;
            start.Hide();
            this.Enabled = true;
            this.Show();
            Console.WriteLine("Hide the Main menu, open the Options menu!");
        }
    }
}
