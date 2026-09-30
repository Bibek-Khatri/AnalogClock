using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class Clock : Form
    {
        private Bitmap bitmap;
        private Graphics cg;

        private int cx, cy;
        private int width = 400;
        private int height = 400;

        private Font myFont = new Font("Arial", 12);
        private Brush myBrush = new SolidBrush(Color.Black);

        public Clock()
        {
            InitializeComponent();
        }

        private void AnalogClock_Load(object sender, EventArgs e)
        {
            // Center of the clock
            cx = width / 2;
            cy = height / 2;

            // Create bitmap and graphics
            bitmap = new Bitmap(width, height);
            cg = Graphics.FromImage(bitmap);

            // Draw clock design
            Draw();
            timer1.Start();
        }

        private void Draw()
        {
            // Clear background
            cg.Clear(Color.FromArgb(255, 200, 120));

            // Draw square outline
            using (Pen squarePen = new Pen(Color.Black, 2))
            {
                cg.DrawRectangle(
                    squarePen,
                    0,
                    0,
                    width - 1,
                    height - 1
                );
            }

            // Draw clock circle
            using (Pen circlePen = new Pen(Color.Black, 2))
            {
                cg.DrawEllipse(
                    circlePen,
                    0,
                    0,
                    width - 1,
                    height - 1
                );
            }

            // Draw numbers 1 to 12
            for (int i = 1; i <= 12; i++)
            {
                double angle = Math.PI / 6 * i;

                int x = cx + (int)(160 * Math.Sin(angle));
                int y = cy - (int)(160 * Math.Cos(angle));

                cg.DrawString(
                    i.ToString(),
                    myFont,
                    myBrush,
                    new PointF(x - 10, y - 10)
                );
            }

            //Draw clock hands
            DrawHands();

            // Display bitmap in PictureBox
            pictureBox1.Image = bitmap;
        }

        private void DrawHands()
        {
            // Hour hand
            DateTime now = DateTime.Now;
            double hourAngle = Math.PI / 6 * (now.Hour % 12 + now.Minute / 60.0);

            int hourX = cx + (int)(100 * Math.Sin(hourAngle));
            int hourY = cy - (int)(100 * Math.Cos(hourAngle));

            using (Pen hourPen = new Pen(Color.Black, 6))
            {
                cg.DrawLine(
                    hourPen,
                    cx,
                    cy,
                    hourX,
                    hourY
                );

            }
            // Minute hand
            double minuteAngle = Math.PI / 30 * now.Minute;

            int minuteX = cx + (int)(135 * Math.Sin(minuteAngle));
            int minuteY = cy - (int)(135 * Math.Cos(minuteAngle));

            using (Pen minutePen = new Pen(Color.Black, 4))
            {
                cg.DrawLine(
                    minutePen,
                    cx,
                    cy,
                    minuteX,
                    minuteY
                );
            }

            // Second hand
            double secondAngle = Math.PI / 30 * now.Second;

            int secondX = cx + (int)(155 * Math.Sin(secondAngle));
            int secondY = cy - (int)(155 * Math.Cos(secondAngle));

            using (Pen secondPen = new Pen(Color.Red, 2))
            {
                cg.DrawLine(
                    secondPen,
                    cx,
                    cy,
                    secondX,
                    secondY
                );
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Dispose graphics
            if (cg != null)
            {
                cg.Dispose();
            }

            // Dispose bitmap
            if (bitmap != null)
            {
                bitmap.Dispose();
            }

            // Dispose font and brush
            myFont.Dispose();
            myBrush.Dispose();

            base.OnFormClosed(e);
        }
    }


}