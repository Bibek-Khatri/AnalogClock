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

            // Display bitmap in PictureBox
            pictureBox1.Image = bitmap;
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