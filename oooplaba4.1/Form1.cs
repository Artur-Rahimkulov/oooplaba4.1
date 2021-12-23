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
        Color redraw = Color.Salmon;
        int whattodo = 0;
        int whattopaint = 0;
        int numberofpointpolygon = 0;
        PointF[] polygon;
        int modif=1;
        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = (Form1)sender;

            str.isHit(modif,redraw, whattodo, e.X, e.Y, pb, bmp, g);
                switch (whattopaint)
                {
                    case 0:
                        str.Add(new CCircle(redraw, e.X, e.Y), pb, bmp, g);

                        
                        break;
                    case 1:
                        str.Add(new RRectangle(redraw, e.X, e.Y), pb, bmp, g);

                        
                        break;
                    case 2:
                            
                            
                        
                        
                        break;
                    default:
                        break;

                }
            g.Clear(Color.White);
            str.DrawAll(pb, bmp, g);
            this.Refresh();
            
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


        private void контурToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 2;
            whattopaint = -1;

        }

        private void заливкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 1;
            whattopaint = -1;

        }

        private void всёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 3;
            whattopaint = -1;

        }

        private void выделитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 0;
            whattopaint = -1;

        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Red;
        }

        private void зелёныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Green;
        }

        private void коричневыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Brown;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Blue;
        }

        private void жёлтыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Yellow;
        }

        private void чёрныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.Black;

        }

        private void белыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redraw = Color.White;

        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattopaint = 1;
            whattodo = -1;

        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattopaint = 0;
            whattodo = -1;


        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattopaint = 2;
            numberofpointpolygon = 0;
            polygon = new PointF[3];
            whattodo = -1;


        }

        private void moToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 4;
            whattopaint = -1;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
           

        }

        private void изменитьРазмерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 5;
            whattopaint = -1;

        }

       

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            { 
            modif = Int32.Parse(toolStripTextBox1.Text);
            whattodo = 5;
            whattopaint = -1;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X + ""; 
            label2.Text = e.Y + "";
        }
    }
    public class RRectangle : Figure
    {

        public int height = 25;
        public int width = 25;
        public RRectangle(Color color, int x_, int y_)
        {
            x = x_;
            y = y_;
            isSelected = true;
            outer = color;
        }
        ~RRectangle()
        {

        }

        public override void Draw(Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - width / 2, y - height / 2, width, height);
            Pen pen;
            if (isSelected)
                pen = new Pen(Color.LightSteelBlue, 8);
            else
                pen = new Pen(outer, 8);
            g.DrawRectangle(pen, rect);
            sender.BackgroundImage = bmp;
            Zalivka(sender, bmp, g);

        }

        public override void Zalivka(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(inner);
            g.FillRectangle(brush, x - width / 2, y - height / 2, width, height);
        }

        public override bool isHit(int x_, int y_)
        {
            if (((x - width / 2) < x_) && (x + width / 2 > x_) && ((y - height) < y_) && (y + height / 2 > y_))
            {

                return true;
            }
            else
                return false;
        }
        public override void SetScale(int modif) {
            if ((x + (width / 2 + modif / 2) < 1535) && (y + (width / 2 + modif / 2) < 790) && (x - (width / 2 + modif / 2) > 0) && (y - (width / 2 + modif/2) > 26)) { width += modif; height += modif; }
        }
        public override void SetCoords(int x_, int y_) { if ((x_ + width/2 < 1535) && (y_ + width / 2 < 790) && (x_ - width / 2 > 0) && (y_ - width / 2 > 26)) { x = x_; y = y_; } }



    }
    public class CCircle:Figure
    {

        public int rad = 20;
        public CCircle(Color color, int x_, int y_)
        {
            x = x_;
            y = y_;
            isSelected = true;
            outer = color;
        }
        ~CCircle()
        {

        }

        public override void Draw( Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x -rad, y - rad, rad * 2, rad * 2);
            Pen pen;
            if (isSelected)
                pen = new Pen(Color.LightSteelBlue, 8);
            else
                pen = new Pen(outer, 8);
            Font font = new Font("Arial", 25, FontStyle.Regular);
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
            Zalivka(sender, bmp, g);

        }
        public override void SetCoords(int x_, int y_) { if ((x_ + rad < 1535) && (y_ + rad < 790) &&(x_ - rad > 0) && (y_ - rad >26)) { x = x_; y = y_; } }

        public override void Zalivka(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 25, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(inner);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
        }

        public override bool isHit(int x_, int y_)
        {
            if (((x - rad) < x_) && (x + rad > x_) && ((y - rad - rad) < y_) && (y + rad > y_))
            {

                return true;
            }
            else
                return false;
        }
        public override void SetScale(int modif) {
            if ((x + (rad+ modif) < 1535) && (y + (rad + modif) < 790) && (x - (rad + modif) > 0) && (y - (rad + modif) > 26)) { rad += modif; }
        }


    }
    public class Figure
    {
        public int x, y;
        public Color inner = Color.White;
        public Color outer = Color.Black;

        public bool isSelected = false;
        public virtual void Draw( Form1 sender, Bitmap bmp, Graphics g) { }
        public void SetSelectedFalse() { isSelected = false; }
        public void SetSelectedTrue() { isSelected = true; }
        public int GetCoorY() { return x; }
        public virtual void SetCoords(int x_, int y_) { x = x_; y = y_; }
        public virtual void SetScale(int modif) {  }

        public int GetCoorX() { return y; }
        public virtual bool isHit(int x_, int y_) { return false; }
        public virtual void Zalivka(Form1 sender, Bitmap bmp, Graphics g) { }
        public virtual void Setcolorout(Color color) { outer = color; }
        public virtual void Setcolorinn(Color color) { inner = color; }





    }
    public class MyStorage
    {
        int size;
        static public int dlc = 0;
        static public int x1, x2, y1, y2;
        static public int dl1 = -1;
        static public int dl2 = -1;
        static public Figure[] objects;

        public MyStorage()
        {
            objects = new Figure[1000];
        }

        ~MyStorage()
        {

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
        public void MoveSelected(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isSelected)
                    {

                        
                        objects[i].SetCoords(x, y);
                        g.Clear(Color.White);
                        DrawAll(sender, bmp, g);
                    }
            }
        }
        public void Add( Figure obj, Form1 sender, Bitmap bmp, Graphics g)
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
                    objects[i].Draw(sender, bmp, g);
            }

        }
        public void isHit(int modif,Color color,int whattodo,int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                    {
                        switch (whattodo) 
                        {
                            case 0:
                                if (!(Control.ModifierKeys == Keys.Control))
                                {
                                    SetAllSelectedFalse();
                                   }
                                objects[i].SetSelectedTrue();
                                DrawAll(sender, bmp, g);
                                break;
                            case 1:
                                objects[i].Setcolorinn(color);
                                objects[i].Draw( sender, bmp, g);

                                break;
                            case 2:
                                objects[i].Setcolorout(color);
                                objects[i].Draw(sender, bmp, g);

                                break;
                            case 3:
                                objects[i].Setcolorout(color);
                                objects[i].Setcolorinn(color);
                                objects[i].Draw(sender, bmp, g);

                                break;
                            case 5:
                                objects[i].SetScale(modif);
                                DrawAll(sender, bmp, g);

                                break;

                            default:
                                break;
                        }
                        
                    }
            }
            if(whattodo == 4)
            MoveSelected(x, y,sender,bmp,g);
        }


    }

}
