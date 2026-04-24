using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace BouncingBalls
{
    public class Ball
    {
        public double x { get; set; }
        public double y { get; set; }
        public double dx { get; set; }
        public double dy { get; set; }
        public Ellipse Ellipse { get; set; }

        public Ball(double canvasWidth, double canvasHeight, Random r)
        {
            this.Ellipse = new Ellipse
            {
                Width = 50,
                Height = 50,
                Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)))
            };

            double maxY = canvasHeight - Ellipse.Height;
            this.x = r.NextDouble() * ((canvasWidth - 50) >0 ? (canvasWidth - 50) : 0);
            this.y = r.NextDouble() * (maxY > 0 ? maxY : 0);
            this.dx = r.Next(-5, 5);
            this.dy = r.Next(-5, 5);
            if (dx == 0) dx = 2;
            if (dy == 0) dy = 2;
            Canvas.SetLeft(Ellipse, x);
            Canvas.SetTop(Ellipse, y);

        }
        public Ball(double startX, double startY, double speedX, double speedY, Ellipse ellipse)
        {
            x = startX;
            y = startY;
            dx = speedX;
            dy = speedY;
            Ellipse = ellipse;
        }

        public void UpdatePosition(double canvasWidth, double canvasHeight)
        {
            x = x + dx;
            y = y + dy;

            if (x <= 0 || x + Ellipse.ActualWidth >= canvasWidth) dx = -dx;
            if (y <= 0 || y + Ellipse.ActualHeight >= canvasHeight) dy = -dy;
        }

    }
}
