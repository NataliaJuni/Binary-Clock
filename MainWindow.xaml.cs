﻿using System;
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
        Brush[] colors = new Brush[] {Brushes.Red, Brushes.Blue, Brushes.Brown, Brushes.Gray, Brushes.MediumBlue, Brushes.Aquamarine,
        Brushes.BlueViolet, Brushes.BurlyWood, Brushes.CadetBlue, Brushes.Chocolate, Brushes.Coral, Brushes.CornflowerBlue,
        Brushes.Crimson, Brushes.Cyan, Brushes.DarkGoldenrod, Brushes.DarkMagenta, Brushes.ForestGreen, Brushes.Fuchsia, Brushes.BlueViolet, Brushes.DarkBlue,
        Brushes.DarkCyan, Brushes.DarkOliveGreen, Brushes.DarkOrchid, Brushes.DarkRed, Brushes.DarkSalmon, Brushes.DarkSeaGreen, Brushes.DarkSlateBlue,
        Brushes.DarkSlateGray, Brushes.MediumOrchid, Brushes.MediumPurple, Brushes.MediumSpringGreen, Brushes.MediumTurquoise,Brushes.MediumVioletRed,
         Brushes.DarkTurquoise, Brushes.DeepSkyBlue, Brushes.DodgerBlue, Brushes.IndianRed, Brushes.Indigo, Brushes.Lavender, Brushes.MediumBlue};
        DispatcherTimer myTimer = new DispatcherTimer();
        DispatcherTimer myTimerStop = new DispatcherTimer();
        DispatcherTimer myTimerCD = new DispatcherTimer();
        bool mono;
        DateTime time, timeStart;
        TimeSpan timeDiff;
        public MainWindow()
        {
            InitializeComponent();
            SekA = new TextBlock[] { SekA1, SekA2, SekA3, SekA4 };
            SekB = new TextBlock[] { SekB1, SekB2, SekB3 };
            MinA = new TextBlock[] { MinA1, MinA2, MinA3, MinA4 };
            MinB = new TextBlock[] { MinB1, MinB2, MinB3 };
            StdA = new TextBlock[] { StdA1, StdA2, StdA3, StdA4 };
            StdB = new TextBlock[] { StdB1, StdB2 };
            if (timeDiff == TimeSpan.Zero)
            {
                myTimerCD.Stop();
            }
        }
        private void btnShowTime_Click(object sender, RoutedEventArgs e)
        {
            if (myTimer.IsEnabled) myTimer.Stop();
            if (myTimerCD.IsEnabled) myTimerCD.Stop();
            if (myTimerStop.IsEnabled) myTimerStop.Stop();
            time = DateTime.Now;
            mono = false;
            ShowTime(mono);
            lblDecimalTime.Content = "      " + DateTime.Now.ToLongTimeString();
        }
        #region Show Time
        public void ShowTime(bool mono)
        {
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
                n = myRandom.Next(colors.Length);
                myT.Background = colors[n];
                myT.Opacity = 1.0;
            }
            else
            {
                myT.Background = Brushes.CornflowerBlue;
                myT.Opacity = 1.0;
            }
        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (myTimerStop.IsEnabled) myTimerStop.Stop();
            if (myTimerCD.IsEnabled) myTimerCD.Stop();
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
            myTimer.Tick += myTimerNow;
            myTimer.Start();
        }
        void myTimerNow(object sender, EventArgs e)
        {
            lblDecimalTime.Content = "    " + DateTime.Now.ToLongTimeString();
            time = DateTime.Now;
            ShowTime(mono);
        }
        #endregion Show Time
        #region Stoppuhr
        private void btnStoppUhrStart_Click(object sender, RoutedEventArgs e)
        {
            if (myTimerCD.IsEnabled) myTimerCD.Stop();
            if (myTimer.IsEnabled) myTimer.Stop();
            timeStart = DateTime.Now;
            myTimerStop.Interval = TimeSpan.FromMilliseconds(100);
            myTimerStop.Tick += myTimerStop_Tick;
            myTimerStop.Start();
        }
        public void myTimerStop_Tick(object sender, EventArgs e)
        {
            timeDiff = DateTime.Now - timeStart;
            time = DateTime.MinValue.AddTicks(timeDiff.Ticks);
            ShowTime(true);
            lblDecimalTime.Content = "    " + time.ToLongTimeString();
        }
        private void btnStoppuhrStop_Click(object sender, RoutedEventArgs e)
        {
            if (myTimerStop.IsEnabled)
            {
                myTimerStop.Stop();
            }
        }
        #endregion Stoppuhr
        #region Countdown
        private void btnCDStart1_Click(object sender, RoutedEventArgs e)
        {
            // Vorherige Timer stoppen, wenn nötig
            if (myTimer.IsEnabled) myTimer.Stop();
            if (myTimerStop.IsEnabled) myTimerStop.Stop();
            if (myTimerCD.IsEnabled)
            {
                myTimerCD.Stop();
                myTimerCD.Tick -= myTimerCD_Tick;
                txtCDMin.Text = " ";
            }
            // Zeiteingabe in Minuten ablesen
            int.TryParse(txtCDMin.Text, out int countDown);
            // Timer für countdown definieren
            time = DateTime.MinValue.AddMinutes(countDown);
            myTimerCD.Interval = TimeSpan.FromSeconds(1);
            myTimerCD.Tick += myTimerCD_Tick;
            //Timer für countdown starten
            if (time >DateTime.MinValue) myTimerCD.Start();
        }

        private void btnCDStop_Click(object sender, RoutedEventArgs e)
        {
            myTimerCD.Stop();
            myTimerCD.Tick -= myTimerCD_Tick;
            txtCDMin.Text = " ";
        }
        public void myTimerCD_Tick(object sender, EventArgs e)
        {
            if (time >= DateTime.MinValue.AddSeconds(1))
            {

                time = time.AddSeconds(-1);
            }
            ShowTime(true);
            lblDecimalTime.Content = "    " + time.ToLongTimeString();
            txtCDMin.Text = time.ToString("mm:ss");
        }
        #endregion Countdown


    }
}


