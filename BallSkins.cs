using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectPong
{
    static class BallSkins
    {
        private static List<Image> bs;
        

        private static void setup()
        {
            bs = new List<Image>();
            makeArr();
            Console.WriteLine("Ball skins sucessfully imported. Accessible via Skins.retBalls(int index).");
        }

        private static void makeArr()
        {

            try
            {

                bs.Add(KinectPong.Properties.Resources.ball_00);
                bs.Add(KinectPong.Properties.Resources.ball_01);
                bs.Add(KinectPong.Properties.Resources.ball_02);
                bs.Add(KinectPong.Properties.Resources.ball_03);
                bs.Add(KinectPong.Properties.Resources.ball_04);
                bs.Add(KinectPong.Properties.Resources.ball_05);
                bs.Add(KinectPong.Properties.Resources.ball_06);
                bs.Add(KinectPong.Properties.Resources.ball_07);
                bs.Add(KinectPong.Properties.Resources.ball_08);
                bs.Add(KinectPong.Properties.Resources.ball_09);
                bs.Add(KinectPong.Properties.Resources.ball_10);
                bs.Add(KinectPong.Properties.Resources.ball_11);
                bs.Add(KinectPong.Properties.Resources.ball_12);
                bs.Add(KinectPong.Properties.Resources.ball_13); 
                bs.Add(KinectPong.Properties.Resources.ball_14);
                bs.Add(KinectPong.Properties.Resources.ball_15);
                bs.Add(KinectPong.Properties.Resources.ball_16);
                bs.Add(KinectPong.Properties.Resources.ball_17);
                bs.Add(KinectPong.Properties.Resources.ball_18);
                bs.Add(KinectPong.Properties.Resources.ball_19);
                bs.Add(KinectPong.Properties.Resources.ball_20);
                bs.Add(KinectPong.Properties.Resources.ball_21);

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in the makeArr() method in Skins. " + e.Message);
            }

        }

        public static Image getBalls(int index)
        {
            setup();

            try
            {
                return bs[index];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
            
        }
    }
}



