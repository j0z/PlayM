using System;
using System.Collections.Generic;
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
using System.Threading;

namespace PlayM_Controller_Legacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client client = new Client();
        private Thread clientThread;

        TouchPoint previousTP = null;

        public int mouseScale = 7; //"Magic number" it basically sets the sensitivity of the trackpad. This will need to be adjusted if you change its size. (higher = more sensitive)


        public MainWindow()
        {
            InitializeComponent();

            clientThread = new Thread(new ThreadStart(client.startClient));
            clientThread.Start();
        }

        //Everything after this just calls client.send with the correct arguments. 

        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Enter");
        }

        private void Btn_Space_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Space");
        }

        private void Btn_Escape_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Escape");
        }

        private void Btn_W_Start(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "W", 1);
        }

        private void Btn_W_End(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "W", 2);
        }

        private void Btn_W_Start(object sender, TouchEventArgs e)
        {
            client.send("KB", "W", 1);
        }

        private void Btn_W_End(object sender, TouchEventArgs e)
        {
            client.send("KB", "W", 2);
        }

        private void Btn_A_Start(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "A", 1);
        }

        private void Btn_A_End(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "A", 2);
        }

        private void Btn_A_Start(object sender, TouchEventArgs e)
        {
            client.send("KB", "A", 1);
        }

        private void Btn_A_End(object sender, TouchEventArgs e)
        {
            client.send("KB", "A", 2);
        }

        private void Btn_D_Start(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "D", 1);
        }

        private void Btn_D_End(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "D", 2);
        }

        private void Btn_D_Start(object sender, TouchEventArgs e)
        {
            client.send("KB", "D", 1);
        }

        private void Btn_D_End(object sender, TouchEventArgs e)
        {
            client.send("KB", "D", 2);
        }

        private void Btn_S_Start(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "S", 1);
        }

        private void Btn_S_End(object sender, MouseButtonEventArgs e)
        {
            client.send("KB", "S", 2);
        }

        private void Btn_S_Start(object sender, TouchEventArgs e)
        {
            client.send("KB", "S", 1);
        }

        private void Btn_S_End(object sender, TouchEventArgs e)
        {
            client.send("KB", "S", 2);
        }

        private void Btn_Up_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Up");
        }

        private void Btn_Left_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Left");
        }

        private void Btn_Right_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Right");
        }

        private void Btn_Down_Click(object sender, RoutedEventArgs e)
        {
            client.send("KB", "Down");
        }

        private void Thumb_Down(object sender, TouchEventArgs e)
        {
            TouchPoint pt = e.GetTouchPoint(Touchpad);
            
            Lbl_Coords.Content = "x: " + Convert.ToInt32(pt.Position.X).ToString() + " y: " + Convert.ToInt32(pt.Position.Y).ToString();

            if (previousTP != null) 
            {
                int xpos = (Convert.ToInt32(pt.Position.X - previousTP.Position.X)*mouseScale);
                int ypos = (Convert.ToInt32(pt.Position.Y - previousTP.Position.Y)*mouseScale);

                client.send("M", xpos.ToString() + ',' + ypos.ToString());
            }
            previousTP = pt;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {

            //Point pt = e.GetPosition(Touchpad);
            //Point previousTP = new Point(0, 0);
            //Lbl_Coords.Content = "x: " + Convert.ToInt32(pt.X).ToString() + " y: " + Convert.ToInt32(pt.Y).ToString();

            //if (previousTP != null &&
            //    (Math.Abs(pt.X - previousTP.X) > 10) ||
            //    (Math.Abs(pt.Y - previousTP.Y) > 10))
            //{
            //    client.send("M", Convert.ToInt32(pt.X).ToString() + ',' + Convert.ToInt32(pt.Y).ToString());
            //}
            //previousTP = pt;
        }

        private void Thumb_Up(object sender, TouchEventArgs e)
        {
            previousTP = null; //won't jump around when you put your thumb in a different position next time you touch the pad. 
        }
    }
}
