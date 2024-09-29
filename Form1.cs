using System.Drawing;

namespace Robo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
            graphics.FillRectangle(brush, 0, 0, 1000, 1000);
            DrawRobo();
            LabGen();
            textBox2.Text = coordX.ToString();
            textBox3.Text = coordY.ToString();
        }
        private bool isMouse = false;

        int[,] array = new int[13, 13];

        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {

                return points;
            }
        }
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

        }
        int robotX = 100, robotY = 940;
        private ArrayPoints arrayPoints = new ArrayPoints(2);
        Pen pen = new Pen(Color.Black, 3f);
        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;
        Brush brush = new SolidBrush(Color.White);
        int rotation = 0;
        Pen pen2 = new Pen(Color.Pink, 3f);
        int coordY = 12;
        int coordX = 0;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse)
            {
                return;
            }
            arrayPoints.SetPoint(e.X, e.Y);
            if (arrayPoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;

        }
        void DrawRobo()
        {
            switch (rotation)
            {
                case 0:
                    graphics.DrawLine(pen2, robotX - 15, robotY + 8, robotX + 15, robotY + 8);
                    graphics.DrawLine(pen2, robotX - 15, robotY - 8, robotX, robotY + 8);
                    graphics.DrawLine(pen2, robotX + 15, robotY - 8, robotX, robotY + 8);
                    pictureBox1.Image = map;
                    break;
                case 90:
                    graphics.DrawLine(pen2, robotX + 15, robotY - 15, robotX + 15, robotY + 15);
                    graphics.DrawLine(pen2, robotX - 8, robotY - 15, robotX + 15, robotY);
                    graphics.DrawLine(pen2, robotX - 8, robotY + 15, robotX + 15, robotY);
                    pictureBox1.Image = map;
                    break;
                case 180:
                    graphics.DrawLine(pen2, robotX - 15, robotY - 8, robotX + 15, robotY - 8);
                    graphics.DrawLine(pen2, robotX - 15, robotY + 8, robotX, robotY - 8);
                    graphics.DrawLine(pen2, robotX + 15, robotY + 8, robotX, robotY - 8);
                    pictureBox1.Image = map;
                    break;
                case 270:
                    graphics.DrawLine(pen2, robotX - 15, robotY - 15, robotX - 15, robotY + 15);
                    graphics.DrawLine(pen2, robotX + 8, robotY - 15, robotX - 15, robotY);
                    graphics.DrawLine(pen2, robotX + 8, robotY + 15, robotX - 15, robotY);
                    pictureBox1.Image = map;
                    break;
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            graphics.FillRectangle(brush, robotX - 25, robotY - 25, 100, 100);
            DrawRobo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pen pen2 = new Pen(Color.White, 3f);
            graphics.FillRectangle(brush, 10, 10, 100, 100);
            graphics.RotateTransform(30.0F);
            pictureBox1.Image = map;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graphics.FillRectangle(brush, robotX - 25, robotY - 25, 50, 50);
            move();
            DrawRobo();

        }
        void move()
        {

            switch (rotation)
            {
                case 0:

                    robotY -= 70;
                    coordY -= 1;
                    if (coordY < 0) { coordY = 0; robotY += 70; }
                    textBox2.Text = coordX.ToString();
                    textBox3.Text= coordY.ToString();
                    break;
                case 180:
                    robotY += 70;
                    coordY += 1;
                    if (coordY > 12) { coordY = 12; robotY -= 70; }
                    textBox2.Text = coordX.ToString();
                    textBox3.Text = coordY.ToString();
                    break;
                case 90:
                    robotX -= 70;
                    coordX -= 1;
                    if (coordX < 0) { coordX = 0; robotX += 70; }
                    textBox2.Text = coordX.ToString();
                    textBox3.Text = coordY.ToString();
                    break;
                case 270:
                    robotX += 70;
                    coordX += 1;
                    if (coordX > 12) { coordX = 12; robotX -= 70; }
                    textBox2.Text = coordX.ToString();
                    textBox3.Text = coordY.ToString();
                    break;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            rotation -= 90;
            if (rotation < 0) rotation = 270;
            textBox1.Text = rotation.ToString();
            graphics.FillRectangle(brush, robotX - 25, robotY - 25, 50, 50);
            DrawRobo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rotation += 90;
            if (rotation == 360) rotation = 0;
            textBox1.Text = rotation.ToString();
            graphics.FillRectangle(brush, robotX - 25, robotY - 25, 50, 50);
            DrawRobo();
        }

        bool isWall(int x,int y)
        {
            Color xd = map.GetPixel(x, y);
            if ((xd.R == 0) && (xd.G == 0) && (xd.B == 0))
                return true;
            return false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            switch (rotation)
            {
                case 0:
                    for (int i = 0; i < 70; i++)
                    {
                        
                        if (isWall(robotX, robotY - i)) { textBox1.Text = "I see black1"; }
                            
                       

                    }
                    break;
                case 180:
                    for (int i = 0; i < 70; i++)
                    {

                        if (isWall(robotX, robotY + i)) { textBox1.Text = "I see black3"; }

                    }
                    break;
                case 90:
                    for (int i = 0; i < 70; i++)
                    {

                        if (isWall(robotX-i, robotY)) { textBox1.Text = "I see black2"; }

                    }
                    break;
                case 270:
                    for (int i = 0; i < 70; i++)
                    {

                        if (isWall(robotX + i, robotY)) { textBox1.Text = "I see black4"; }

                    }
                    break;
            }
        }

        void LabGen()
        {
            //13(0-12) up  //13(0-12) side
            graphics.DrawRectangle(pen, 20, 20,960,1000);
            for (int i = 0; i < 12; i++)
            {
                graphics.DrawLine(pen, 135+i*70, 20, 135+i*70, 800);
            }
            
            pictureBox1.Image = map;
        }
    }
}
