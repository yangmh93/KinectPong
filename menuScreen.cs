using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KinectPong
{
    public class GameOptions
    {
        public int numPlayers = 1;

        public int difficulty = 1;

        public String mode = "Classic";

        public String background = "";

        public int ballColor = 0;
    }

    public partial class menuScreen : Form
    {
        
        private int screenW;
        private int screenH;

        private int difficulty;
        private int players;

        public MainWindow main = (MainWindow)App.Current.MainWindow;

        public GameOptions gameOptions = new GameOptions();

        private optionsMenu options = new optionsMenu();

        public menuScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            screenW = Screen.FromControl(this).Bounds.Width;
            screenH = Screen.FromControl(this).Bounds.Height;

            difficulty = 1;
            players = 1;
            
            organizeStart();
        }

        private void organizeStart()
        {
           
            this.Size = new Size(new Point(screenW, screenH));

            pnlButtons.Size = new Size(new Point((int)(this.Width * 0.25), (int)(this.Height * 0.5) - 50));

            btnStart.Size = new Size(new Point((int)(pnlButtons.Width / 2), (int)(pnlButtons.Height / 5)));
            pnlDifficulty.Size = new Size(new Point((int)(pnlButtons.Width), (int)(pnlButtons.Height / 7)));
            btnEasy.Size = new Size(new Point((int)(pnlButtons.Width / 4), (int)(pnlButtons.Height / 5)));
            btnMedium.Size = new Size(new Point((int)(pnlButtons.Width / 4) + 32, (int)(pnlButtons.Height / 5)));
            btnHard.Size = new Size(new Point((int)(pnlButtons.Width / 4), (int)(pnlButtons.Height / 5)));
            pnlPlayers.Size = new Size(new Point((int)(pnlButtons.Width), (int)(pnlButtons.Height / 7)));
            btn1Plyr.Size = new Size(new Point((int)(pnlButtons.Width / 4) + 30, (int)(pnlButtons.Height / 5)));
            btn2Plyr.Size = new Size(new Point((int)(pnlButtons.Width / 4) + 30, (int)(pnlButtons.Height / 5)));
            btnOptions.Size = new Size(new Point((int)(pnlButtons.Width / 2), (int)(pnlButtons.Height / 5)));


            pnlButtons.Location = new Point((int)(this.Width * 0.17), (this.Height / 2) - (pnlButtons.Height / 3));

            btnStart.Location = new Point((pnlButtons.Width / 2) - (btnStart.Width / 2), pnlButtons.Height / 20);
            pnlDifficulty.Location = new Point(0, btnStart.Bottom + (pnlButtons.Height / 20));
            btnEasy.Location = new Point((pnlDifficulty.Width / 4) - (btnEasy.Width / 2) - 32, (pnlDifficulty.Height / 2) - (btnEasy.Height / 2));
            btnMedium.Location = new Point((pnlDifficulty.Width / 2) - (btnMedium.Width / 2), (pnlDifficulty.Height / 2) - (btnMedium.Height / 2));
            btnHard.Location = new Point(((int)(pnlDifficulty.Width * .75)) - (btnHard.Width / 2) + 32, (pnlDifficulty.Height / 2) - (btnHard.Height / 2));
            pnlPlayers.Location = new Point(0, pnlDifficulty.Bottom + (pnlButtons.Height / 20));
            btn1Plyr.Location = new Point((pnlButtons.Width / 2) - btn1Plyr.Width - 1, (pnlPlayers.Height / 2) - (btn1Plyr.Height / 2));
            btn2Plyr.Location = new Point((pnlButtons.Width / 2) + 1, (pnlPlayers.Height / 2) - (btn2Plyr.Height / 2));
            btnOptions.Location = new Point((pnlButtons.Width / 2) - (btnOptions.Width / 2), pnlPlayers.Bottom + (pnlButtons.Height / 20));

            btnStart.BackColor = Color.WhiteSmoke;
            btnEasy.BackColor = Color.WhiteSmoke;
            btnMedium.BackColor = Color.WhiteSmoke;
            btnHard.BackColor = Color.WhiteSmoke;
            btn1Plyr.BackColor = Color.WhiteSmoke;
            btn2Plyr.BackColor = Color.WhiteSmoke;
            btnOptions.BackColor = Color.WhiteSmoke;

            btnEasy.Checked = true;
            btnMedium.Checked = false;
            btnHard.Checked = false;
            btn1Plyr.Checked = true;
            btn2Plyr.Checked = false;
            this.BackgroundImage = KinectPong.Properties.Resources.GalacticTitleBase;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Mode " + gameOptions.mode);
            //this.Hide();
            //gamePlay.updateValues(difficulty, players);
            //gamePlay.Show();
            this.Hide();
            main.startGame(gameOptions);
            
            Console.WriteLine("Start the game!");
            Console.WriteLine("Num Players " + gameOptions.numPlayers);
            Console.WriteLine("Difficulty " + gameOptions.difficulty);
            Console.WriteLine("Mode " + gameOptions.mode);
        }
    
        private void btnOptions_Click(object sender, EventArgs e)
        {
            options.optionsStart(this, gameOptions);
        }

        private void btnEasy_CheckedChanged(object sender, EventArgs e)
        {
            if (btnEasy.Checked == true)
            {
                gameOptions.difficulty = 5;
                Console.WriteLine(gameOptions.difficulty);
                btnMedium.Checked = false;
                btnHard.Checked = false;
            }
        }

        private void btnMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (btnMedium.Checked == true)
            {
                gameOptions.difficulty = 10;
                Console.WriteLine(gameOptions.difficulty);
                btnEasy.Checked = false;
                btnHard.Checked = false;
            }
        }

        private void btnHard_CheckedChanged(object sender, EventArgs e)
        {
            if (btnHard.Checked == true)
            {
                gameOptions.difficulty = 15;
                Console.WriteLine(gameOptions.difficulty);
                btnEasy.Checked = false;
                btnMedium.Checked = false;
            }
        }

        private void btn1Plyr_CheckedChanged(object sender, EventArgs e)
        {
            if (btn1Plyr.Checked == true)
            {
                gameOptions.numPlayers = 1;
                Console.WriteLine(gameOptions.numPlayers);
                btn2Plyr.Checked = false;
            }
        }

        private void btn2Plyr_CheckedChanged(object sender, EventArgs e)
        {
            if (btn2Plyr.Checked == true)
            {
                gameOptions.numPlayers = 2;
                Console.WriteLine(gameOptions.numPlayers);
                btn1Plyr.Checked = false;
            }
        }

    }

    

}
