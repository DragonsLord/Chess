using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    struct TextLine
    {
        public string Text;
        public StringFormat Format;
        public int Part;
        public Brush brush; 
    }
    public class Panel
    {
        public Font @Font { get; set; }
        public Size @Size { get; set; }
        private float y_margin;
        private PointF _location;
        public PointF Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private Dictionary<int, TextLine> data;
        public Panel()
        {
            Size = new Size((Program.MainWindow.Size.Width * 35)/100, (Program.MainWindow.Size.Height * 75)/100);
            Program.MainWindow.info_paper.Size = Size;
            if (FileManager.options["BoardPosition"] == "Right")
            {
                _location.X = Program.MainWindow.Size.Width * 0.05F;
                _location.Y = Program.MainWindow.Size.Height * 0.08F;
                Program.MainWindow.info_paper.Location = Point.Ceiling(this.Location);
            }
            else 
            {
                _location.X = Program.MainWindow.DrawZone.Size.Width + Program.MainWindow.Size.Width * 0.08F;
                _location.Y = Program.MainWindow.Size.Height * 0.08F;
                Program.MainWindow.info_paper.Location = Point.Ceiling(this.Location);
            }
            y_margin = Program.MainWindow.info_paper.Size.Height / 10;
            this.Font = new Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            data = new Dictionary<int, TextLine>();
        }
        /// <summary>
        /// Add string that should draw by Draw function
        /// </summary>
        /// <param name="text">string ty write</param>
        /// <param name="line_number">number of line wher will be this string</param>
        public void Write(string text, int line_number, bool centered = true)
        {
            TextLine Line = new TextLine();
            Line.Format = new StringFormat();
            Line.Text = text;
            Line.Part =  10;
            Line.brush = Brushes.Black;
            if (centered)
            { 
                Line.Format.Alignment = StringAlignment.Center;
                Line.Part =  2;
            }
            data[line_number] = Line;
        }
        public void Redraw()
        {
            Program.MainWindow.info_paper.Invalidate();
        }
        public void Draw(Graphics g)
        {
            foreach (var dat in data)
                g.DrawString(dat.Value.Text, this.Font, dat.Value.brush, new Point(Size.Width / dat.Value.Part, Size.Height * 5 / 100 + Font.Height * dat.Key), dat.Value.Format);
        }
        public void ClearData()
        {
            data.Clear();
        }
        public void Reshape()
        {
            Size = new Size((Program.MainWindow.Size.Width * 35) / 100, (Program.MainWindow.Size.Height * 75) / 100);
            Program.MainWindow.info_paper.Size = Size;
            if (FileManager.options["BoardPosition"] == "Right")
            {
                _location.X = Program.MainWindow.Size.Width * 0.05F;
                _location.Y = Program.MainWindow.Size.Height * 0.08F;
                Program.MainWindow.info_paper.Location = Point.Ceiling(this.Location);
            }
            else
            {
                _location.X = Program.MainWindow.DrawZone.Size.Width + Program.MainWindow.Size.Width * 0.08F;
                _location.Y = Program.MainWindow.Size.Height * 0.08F;
                Program.MainWindow.info_paper.Location = Point.Ceiling(this.Location);
            }
            y_margin = Program.MainWindow.info_paper.Size.Height / 10;
            this.Font = new Font("Buxton Sketch", Size.Height * 0.03F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }
        //TODO: Show part of log
        public void ShowLog()
        {
            string[] log = FileManager.Log.ToArray();
            int num = Math.Min(FileManager.Log.Count, 10);
            for (int i = 0; i < num; i++)
            {
                TextLine Line = new TextLine();
                Line.Format = new StringFormat();
                Line.Text = log[FileManager.Log.Count - 1 - i];
                Line.Part = 10;
                string[] l = Line.Text.Split();
                if (l[0] == "White")
                    Line.brush = Brushes.White;
                else Line.brush = Brushes.Black;
                Line.Text = l[1] + " " + l[2];
                data[12 - i] = Line;
            }
        }

    }
}
