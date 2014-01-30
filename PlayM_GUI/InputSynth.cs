using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InputManager;
using System.Windows.Forms;
using System.Diagnostics;

namespace PlayM_GUI
{
    class InputSynth
    {
        //See explaination at mouseMove()
        private int[] px = new int[10];
        private int[] py = new int[10];
        private int currentIndex = 0;
        private int avgX;
        private int avgY;

        public InputSynth()
        {
        }

        // I don't like that mouse buttons are handled here. This will be fixed later. 
        public void keyPress(string rawKey, int hold = 0)
        {
            if (rawKey.Contains("M_"))
            {
                switch (rawKey)
                {
                    case ("M_1"):
                        Mouse.PressButton(Mouse.MouseKeys.Left);
                        break;
                    case ("M_2"):
                        Mouse.PressButton(Mouse.MouseKeys.Right);
                        break;
                    case ("M_3"):
                        Mouse.PressButton(Mouse.MouseKeys.Middle);
                        break;
                    default:
                        Console.WriteLine("Invalid Mouse Button Press");
                        break;

                }
            }
            else
            {
                KeysConverter kc = new KeysConverter();
                Keys key = (Keys)kc.ConvertFromString(rawKey);

                if (hold == 0)
                    Keyboard.KeyPress(key, 5);
                else if (hold == 1)
                    Keyboard.KeyDown(key);
                else
                    Keyboard.KeyUp(key);
            }
        }

        //This mess was an attempt to fix the jittery camera issue in FPS games. It was unsuccessful. However, it is a fairly nice mouse acceleration
        //implimentation. I don't know if I'll keep it, I'll probably at least let it be an option at some point. 
        public void mouseMove(string rawCoords)
        {
            Debug.WriteLine("mousemove: " + rawCoords);
            string[] coords = rawCoords.Split(',');
            int x = Convert.ToInt32(coords[0]);
            int y = Convert.ToInt32(coords[1]);

            Mouse.MoveRelative(avgX, avgY);
            //Mouse.Move(x * mouseScale, y * mouseScale);
            //Mouse.Move(x+630, y+393);

            if(currentIndex < 9)
            {
                px[currentIndex] = x;
                py[currentIndex] = y;
                currentIndex++;
            }
            else if (currentIndex == 9)
            {
                px[currentIndex] = x;
                py[currentIndex] = y;
                currentIndex = 0;
            }

            average();
        }

        private void average()
        {
            foreach(int i in px)
            {
                avgX += i;
            }

            avgX = avgX / px.Length;
            foreach(int i in py)
            {
                avgY += i;
            }

            avgY = avgY / py.Length;
        }
    }
}
