using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Form1 a;
        int whattodo = 0;
        int whattopaint = 0;
        int numberofpointpolygon = 0;
        PointF[] polygon;
        int modif = 1;
        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1 pb = (Form1)sender;
            a = pb;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = (Form1)sender;
            a = pb;

            str.isHit(modif, redraw, whattodo, e.X, e.Y, pb, bmp, g);
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

        private void изменитьРазмерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whattodo = 5;
            whattopaint = -1;

        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void группироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            whattodo = 6;
            whattopaint = -1;
            str.GroupSelected(a, bmp, g);
            str.DrawAll(a, bmp, g);
            this.Refresh();
        }

        private void разгруппироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            str.UnGroupSelected(a,bmp,g);
            whattodo = 7;
            whattopaint = -1;
            g.Clear(Color.White);
            str.DrawAll(a, bmp, g);
            this.Refresh();
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                Graphics g = Graphics.FromImage(bmp);
                str.DeleteSelected();
                g.Clear(Color.White);
                str.DrawAll(a, bmp, g);
                this.Refresh();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            str.saveObjs();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {   str.loadObjs();
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            str.DrawAll(a, bmp, g);
            this.Refresh();
            
        }
    }
    public class RRectangle : Figure
    {
        public RRectangle(Color color, int x_, int y_)
        {
            x = x_;
            y = y_;
            isSelected = true;
            outer = color;
            height = 25;
            width = 25;
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
        public override void save(StreamWriter _file) //сохранение объекта
        {
            _file.WriteLine("R"); //пишем, что записываемый объект - квадрат
            _file.WriteLine(x); //записываем его данные
            _file.WriteLine(y);
            _file.WriteLine(height);
            _file.WriteLine(outer.ToKnownColor());
            _file.WriteLine(inner.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            x = Convert.ToInt32(_file.ReadLine());
            y = Convert.ToInt32(_file.ReadLine());
            height = Convert.ToInt32(_file.ReadLine());
            width = Convert.ToInt32(_file.ReadLine());
            outer = Color.FromName(_file.ReadLine());
            inner = Color.FromName(_file.ReadLine());

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
        public override void SetScale(int modif)
        {
            if ((x + (width / 2 + modif / 2) < 1535) && (y + (width / 2 + modif / 2) < 790) && (x - (width / 2 + modif / 2) > 0) && (y - (width / 2 + modif / 2) > 26)) { width += 2 * modif; height += 2 * modif; }
        }
        public override void SetCoords(int x_, int y_) { if ((x_ + width / 2 < 1535) && (y_ + width / 2 < 790) && (x_ - width / 2 > 0) && (y_ - width / 2 > 26)) { x = x_; y = y_; } }




    }

    public class CCircle : Figure
    {

        public int rad = 20;
        public CCircle(Color color, int x_, int y_)
        {
            x = x_;
            y = y_;
            isSelected = true;
            outer = color;
            width = rad * 2;
            height = rad * 2;

        }
        ~CCircle()
        {

        }
        public override void save(StreamWriter _file) //сохранение объекта
        {
            _file.WriteLine("C"); //пишем, что записываемый объект - круг
            _file.WriteLine(x); //записываем его данные (координаты,радиус и цвет)
            _file.WriteLine(y);
            _file.WriteLine(rad);
            _file.WriteLine(outer.ToKnownColor());
            _file.WriteLine(inner.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            x = Convert.ToInt32(_file.ReadLine());
            y = Convert.ToInt32(_file.ReadLine());
            rad = Convert.ToInt32(_file.ReadLine());
            outer = Color.FromName(_file.ReadLine());
            inner = Color.FromName(_file.ReadLine());
        }
        public override void Draw(Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
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
        public override void SetCoords(int x_, int y_) { if ((x_ + rad < 1535) && (y_ + rad < 790) && (x_ - rad > 0) && (y_ - rad > 26)) { x = x_; y = y_; } }

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
        public override void SetScale(int modif)
        {
            if ((x + (rad + modif) < 1535) && (y + (rad + modif) < 790) && (x - (rad + modif) > 0) && (y - (rad + modif) > 26))
            {
                rad += modif; width = rad * 2;
                height = rad * 2;
            }
        }


    }
    class CFigureFactory
    {
        public Figure CreateFigure(char code)
        {
            Figure figure = null;
            switch (code)
            {
                case 'C':
                    figure = new CCircle(Color.Black,0,0);
                    break;
                case 'R':
                    figure = new RRectangle( Color.Black,0,0);
                    break;
                case 'G':
                    figure = new Group();
                    break;
                default:
                    break;

            }
            return figure;
        }
    }

    public class Figure
    {
        public int width;
        public int height;
        public int size;
        public Figure[] objects;
        public bool grouped = false;
        public int x, y;
        public bool isgroup = false;
        public Color inner = Color.White;
        public Color outer = Color.Black;
        virtual public void save(StreamWriter _file) //сохранение объекта в файл
        {
        }
        virtual public void load(StreamReader _file) //выгрузка данных об объекте из файла
        {
        }
        public bool isSelected = false;
        public virtual void Draw(Form1 sender, Bitmap bmp, Graphics g) { }
        public virtual void SetSelectedFalse() { isSelected = false; }
        public virtual void SetSelectedTrue() { isSelected = true; }
        public int GetCoorY() { return x; }
        public virtual void SetCoords(int x_, int y_) { x = x_; y = y_; }
        public virtual void SetScale(int modif) { }

        public int GetCoorX() { return y; }
        public virtual bool isHit(int x_, int y_) { return false; }
        public virtual void Zalivka(Form1 sender, Bitmap bmp, Graphics g) { }
        public virtual void Setcolorout(Color color) { outer = color; }
        public virtual void Setcolorinn(Color color) { inner = color; }

        public virtual void DeleteSelected() { }
        public virtual void SelectDisplay(int lx, int vy, int weight, int width) { }




    }
    public class Group : Figure
    {
        PointF[] otn;
        int lx, vy;
        public Group()
        {
            objects = new Figure[100];
            otn = new PointF[100];
            size = 0;
            isgroup = true;
        }

        public override void SelectDisplay(int lx, int vy, int height, int width)
        {
            x = lx + width / 2;
            y = vy + height / 2;
            this.height = height;
            this.width = width;
            this.lx = lx;
            this.vy = vy;
        }
        public override void save(StreamWriter _file) //сохранение объекта
        {
            _file.WriteLine("G"); //пишем, что записываемый объект - группа
            _file.WriteLine(size); //записываем размер группы
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null) //если объект существует
                {
                    {
                        objects[i].save(_file); //сохраняем его
                    }
                }
            }
        }
        public override void load(StreamReader _file)
        {
            CFigureFactory factory = new CFigureFactory(); //factory для создания объектов
            char code;  //код, определяюший тип объекта
            size = Convert.ToInt32(_file.ReadLine());
            objects = new Figure[size]; //создаем хранилище определенного размера
            for (int i = 0; i < size; i++)
            {
                code = Convert.ToChar(_file.ReadLine()); //считываем тип объекта
                objects[i] = factory.CreateFigure(code); //factory создает объект определенного типа
                if (objects[i] != null)
                {
                    objects[i].load(_file); //считываем информацию о объекте из файла
                }
            }
        }
        public override void Setcolorinn(Color color)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].inner = color;
                }
            }

        }

        public override void Setcolorout(Color color)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].outer = color;
                }
            }

        }


        ~Group()
        {

        }

        public override void SetScale(int modif)
        {
            if ((lx + width + modif < 1535) && (vy + height + modif < 790) && (lx - modif > 0) && (vy - modif > 0))
            {
                for (int i = 0; i < size; i++)
                {
                    if (objects[i] != null)
                        objects[i].SetScale(modif);
                }
                lx -= modif;
                vy -= modif;
                width += 2 * modif;
                height += 2 * modif;
                x = lx + width / 2;
                y = vy + height / 2;

            }
        }
        public override void SetCoords(int x_, int y_)
        {
            if ((x_ + width < 1535) & (y_ + height < 790))
            {
                for (int i = 0; i < size; i++)
                {
                    if (objects[i] != null)
                    {
                        otn[i].X = objects[i].x - lx;
                        otn[i].Y = objects[i].y - vy;
                    }
                }

                lx = x_;
                x = lx;
                vy = y_;
                y = vy;
                for (int i = 0; i < size; i++)
                {
                    if (objects[i] != null)
                    {
                        if (objects[i].isgroup)
                        {
                            objects[i].SetCoords(Convert.ToInt32(lx + otn[i].X), Convert.ToInt32(vy + otn[i].Y));
                        }
                        else
                        {
                            objects[i].x = Convert.ToInt32(lx + otn[i].X);
                            objects[i].y = Convert.ToInt32(vy + otn[i].Y);
                        }
                    }
                }

            }

        }
        public void Add(Figure obj)
        {
            objects[size] = obj;
            size++;


        }
        public override void Draw(Form1 sender_, Bitmap bmp_, Graphics g_)
        {
            Rectangle rect = new Rectangle(lx, vy, width, height);
            Pen pen;
            if (isSelected)
                pen = new Pen(Color.LightSteelBlue, 1);
            else
                pen = new Pen(outer, 1);
            g_.DrawRectangle(pen, rect);
            sender_.BackgroundImage = bmp_;
            for (int i = 0; i < size + 1; i++)
            {
                if (objects[i] != null)
                    objects[i].Draw(sender_, bmp_, g_);
            }

        }
        public override void SetSelectedFalse()
        {
            isSelected = false;
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    objects[i].SetSelectedFalse();

            }
        }

        public override void SetSelectedTrue()
        {
            isSelected = true;
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    objects[i].SetSelectedTrue();

            }

        }

        public override bool isHit(int x, int y)
        {
            bool selected = false;
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                    {
                        return true;

                    }
            }
            return false;
        }


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
                if (objects[i] != null)
                    if (objects[i].isSelected)
                        if (objects[i].isgroup)
                        {
                            objects[i].DeleteSelected();
                            objects[i] = null;
                        }
                        else
                            objects[i] = null;
            }
        }
        public void saveObjs() //функция сохранения хранилища в файл
        {

            string path = @"C:\Users\q3032\source\repos\oooplaba4.1\save.txt"; //путь до файла
            StreamWriter cfile = new StreamWriter(path, false); //создаем записыватель файла
            cfile.WriteLine(size); //записываем размер хранилища
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null) //если объект существует
                {
                    {
                        objects[i].save(cfile); //сохраняем его
                    }
                }
            }
            cfile.Close();
        }
        public void loadObjs() //выгрузка объектов из файла в хранилище
        {
            string path = @"C:\Users\q3032\source\repos\oooplaba4.1\save.txt"; //путь до файла
            CFigureFactory factory = new CFigureFactory(); //factory для создания объектов
            StreamReader sr = new StreamReader(path); //читатель файла
            char code;  //код, определяюший тип объекта
            size = Convert.ToInt32(sr.ReadLine());
            objects = new Figure[100]; //создаем хранилище определенного размера
            for (int i = 0; i < size; i++)
            {
                code = Convert.ToChar(sr.ReadLine()); //считываем тип объекта
                objects[i] = factory.CreateFigure(code); //factory создает объект определенного типа
                if (objects[i] != null)
                {
                    objects[i].load(sr); //считываем информацию о объекте из файла
                }
            }
            sr.Close(); //закрываем файл

        }
        public void MoveSelected(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isSelected)
                        objects[i].SetCoords(x, y);
            }
        }
        public void GroupSelected(Form1 sender, Bitmap bmp, Graphics g)
        {
            int lx = 1000, rx = 0, vy = 1000, ny = 0, cx, cy;
            Group gr = new Group();
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isSelected)
                    {

                        if (lx > objects[i].x - objects[i].width / 2) lx = objects[i].x - objects[i].width / 2;
                        if (rx < objects[i].x + objects[i].width / 2) rx = objects[i].x + objects[i].width / 2;
                        if (vy > objects[i].y - objects[i].height / 2) vy = objects[i].y - objects[i].height / 2;
                        if (ny < objects[i].y + objects[i].height / 2) ny = objects[i].y + objects[i].height / 2;
                        gr.Add(objects[i]);
                        objects[i] = null;
                    }
            }
            MessageBox.Show(" " + lx + " " + rx + " " + vy + " " + ny + " " + " " + " ");
            int height = (ny - vy);
            int width = (rx - lx);
            gr.SelectDisplay(lx, vy, height, width);
            Add(gr, sender, bmp, g);
        }
        public void UnGroupSelected(Form1 sender, Bitmap bmp, Graphics g)
        {

            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isSelected)
                        if (objects[i].isgroup)
                        {
                            
                            for (int j = 0; j < objects[i].size; j++)
                            {
                                if (objects[i].objects[j] != null)
                                {
                                    Add(objects[i].objects[j], sender, bmp, g);
                                }
                            }

                            objects[i] = null;
                            SetAllSelectedFalse();
                        }
                        else
                            objects[i] = null;
            }

        }

        public void Add(Figure obj, Form1 sender, Bitmap bmp, Graphics g)
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
                if ((objects[i] != null) && (!objects[i].grouped))
                    objects[i].Draw(sender, bmp, g);
            }

        }
        public void isHit(int modif, Color color, int whattodo, int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                {
                    if ((whattodo == 4) && (objects[i].isSelected))
                        MoveSelected(x, y, sender, bmp, g);
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
                                objects[i].Draw(sender, bmp, g);

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
                            case 6:
                                GroupSelected(sender, bmp, g);
                                DrawAll(sender, bmp, g);


                                break;
                            case 7:
                                if (objects[i].isgroup)
                                {
                                    UnGroupSelected(sender, bmp, g);
                                    DrawAll(sender, bmp, g);
                                }

                                break;

                            default:
                                break;
                        }

                    }
                }
            }

        }


    }

}
