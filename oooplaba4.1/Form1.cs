using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oooplaba4._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int n = 25;
        Bitmap bmp = new Bitmap(1800, 800);
        MyStorage str = new MyStorage();

        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = (Form1)sender;



            if (str.isHit(e.X, e.Y, pb, bmp, g))
                this.Refresh();
            else
            {

                str.Add(new CCircle(e.X, e.Y), pb, bmp, g);

                this.Refresh();
            }
        }
    }
    public class CCircle
    {

        public int x, y, num;
        public bool isSelected = false;
        public int rad = 20;
        public int color = 0;
        public CCircle(int x_, int y_)
        {
            x = x_;
            y = y_;
            color = 0;
        }
        ~CCircle()
        {

        }

        public void DrawCircleBlack(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            num = size + 1;
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Black, 8);
            Font font = new Font("Arial", 25, FontStyle.Regular);

            isSelected = true;
            g.DrawEllipse(pen, rect);

            g.DrawString((num).ToString(), font, Brushes.Black, x - 20, y - 20);
            sender.BackgroundImage = bmp;
            Zalivka(sender, bmp, g);
        }

        public void Zalivka(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((num).ToString(), font, Brushes.Black, x - 20, y - 20);
            color = 0;

        }

        public void ZalivkaGreen(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Green);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((num).ToString(), font, Brushes.White, x - 20, y - 20);
            color = 1;

        }
        public void ZalivkaRed(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((num).ToString(), font, Brushes.White, x - 20, y - 20);
            color = 2;

        }

        public void DrawCircleGreen(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Green, 3);
            Font font = new Font("Arial", 25, FontStyle.Regular);
            isSelected = true;
            g.DrawEllipse(pen, rect);
            g.DrawString((size + 1).ToString(), font, Brushes.Green, x - 20, y - 20);
            sender.BackgroundImage = bmp;
        }
        public void DrawCircleRed(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Red, 3);
            Font font = new Font("Arial", 25, FontStyle.Regular);
            isSelected = true;
            g.DrawEllipse(pen, rect);
            g.DrawString((size + 1).ToString(), font, Brushes.Green, x - 20, y - 20);
            sender.BackgroundImage = bmp;
        }

        public void DrawLine(int x1, int y1, int x2, int y2, Form1 sender, Bitmap bmp, Graphics g)
        {
            Pen p = new Pen(Color.DimGray, 6);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            g.DrawLine(p, p1, p2);

        }

        public void DrawLineGreen(int x1, int y1, int x2, int y2, Form1 sender, Bitmap bmp, Graphics g)
        {
            Pen p = new Pen(Color.Green, 6);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            g.DrawLine(p, p1, p2);

        }
        public void DrawLineRed(int x1, int y1, int x2, int y2, Form1 sender, Bitmap bmp, Graphics g)
        {
            Pen p = new Pen(Color.Red, 6);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            g.DrawLine(p, p1, p2);

        }

        public bool isHit(int x_, int y_)
        {
            if (((x - rad) < x_) && (x + rad > x_) && ((y - rad - rad) < y_) && (y + rad > y_))
            {

                return true;
            }
            else
                return false;
        }

        public int GetCoorX()
        {
            return (x);
        }
        public int Getcolor()
        {
            return color;
        }
        public int GetCoorY()
        {
            return (y);
        }
    }

    public class MyStorage
    {
        static int[,] arr2 = new int[25, 25];

        static public int size = 0;
        static public int dlc = 0;
        static public int x1, x2, y1, y2;
        static public int dl1 = -1;
        static public int dl2 = -1;
        static public CCircle[] objects;

        public MyStorage()
        {
            objects = new CCircle[25];
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                    arr2[i, j] = 0;
        }

        ~MyStorage()
        {

        }

        public void Drawing(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (objects[index] != null)
                objects[index].DrawCircleBlack(size, sender, bmp, g);

        }


        public int GetSize()
        {
            return (size);
        }
        public void GetArr(int[,] arr)
        {
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                    arr[i, j] = arr2[i, j];
        }
        public void Add(CCircle obj, Form1 sender, Bitmap bmp, Graphics g)
        {
            objects[size] = obj;

            Drawing(size, sender, bmp, g);
            size++;

        }
        public void DrawAll(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    objects[i].Zalivka(sender, bmp, g);
                for (int j = 0; j < size; j++)
                {
                    if (arr2[i, j] == 1)
                    {
                        dl1 = i;
                        dl2 = j;
                        DrawL(sender, bmp, g);
                    }
                }
            }
            dl1 = -1;
            dl2 = -1;
            dlc = 0;
        }
        public bool isHit(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                    {
                        if (dl1 == -1)
                        {
                            dl1 = i;
                            dlc++;
                        }
                        else
                        {
                            dl2 = i;
                            dlc++;
                        }
                        if (dlc == 2)
                            if (dl1 != dl2)
                            {
                                DrawL(sender, bmp, g);
                                dl1 = -1;
                                dl2 = -1;
                                dlc = 0;
                            }
                            else
                            {
                                dl1 = -1;
                                dl2 = -1;
                                dlc = 0;
                            }
                        return true;
                    }
            }
            return false;
        }


    }

}
