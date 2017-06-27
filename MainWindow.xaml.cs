using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace KinectPong
{

    public class Player
    {
        public Point handLocation;
        public Brush playerBrush;
        public bool isHandOpen;
        public bool isHandClosed;
        public bool useLeftHand;
        public bool needsAllocation;
        public ulong trackingID;
        public int score;

        public Point getHandLocation()
        {
            return new Point(handLocation.X / MainWindow.gridWidth, handLocation.Y / MainWindow.gridHeight);
        }

        public double move()
        {
            Point handLoc = getHandLocation();
            if (handLoc.Y > .2 && handLoc.Y < .8)
            {
                return handLoc.Y * 800 - 100;
            }
            if (handLoc.Y <= .2)
            {
                return 0;
            }
            if (handLoc.Y >= .8)
            {
                return 600;
            }

            return 300;//default middle of paddle at y=400
        }
    }

    

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Player playerOne;

        public Player playerTwo;

        public GameOptions options;

        public const double gridHeight = 600, gridWidth = 600;

        // Radius of drawn hand circles
        private const double HandSize = 30;

        // Thickness of drawn joint lines
        private const double JointThickness = 3;

        // Thickness of clip edge rectangles
        private const double ClipBoundsThickness = 10;

        // Constant for clamping Z values of camera space points from being negative
        private const float InferredZPositionClamp = 0.1f;

        // Brush used for drawing hands that are currently tracked as closed
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        // Brush used for drawing hands that are currently tracked as opened
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        // Brush used for drawing hands that are currently tracked as in lasso (pointer) position
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        // Brush used for drawing joints that are currently tracked
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        // Brush used for drawing joints that are currently inferred
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        // Pen used for drawing bones that are currently inferred
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        // Drawing group for body rendering output
        private DrawingGroup drawingGroup;

        // Drawing image that we will display
        private DrawingImage imageSource;

        // Active Kinect sensor
        private KinectSensor kinectSensor = null;

        // Coordinate mapper to map one type of point to another
        private CoordinateMapper coordinateMapper = null;

        // Reader for body frames
        private BodyFrameReader bodyFrameReader = null;

        // Array for the bodies
        private Body[] bodies = null;

        // Definition of bones
        private List<Tuple<JointType, JointType>> bones;

        // Width of display (depth space)
        private int displayWidth;

        // Height of display (depth space)
        private int displayHeight;

        // List of colors for each body tracked
        private List<Pen> bodyColors;

        // Current status text to display
        private string statusText = null;

        // Menu Window
        menuScreen menuForm;

        // Game Window
        Form2 gameForm;

        // INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        public event PropertyChangedEventHandler PropertyChanged;

        // Initializes a new instance of the MainWindow class.
        public MainWindow()
        {
            initializeWindow();
        }

        public void initializeWindow()
        {
            // The current Kinect Sensor
            this.kinectSensor = KinectSensor.GetDefault();

            // Coordinate Mapper
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // Depth display information
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // Set size of the space
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;

            // The body frame reader
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // Add the bones to a list
            addBones();

            // Add the body colors to a list
            addBodyColors();

            // Set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // Open Kinect Sensor
            this.kinectSensor.Open();

            // Set status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.NoSensorStatusText;

            // Create Drawing group for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Use the window object as the view model in this simple example
            this.DataContext = this;

            // Initialize the components (controls) of the window
            this.InitializeComponent();
        }

        /**
         * addBones creates all the bone types between joints
         */
        public void addBones()
        {
            // A bone is defined as a line between 2 joints.
            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));
        }

        /**
         * addBodyColors creates the colors to be assigned to players
         */
        public void addBodyColors()
        {
            this.bodyColors = new List<Pen>();

            this.bodyColors.Add(new Pen(Brushes.SteelBlue, 6));
            this.bodyColors.Add(new Pen(Brushes.Goldenrod, 6));
            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6));
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));
        }

        /**
         * loadGameMenu starts the game menu
         */
        public void loadGameMenu()
        {
            menuForm = new menuScreen();
            //menuForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //menuForm.Location = new System.Drawing.Point(600, 0);
            menuForm.Show();
        }

        /**
         * startGame starts the game window
         */
        public void startGame(GameOptions options)
        {
            gameForm = new Form2(options);
            gameForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            gameForm.Location = new System.Drawing.Point(730, 0);
            gameForm.Show();
        }

        /**
         * playGame plays the game with the selected options
         */
        public void playGame()
        {
            if (gameForm != null)
            {
                scoreOneLabel.Content = gameForm.playerOnePoints;
                //Console.WriteLine("player 1 id: " + playerId1 + " player 2 id: " + playerId2);)
                gameForm.picBoxPlayer1.Location = new System.Drawing.Point(0, (int)playerOne.move());
                if (gameForm.picBoxPlayer2 != null)
                {
                    gameForm.picBoxPlayer2.Location = new System.Drawing.Point(gameForm.ClientSize.Width - 15, (int)playerTwo.move());
                    scoreTwoLabel.Content = gameForm.playerTwoPoints;
                }
                gameForm.picBoxBall.Location = gameForm.moveBall(gameForm.picBoxBall.Location);
                gameForm.Update();
            }
            
        }

        /**
         * Gets the bitmap to display
         */
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }

        /**
         * Gets or sets the current status text to display
         */
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }

        /**
         * Execute start up tasks on load
         */
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loadGameMenu();
            this.playerOne = new Player();
            this.playerTwo = new Player();
            playerOne.trackingID = 0;
            playerTwo.trackingID = 1;
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }
            //startGame();
        }

        /**
         * Execute shurt down tasks on close
         */
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    int count;
                    if (gameForm != null) {
                    
                        options = gameForm.options;
                        switch (this.options.numPlayers)
                        {
                            case 1:
                                this.playerOne.needsAllocation = true;
                                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                                foreach (Body body in this.bodies)
                                {
                                    if (body.IsTracked)
                                    {
                                        if (this.playerOne.trackingID == 0 && body.TrackingId != 0)
                                        {
                                            this.playerOne.trackingID = body.TrackingId;
                                            break;
                                        }
                                    }
                                }

                                foreach (Body body in this.bodies)
                                {
                                    Pen drawPen;

                                    if (body.IsTracked && body.TrackingId == this.playerOne.trackingID)
                                    {
                                        drawPen = this.bodyColors[0];
                                        this.DrawClippedEdges(body, dc);

                                        IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                                        Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                        foreach (JointType jointType in joints.Keys)
                                        {
                                            CameraSpacePoint position = joints[jointType].Position;
                                            if (position.Z < 0)
                                            {
                                                position.Z = InferredZPositionClamp;
                                            }

                                            DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                            jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                        }

                                        this.DrawBody(joints, jointPoints, dc, drawPen);

                                        this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                        this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);

                                        HandState givenHandState;
                                        playerOne.useLeftHand = true;

                                        if (playerOne.useLeftHand)
                                        {
                                            givenHandState = body.HandLeftState;
                                        }
                                        else
                                        {
                                            givenHandState = body.HandRightState;
                                        }

                                        switch (givenHandState)
                                        {
                                            case HandState.Open:
                                                this.playerOne.isHandOpen = true;
                                                this.playerOne.isHandClosed = false;
                                                break;
                                            case HandState.Closed:
                                                if (this.playerOne.isHandClosed)
                                                {
                                                    this.playerOne.isHandClosed = true;
                                                    this.playerOne.isHandOpen = false;
                                                    break;
                                                }

                                                this.playerOne.isHandClosed = false;
                                                break;
                                            default:
                                                this.playerOne.isHandClosed = false;
                                                break;
                                        }
                                        double shoulderLengthScale = Math.Sqrt(Math.Pow(jointPoints[JointType.ShoulderLeft].X - jointPoints[JointType.ShoulderRight].X, 2)
                                            + Math.Pow(jointPoints[JointType.ShoulderLeft].Y - jointPoints[JointType.ShoulderRight].Y, 2));

                                        Rect handRegion;

                                        if (playerOne.useLeftHand)
                                        {
                                            handRegion = new Rect(jointPoints[JointType.ShoulderLeft].X - (1.3 * shoulderLengthScale),
                                                                    jointPoints[JointType.ShoulderLeft].Y - (1.0 * shoulderLengthScale),
                                                                    2 * shoulderLengthScale, 2 * shoulderLengthScale);
                                            this.playerOne.handLocation.X = (jointPoints[JointType.HandLeft].X - handRegion.X) / (handRegion.Width) * 600;
                                            this.playerOne.handLocation.Y = (jointPoints[JointType.HandLeft].Y - handRegion.Y) / (handRegion.Height) * 600;
                                        }
                                        else
                                        {
                                            //using right hand
                                            handRegion = new Rect(jointPoints[JointType.ShoulderRight].X - (.7 * shoulderLengthScale),
                                            jointPoints[JointType.ShoulderRight].Y - (1.0 * shoulderLengthScale),
                                            2 * shoulderLengthScale,
                                            2 * shoulderLengthScale);
                                            this.playerOne.handLocation.Y = (jointPoints[JointType.HandRight].Y - handRegion.Y) / (handRegion.Height) * 600;
                                            this.playerOne.handLocation.X = (jointPoints[JointType.HandRight].X - handRegion.X) / (handRegion.Width) * 600;
                                        }

                                        dc.DrawRectangle(null, drawPen, handRegion);

                                        Canvas.SetLeft(this.Hand, this.playerOne.handLocation.X - 25);
                                        Canvas.SetTop(this.Hand, this.playerOne.handLocation.Y - 25);
                                    }
                                }

                                count = 0;
                                foreach (Body body in bodies)
                                {
                                    if (body.IsTracked)
                                        count++;
                                }
                                if (count >= 1)
                                    playGame();

                                //JointType.Hand
                                // prevent drawing outside of our render area
                                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                                break;
                            case 2:
                                this.playerOne.needsAllocation = true;
                                this.playerTwo.needsAllocation = true;
                                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                                foreach (Body body in this.bodies)
                                {
                                    if (body.IsTracked)
                                    {
                                        if (this.playerOne.trackingID == 0 && body.TrackingId != 0)
                                        {
                                            this.playerOne.trackingID = body.TrackingId;
                                            break;
                                        }
                                        else if (this.playerTwo.trackingID == 1 && body.TrackingId != 0 && this.playerOne.trackingID != body.TrackingId)
                                        {
                                            this.playerTwo.trackingID = body.TrackingId;
                                            break;
                                        }
                                    }
                                }

                                foreach (Body body in this.bodies)
                                {
                                    Pen drawPen;

                                    if (body.IsTracked && body.TrackingId == this.playerOne.trackingID)
                                    {
                                        drawPen = this.bodyColors[0];
                                        this.DrawClippedEdges(body, dc);

                                        IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                                        Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                        foreach (JointType jointType in joints.Keys)
                                        {
                                            CameraSpacePoint position = joints[jointType].Position;
                                            if (position.Z < 0)
                                            {
                                                position.Z = InferredZPositionClamp;
                                            }

                                            DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                            jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                                        }

                                        this.DrawBody(joints, jointPoints, dc, drawPen);

                                        this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                        this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);

                                        HandState givenHandState;
                                        playerOne.useLeftHand = true;
                                        if (playerOne.useLeftHand)
                                        {
                                            givenHandState = body.HandLeftState;
                                        }
                                        else
                                        {
                                            givenHandState = body.HandRightState;
                                        }

                                        switch (givenHandState)
                                        {
                                            case HandState.Open:
                                                this.playerOne.isHandOpen = true;
                                                this.playerOne.isHandClosed = false;
                                                break;
                                            case HandState.Closed:
                                                if (this.playerOne.isHandClosed)
                                                {
                                                    this.playerOne.isHandClosed = true;
                                                    this.playerOne.isHandOpen = false;
                                                    break;
                                                }

                                                this.playerOne.isHandClosed = false;
                                                break;
                                            default:
                                                this.playerOne.isHandClosed = false;
                                                break;
                                        }
                                        double shoulderLengthScale = Math.Sqrt(Math.Pow(jointPoints[JointType.ShoulderLeft].X - jointPoints[JointType.ShoulderRight].X, 2)
                                            + Math.Pow(jointPoints[JointType.ShoulderLeft].Y - jointPoints[JointType.ShoulderRight].Y, 2));

                                        Rect handRegion;

                                        if (playerOne.useLeftHand)
                                        {
                                            handRegion = new Rect(jointPoints[JointType.ShoulderLeft].X - (1.3 * shoulderLengthScale),
                                                                    jointPoints[JointType.ShoulderLeft].Y - (1.0 * shoulderLengthScale),
                                                                    2 * shoulderLengthScale, 2 * shoulderLengthScale);
                                            this.playerOne.handLocation.X = (jointPoints[JointType.HandLeft].X - handRegion.X) / (handRegion.Width) * 600;
                                            this.playerOne.handLocation.Y = (jointPoints[JointType.HandLeft].Y - handRegion.Y) / (handRegion.Height) * 600;
                                        }
                                        else
                                        {
                                            //using right hand
                                            handRegion = new Rect(jointPoints[JointType.ShoulderRight].X - (.7 * shoulderLengthScale),
                                                                    jointPoints[JointType.ShoulderRight].Y - (1.0 * shoulderLengthScale),
                                                                    2 * shoulderLengthScale,
                                                                    2 * shoulderLengthScale);
                                            this.playerOne.handLocation.Y = (jointPoints[JointType.HandRight].Y - handRegion.Y) / (handRegion.Height) * 600;
                                            this.playerOne.handLocation.X = (jointPoints[JointType.HandRight].X - handRegion.X) / (handRegion.Width) * 600;
                                        }

                                        dc.DrawRectangle(null, drawPen, handRegion);

                                        Canvas.SetLeft(this.Hand, this.playerOne.handLocation.X - 25);
                                        Canvas.SetTop(this.Hand, this.playerOne.handLocation.Y - 25);
                                    }
                                    else if (body.IsTracked && body.TrackingId == this.playerTwo.trackingID)
                                    {
                                        drawPen = this.bodyColors[1];
                                        this.DrawClippedEdges(body, dc);

                                        IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                                        Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                        foreach (JointType jointType in joints.Keys)
                                        {
                                            CameraSpacePoint position = joints[jointType].Position;
                                            if (position.Z < 0)
                                            {
                                                position.Z = InferredZPositionClamp;
                                            }

                                            DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                            jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);

                                        }

                                        this.DrawBody(joints, jointPoints, dc, drawPen);

                                        this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                        this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);

                                        HandState givenHandState;
                                        playerTwo.useLeftHand = true;
                                        if (playerTwo.useLeftHand)
                                        {
                                            givenHandState = body.HandLeftState;
                                        }
                                        else
                                        {
                                            givenHandState = body.HandRightState;
                                        }

                                        switch (givenHandState)
                                        {
                                            case HandState.Open:
                                                this.playerTwo.isHandOpen = true;
                                                this.playerTwo.isHandClosed = false;
                                                break;
                                            case HandState.Closed:
                                                if (this.playerTwo.isHandOpen)
                                                {
                                                    this.playerTwo.isHandClosed = true;
                                                    this.playerTwo.isHandOpen = false;
                                                    break;
                                                }

                                                this.playerTwo.isHandClosed = false;
                                                break;
                                            default:
                                                this.playerTwo.isHandClosed = false;
                                                break;
                                        }

                                        double shoulderLengthScale = Math.Sqrt(Math.Pow(jointPoints[JointType.ShoulderLeft].X - jointPoints[JointType.ShoulderRight].X, 2)
                                           + Math.Pow(jointPoints[JointType.ShoulderLeft].Y - jointPoints[JointType.ShoulderRight].Y, 2));

                                        Rect handRegion;

                                        if (playerTwo.useLeftHand)
                                        {
                                            handRegion = new Rect(jointPoints[JointType.ShoulderLeft].X - (1.3 * shoulderLengthScale),
                                                                    jointPoints[JointType.ShoulderLeft].Y - (1.0 * shoulderLengthScale),
                                                                    2 * shoulderLengthScale,
                                                                    2 * shoulderLengthScale);
                                            this.playerTwo.handLocation.X = (jointPoints[JointType.HandLeft].X - handRegion.X) / (handRegion.Width) * 600;
                                            this.playerTwo.handLocation.Y = (jointPoints[JointType.HandLeft].Y - handRegion.Y) / (handRegion.Height) * 600;
                                        }
                                        else
                                        {
                                            handRegion = new Rect(jointPoints[JointType.ShoulderRight].X - (.7 * shoulderLengthScale),
                                                                    jointPoints[JointType.ShoulderRight].Y - (1.0 * shoulderLengthScale),
                                                                    2 * shoulderLengthScale,
                                                                    2 * shoulderLengthScale);
                                            this.playerTwo.handLocation.X = (jointPoints[JointType.HandRight].X - handRegion.X) / (handRegion.Width) * 600;
                                            this.playerTwo.handLocation.Y = (jointPoints[JointType.HandRight].Y - handRegion.Y) / (handRegion.Height) * 600;
                                        }

                                        dc.DrawRectangle(null, drawPen, handRegion);

                                        Canvas.SetLeft(this.Hand2, this.playerTwo.handLocation.X - 25); // -25 to center hand on location
                                        Canvas.SetTop(this.Hand2, this.playerTwo.handLocation.Y - 25); // -25 to center hand on location

                                    }
                                }

                                count = 0;
                                foreach (Body body in bodies)
                                {
                                    if (body.IsTracked)
                                        count++;
                                }
                                if (count == 2)
                                    playGame();

                                //JointType.Hand
                                // prevent drawing outside of our render area
                                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                                break;
                            default:
                                this.playerOne.needsAllocation = true;
                                break;
                        }
                    }


                }
            }
        }

        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }
        }

        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(this.handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(this.handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(this.handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
        }

        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, this.displayHeight - ClipBoundsThickness, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(this.displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.SensorNotAvailableStatusText;
        }

    }
}