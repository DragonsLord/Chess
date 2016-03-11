using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

using Chess.Figures;

namespace Chess
{
    public class Cell
    {
        static public SizeF @Size { get; set; }
        private Figure _figure;
        public Figure ChessFigure
        {
            get 
            {
                return _figure;
            }
            set
            {
                _empty = false;
                _figure = value;
            }
        }

        private PointF _coordinates;
        public PointF Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
            }
        }

        private Position _position;
        public Position @Position
        {
            get
            {
                return _position;
            }
        }

        private bool _empty;
        public bool IsEmpty
        {
            get
            {
                return _empty;
            }
        }

        private bool _current;
        public bool IsCurrent
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        private bool _glowing;
        public bool IsGlowing
        {
            get
            {
                return _glowing;
            }
            set
            {
                _glowing = value;
            }
        }

        public string Name
        {
            get
            {
                return ((char)(Position.Column + 97) + (Position.Row+1).ToString());
            }
        }

        private int white_attack;
        public int NumOfWhiteAttackers
        {
            get { return white_attack; }
        }
        private int black_attack;
        public int NumOfBlackAttackers
        {
            get { return black_attack; }
        }
        /// <summary>
        /// Create cell
        /// </summary>
        /// <param name="i">Column</param>
        /// <param name="j">Row</param>
        public Cell(int i, int j, float x, float y)
        {
            _position.Row = j;
            _position.Column = i;
            _coordinates.X = x;
            _coordinates.Y = y;
            _figure = null;
            _empty = true;
            _current = false;
            _glowing = false;
            white_attack = 0;
            black_attack = 0;
        }

        public void Draw(Graphics g)
        {
            Pen pensil = new Pen(Color.Red,2);
            if (!IsEmpty)
            {
                ChessFigure.Draw(g,Coordinates,Size);
            }
            if (IsCurrent && IsGlowing)
            {
                pensil.Color = Color.Green;
                g.DrawRectangle(pensil, Coordinates.X + 0.5F, Coordinates.Y + 0.5F, Size.Width - 1, Size.Height - 1);
            }
            else
            {
                if (IsCurrent)
                {
                    pensil.Color = Color.Red;
                    g.DrawRectangle(pensil, Coordinates.X + 0.5F, Coordinates.Y + 0.5F, Size.Width - 1, Size.Height - 1);
                }
                if (IsGlowing)
                {
                    pensil.Color = Color.Blue;
                    g.DrawRectangle(pensil, Coordinates.X + 0.5F, Coordinates.Y + 0.5F, Size.Width - 1, Size.Height - 1);
                }
            }
            #region Figure Move Animation

            #endregion
        }
        public void Remove_figure()
        {
            _empty = true;
            _figure = null;
        }
        public void Attack(FigureColor color)
        {
            if (color == FigureColor.White)
                white_attack++;
            else
                black_attack++;
        }
        public void Unattack(FigureColor color)
        {
            if (color == FigureColor.White)
                white_attack--;
            else
                black_attack--;
        }
        /// <summary>
        /// Clear all attack statistics
        /// </summary>
        public void Clear()
        {
            white_attack = 0;
            black_attack = 0;
            IsGlowing = false;
        }
        public bool Is_under_attack(FigureColor color)
        {
            if (color == FigureColor.White)
            {
                if (black_attack > 0)
                    return true;
                else return false;
            }
            else 
            {
                if (white_attack > 0)
                    return true;
                else return false;
            }
        }
        public bool IsOponentFigure(FigureColor color)
        {
            if (IsEmpty)
                return false;
            else if (color != ChessFigure.Color)
                return true;
            else return false;
        }
    }
}
