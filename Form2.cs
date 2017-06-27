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
    public partial class Form2 : Form
    {
        public PictureBox picBoxPlayer1;
        public PictureBox picBoxPlayer2;
        public PictureBox picBoxBall;
        public int playerOnePoints = 0;
        public int playerTwoPoints = 0;
        public const int SCREEN_WIDTH = 1200;
        public const int SCREEN_HEIGHT = 800;

        public GameOptions options;
        Size playerSize = new Size(15, 200);
        Size sizeBall = new Size(32, 32);
        int ballSpeedX = 1;
        int ballSpeedY = 1;
        Random rand;
        //END Variables
        public Form2(GameOptions options)
        {
            InitializeComponent();

            this.Text = "Kinect Pong";

            this.options = options;

            this.Width = SCREEN_WIDTH;
            this.Height = SCREEN_HEIGHT;
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.BackColor = Color.Black;
            
            switch(options.background)
            {
                case "GalacticTitleBase":
                    this.BackgroundImage = KinectPong.Properties.Resources.GalacticTitleBase;
                    break;
                case "DesertLandscape":
                    this.BackgroundImage = KinectPong.Properties.Resources.DesertLandscape;
                    break;
                case "mountain":
                    this.BackgroundImage = KinectPong.Properties.Resources.mountain;
                    break;
                case "nebula":
                    this.BackgroundImage = KinectPong.Properties.Resources.nebula;
                    break;
                case "star":
                    this.BackgroundImage = KinectPong.Properties.Resources.star;
                    break;
                default:
                    this.BackColor = Color.Black;
                    break;
            }

            this.BackgroundImageLayout = ImageLayout.Stretch;

            if (options.numPlayers == 1)
            {
                picBoxPlayer1 = new PictureBox();
                picBoxPlayer1.Size = playerSize;
                picBoxPlayer1.Location = new Point(0, ClientSize.Height / 4);
                picBoxPlayer1.BackColor = Color.SteelBlue;
                this.Controls.Add(picBoxPlayer1);
            }
            else
            {
                picBoxPlayer1 = new PictureBox();

                picBoxPlayer1.Size = playerSize;
                picBoxPlayer1.Location = new Point(0, ClientSize.Height / 4);
                picBoxPlayer1.BackColor = Color.SteelBlue;
                this.Controls.Add(picBoxPlayer1);

                picBoxPlayer2 = new PictureBox();
                picBoxPlayer2.Size = playerSize;
                picBoxPlayer2.Location = new Point(ClientSize.Width - 1, ClientSize.Height / 4);
                picBoxPlayer2.BackColor = Color.Goldenrod;
                this.Controls.Add(picBoxPlayer2);
            }

            picBoxBall = new PictureBox();
            picBoxBall.Image = BallSkins.getBalls(options.ballColor);
            
            picBoxBall.Size = sizeBall;
            picBoxBall.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2); //Ball starting position in middle of screen
            picBoxBall.BackColor = Color.Transparent;
            picBoxBall.Enabled = true;
            this.Controls.Add(picBoxBall);
            
            ballSpeedX *= options.difficulty;
            ballSpeedY *= options.difficulty;
            rand = new Random();

        }

        public Point moveBall(Point p)
        {
            if (intersectsWithPaddle(this.picBoxBall))
            {
                ballSpeedX *= -1;
                if (options.mode.Equals("RNG"))
                {
                    if (ballSpeedX < 0) // Moving left
                    {
                        ballSpeedX = rand.Next(-20, -5);
                        ballSpeedY = rand.Next(-options.difficulty, options.difficulty);
                        picBoxPlayer2.Size = new Size(15, rand.Next(20, 400));
                    }
                    else // Moving right
                    {
                        ballSpeedX = rand.Next(5, 20);
                        ballSpeedY = rand.Next(-options.difficulty, options.difficulty);
                        picBoxPlayer1.Size = new Size(15, rand.Next(20, 400));

                    }
                }
                else if (options.mode.Equals("GainSpeed"))
                {
                    if (ballSpeedX < 0)
                    {
                        ballSpeedX -= 5;
                        if (ballSpeedY < 0)
                            ballSpeedY -= 5;
                        else
                            ballSpeedY += 5;
                    }
                    else
                    {
                        ballSpeedX += 5;
                        if (ballSpeedY < 0)
                            ballSpeedY -= 5;
                        else
                            ballSpeedY += 5;
                    }
                }
                if (options.numPlayers == 1)
                    playerOnePoints++;
            }
            if (p.X + ballSpeedX < 0)
            {
                ballSpeedX = options.difficulty;
                ballSpeedY = options.difficulty;
                ballSpeedX *= -1;
                //endgame
                //Console.Write("Game Ended. Your Score:  " + gameScore);
                //this.Close();
                p.X = ClientSize.Width / 2;
                p.Y = ClientSize.Height / 2;
                this.playerTwoPoints++;
                if (options.mode.Equals("RNG") && picBoxPlayer2 != null)
                {
                    picBoxPlayer1.Size = new Size(15, 200);
                }
                if (options.numPlayers == 1)
                    playerOnePoints = 0;

            }
            if (p.X + ballSpeedX > ClientSize.Width)
            {
                ballSpeedX *= -1;
                //Increases ball speed on every bounce
                //if (ballSpeedX < 0)
                //    ballSpeedX -= 2;
                //else
                //    ballSpeedX += 2;
                if (!(options.numPlayers == 1 || options.mode.Equals("Practice")))
                {
                    p.X = ClientSize.Width / 2;
                    p.Y = ClientSize.Height / 2;
                    this.playerOnePoints++;
                }
                if (options.mode.Equals("RNG") && picBoxPlayer2 != null)
                    picBoxPlayer2.Size = new Size(15, 200);
            }
            if (p.Y + ballSpeedY > ClientSize.Height || p.Y + ballSpeedY < 0)
            {
                ballSpeedY *= -1;
                //Increases ball speed on every bounce
                //if (ballSpeedY < 0)
                //    ballSpeedY -= 2;
                //else
                //    ballSpeedY += 2;
            }
            return new Point(p.X += ballSpeedX, p.Y += ballSpeedY);
        }

        private bool intersectsWithPaddle(PictureBox ball)
        {//fixed
            if (ball.Location.Y < picBoxPlayer1.Location.Y + picBoxPlayer1.Size.Height && ball.Location.Y + ball.Size.Height > picBoxPlayer1.Location.Y)
            {
                if (ball.Location.X < picBoxPlayer1.Size.Width)
                {
                    return true;
                }
            }
            if (options.numPlayers == 2 && ball.Location.Y < picBoxPlayer2.Location.Y + picBoxPlayer2.Size.Height && ball.Location.Y + ball.Size.Height > picBoxPlayer2.Location.Y)
            {
                if (ball.Location.X > picBoxPlayer2.Location.X - 10)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
