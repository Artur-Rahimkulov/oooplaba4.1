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
        static int n = 100;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                Graphics g = Graphics.FromImage(bmp);
                Form1 pb = (Form1)sender;
                str.DeleteSelected();
                g.Clear(Color.White);
                str.DrawAll(pb, bmp, g);
                this.Refresh();
            }
        }
    }
    public class CCircle
    {

        public int x, y, num;
        public bool isSelected = false;
        public int rad = 20;
        public CCircle(int x_, int y_)
        {
            x = x_;
            y = y_;
            isSelected = true;
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
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
            Zalivka(sender, bmp, g);
        }

        public void Zalivka(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
        }
        public void DrawCircleGreen(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Green, 3);
            Font font = new Font("Arial", 25, FontStyle.Regular);
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
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
        public int GetCoorY()
        {
            return (y);
        }
        public void SetSelectedTrue()
        {
            isSelected = true;
        }
        public void SetSelectedFalse()
        {
            isSelected = false;
        }
    }

    public class MyStorage
    {
        static public int size = 0;
        static public int dlc = 0;
        static public int x1, x2, y1, y2;
        static public int dl1 = -1;
        static public int dl2 = -1;
        static public CCircle[] objects;

        public MyStorage()
        {
            objects = new CCircle[100];
        }

        ~MyStorage()
        {

        }

        public void Drawing(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            
                if (objects[index].isSelected) { 
                    objects[index].DrawCircleGreen(size, sender, bmp, g);
                }
                else  objects[index].DrawCircleBlack(size, sender, bmp, g);

        }

        public void SetAllSelectedFalse()
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    objects[i].SetSelectedFalse();
            }
        }
        public void SetSelected(int i)
        {
            objects[i].SetSelectedTrue();
        }
        public int GetSize()
        {
            return (size);
        }
        public void DeleteSelected()
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i]!=null)
                if (objects[i].isSelected)
                    objects[i]=null;
            }
        }
        public void Add(CCircle obj, Form1 sender, Bitmap bmp, Graphics g)
        {
            SetAllSelectedFalse();
            objects[size] = obj;
            size++;
            DrawAll(sender, bmp, g);            

        }
        public void DrawAll(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    Drawing(i, sender, bmp, g);
            }

        }
        public bool isHit(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                    {
                        if (!(Control.ModifierKeys == Keys.Control))
                        {
                            SetAllSelectedFalse();
                        }
                        objects[i].SetSelectedTrue();
                        DrawAll(sender, bmp, g);
                        return true;
                    }
            }
            return false;
        }


    }

}
