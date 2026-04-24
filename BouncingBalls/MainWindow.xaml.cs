using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BouncingBalls
{
    public partial class MainWindow : Window
    {
        double x = 10;
        double y = 10;
        double dx = 5;
        double dy = 5;
        private bool moving = false;
        private bool isStartBtnEnabled = true;
        Random r = new Random();
        List<Ball> listaPilek = new List<Ball>();

        public MainWindow()
        {
            InitializeComponent();
            Canvas.SetTop(el, y);
            Canvas.SetLeft(el, x);
            listaPilek.Add(new Ball(x, y, dx, dy, el));
            stopBtn.IsEnabled = false;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            int numberOfBalls = listaPilek.Count;

            if (moving) return;
            startBtn.IsEnabled = false;
            stopBtn.IsEnabled = true;
            addBallBtn.IsEnabled = false;
            deleteBallBtn.IsEnabled = false;
            moving = true;

            Thread t = new Thread(
                new ThreadStart(() => {

                    while (moving)
                    {
                        BallsColideService();

                        foreach (var p in listaPilek.ToList())
                        {
                            p.UpdatePosition(ca.ActualWidth, ca.ActualHeight);

                            Dispatcher.Invoke(() => {
                                Canvas.SetLeft(p.Ellipse, p.x);
                                Canvas.SetTop(p.Ellipse, p.y);
                            });
                        }
                        Thread.Sleep(30);
                    }

                }));
            t.Start();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            moving = false;
            startBtn.IsEnabled = true;
            addBallBtn.IsEnabled = true;
            deleteBallBtn.IsEnabled = true;
            stopBtn.IsEnabled = false;

        }

        private void AddBallBtn_Click(object sender, RoutedEventArgs e)
        {
            Ball newBall;
            bool taken;
            int tries = 0;

            do
            {
                newBall = new Ball(ca.ActualWidth, ca.ActualHeight, r);
                taken = false;

                foreach (var oldBall in listaPilek)
                {
                    if (DoBallsColide(newBall, oldBall))
                    {
                        taken = true;
                        break;
                    }
                }
                tries++;
            } while (taken && tries < 10);

            listaPilek.Add(newBall);
            ca.Children.Add(newBall.Ellipse);
            UpdateButtonsStatus();

        }

        private void DeleteBallBtn_Click(object sender, RoutedEventArgs e)
        {
            int numberOfBalls = listaPilek.Count;
            if (numberOfBalls > 0)
            {
                int index = r.Next(0, numberOfBalls);
                Ball ballToDelete = listaPilek[index];
                ca.Children.Remove(ballToDelete.Ellipse);
                listaPilek.RemoveAt(index);
                UpdateButtonsStatus();
            }
        }

        private void UpdateButtonsStatus()
        {
            bool anyBalls = listaPilek.Count > 0;
            startBtn.IsEnabled = anyBalls;
            deleteBallBtn.IsEnabled = anyBalls;
            stopBtn.IsEnabled = moving;
        }
        public void HitService(Ball b1, Ball b2)
        {
            double tempDX = b1.dx;
            b1.dx = b2.dx;
            b2.dx = tempDX;

            double tempDY = b1.dy;
            b1.dy = b2.dy;
            b2.dy = tempDY;
            b1.UpdatePosition(ca.ActualWidth, ca.ActualHeight);
            b2.UpdatePosition(ca.ActualWidth, ca.ActualHeight);
        }
        public bool DoBallsColide(Ball b1, Ball b2)
        {
            double dx_dist = (b1.x + 25) - (b2.x + 25);
            double dy_dist = (b1.y + 25) - (b2.y + 25);
            double distanceSquared = dx_dist * dx_dist + dy_dist * dy_dist;
            return distanceSquared <= 2500;
        }
        public void BallsColideService()
        {
            for (int i = 0; i < listaPilek.Count; i++)
            {
                for (int j = i + 1; j < listaPilek.Count; j++)
                {
                    if (DoBallsColide(listaPilek[i], listaPilek[j]))
                    {
                        HitService(listaPilek[i], listaPilek[j]);
                    }
                }
            }
        }

    }
}
