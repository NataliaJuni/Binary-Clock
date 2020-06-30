using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;


namespace Binary_Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random myRandom = new Random();
        public int n;
        TextBlock[] SekA;
        TextBlock[] SekB;
        TextBlock[] MinA;
        TextBlock[] MinB;
        TextBlock[] StdA;
        TextBlock[] StdB;
        DispatcherTimer myTimer = new DispatcherTimer();
        bool mono;
        public MainWindow()
        {
            InitializeComponent();
            SekA = new TextBlock[] { SekA1, SekA2, SekA3, SekA4 };
            SekB = new TextBlock[] { SekB1, SekB2, SekB3 };
            MinA = new TextBlock[] { MinA1, MinA2, MinA3, MinA4 };
            MinB = new TextBlock[] { MinB1, MinB2, MinB3 };
            StdA = new TextBlock[] { StdA1, StdA2, StdA3, StdA4 };
            StdB = new TextBlock[] { StdB1, StdB2 };
        }
        private void btnShowTime_Click(object sender, RoutedEventArgs e)
        {
            if (myTimer.IsEnabled)
            {
                myTimer.Stop();
            }
            mono = false;
            ShowTime(mono);
            lblDecimalTime.Content = "      " + DateTime.Now.ToLongTimeString();
        }

        public void ShowTime(bool mono)
        {
            DateTime time = DateTime.Now;
            int timeSekA = time.Second % 10;
            int timeSekB = time.Second / 10;
            int timeMinA = time.Minute % 10;
            int timeMinB = time.Minute / 10;
            int timeStdA = time.Hour % 10;
            int timeStdB = time.Hour / 10;
            TimeZeigen(SekA, timeSekA, mono);
            TimeZeigen(SekB, timeSekB, mono);
            TimeZeigen(MinA, timeMinA, mono);
            TimeZeigen(MinB, timeMinB, mono);
            TimeZeigen(StdA, timeStdA, mono);
            TimeZeigen(StdB, timeStdB, mono);
        }
        public void TimeZeigen(TextBlock[] myT, int t, bool mono)
        {
            foreach (TextBlock i in myT)
            {
                if (t % 2 != 0)
                {
                    FarbenAendern(i, mono);
                }
                else
                {
                    i.Background = Brushes.CornflowerBlue;
                    i.Opacity = 0.3;
                }
                t /= 2;
            }
        }

        public void FarbenAendern(TextBlock myT, bool mono)
        {
            if (!mono)
            {
                n = myRandom.Next(5);
                switch (n)
                {
                    case 1:
                        {
                            myT.Background = Brushes.Aquamarine;
                            myT.Opacity = 1.0;
                            break;
                        }
                    case 2:
                        {
                            myT.Background = Brushes.DarkSeaGreen;
                            myT.Opacity = 1.0;
                            break;
                        }
                    case 3:
                        {
                            myT.Background = Brushes.DarkCyan;
                            myT.Opacity = 1.0;
                            break;
                        }
                    case 4:
                        {
                            myT.Background = Brushes.YellowGreen;
                            myT.Opacity = 1.0;
                            break;
                        }
                    default:
                        {
                            myT.Background = Brushes.LightCoral;
                            myT.Opacity = 1.0;
                            break;
                        }
                }
            }
            else
            {
                myT.Background = Brushes.CornflowerBlue;
                myT.Opacity = 1.0;
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            mono = true;
            if (myTimer.IsEnabled)
            {
                myTimer.Stop();
                btnRun.Content = "Binäre Uhr starten";
            }
            else
            {
                TimeRunning();
                btnRun.Content = "Binäre Uhr läuft";
            }
        }
        public void TimeRunning()
        {
            myTimer.Interval = TimeSpan.FromMilliseconds(100);
            myTimer.Tick += myTimerTick;
            myTimer.Start();
        }
        void myTimerTick(object sender, EventArgs e)
        {
            lblDecimalTime.Content = "      " + DateTime.Now.ToLongTimeString();
            ShowTime(mono);
        }
    }
}


