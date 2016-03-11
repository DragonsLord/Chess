using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Chess.Figures;

namespace Chess
{
    public class GameBoard
    {
        private float border_part; ///< part of board taken by borders
        public float BorderPart
        {
            get { return border_part; }
        }
        private SizeF board_size;
        public SizeF BoardSize
        {
            get
            {
                return board_size;
            }
        }
        private PointF board_position;
        public PointF BoardPosition
        {
            get
            {
                return board_position;
            }
        }

        public GameBoard Copy()
        {
            return this.MemberwiseClone() as GameBoard;
        }

        private Cell[,] board;
        public Cell this[int i,int j]
        {
            get 
            {
                if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    return board[i, 7-j];
                else return null;
            }
        }
        public Cell this[char c, int i]
        {
            get
            {
                int j = 0;
                switch (c)
                {
                    case 'A':
                    case 'a': { j = 0; break; }
                    case 'B':
                    case 'b': { j = 1; break; }
                    case 'C':
                    case 'c': { j = 2; break; }
                    case 'D':
                    case 'd': { j = 3; break; }
                    case 'E':
                    case 'e': { j = 4; break; }
                    case 'F':
                    case 'f': { j = 5; break; }
                    case 'G':
                    case 'g': { j = 6; break; }
                    case 'H':
                    case 'h': { j = 7; break; }
                    default: return null; 
                }
                if (i >= 0 && i < 8)
                    return board[j, 7 - i];
                else return null;
            }
        }
        public Cell this[Position pos]
        {
            get 
            {
                if (pos.Column >= 0 && pos.Column < 8 && pos.Row >= 0 && pos.Row < 8)
                    return board[pos.Column, 7 - pos.Row];
                else return null;
            }
        }

        public void DebugShow()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    System.Diagnostics.Debug.Write(String.Format("{0,7}", this[j, i].ChessFigure.Type.ToString()));
                }
                System.Diagnostics.Debug.WriteLine("/n");
            }
        }

        public GameBoard()
        {
            Size window_size = Program.MainWindow.Size;
            board_size.Width = board_size.Height = (window_size.Width * 0.55F);
            board_position.X = board_position.Y = (window_size.Width * 0.05F);
            if (window_size.Height - 2 * board_position.Y < board_size.Height)
            {
                board_size.Height = board_size.Width = window_size.Height - 2 * board_position.Y;
            }
            if (FileManager.options["BoardPosition"] == "Right")
                board_position.X = Program.MainWindow.Size.Width - board_size.Width - board_position.X;
            border_part = board_size.Width * 0.03F;
            float s = (board_size.Width - 2 * border_part) / 8 ;
            Cell.Size = new SizeF(s,s);
            board = new Cell[8, 8];
        }
        /// <summary>
        /// Change board size when window reshape
        /// </summary>
        public void Reshape()
        {
            Size window_size = Program.MainWindow.Size;
            board_size.Width = board_size.Height = (window_size.Width * 55) / 100;
            board_position.X = board_position.Y = (window_size.Width * 5) / 100;
            if (window_size.Height - 2 * board_position.Y < board_size.Height)
            {
                board_size.Height = board_size.Width = window_size.Height - 2 * board_position.Y;
            }
            if (FileManager.options["BoardPosition"] == "Right")
                board_position.X = Program.MainWindow.Size.Width - board_size.Width - board_position.X;
            border_part = board_size.Width * 3 / 100;
            float s = (board_size.Width - 2 * border_part) / 8;
            Cell.Size = new SizeF(s, s);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[i, j].Coordinates = new PointF(border_part + i * Cell.Size.Width, border_part + j * Cell.Size.Height);
                }
        }
        public void Draw(Graphics g)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j].Draw(g);
        }
        public void Open()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Cell(i, 7 - j, border_part + i * Cell.Size.Width, border_part + j * Cell.Size.Height);
                }
        }
        public void Close()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j] = null;
            foreach (var method in Handler)
                situation_changed -= method;
            Handler.Clear();
        }
        public Cell GetCellByCoordinates(int x, int y)
        {
            x -= (int)border_part + 1;
            y -= (int)border_part + 1;
            Position pos;
            pos.Column = x/(int)Cell.Size.Width;
            pos.Row = 7 - y/(int)Cell.Size.Height;
            return this[pos];
        }
        public void Unglow()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j].IsGlowing = false;
        }

        public delegate void SituationManipylator();
        private List<SituationManipylator> Handler = new List<SituationManipylator>(); ///< list of all callbacks
        private event SituationManipylator situation_changed;
        public event SituationManipylator SituationChanged
        {
            add
            {
                situation_changed += value;
                Handler.Add(value);
            }
            remove
            {
                situation_changed -= value;
                Handler.Remove(value);
            }
        }

        public void SituationUpdate()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this[i, j].Clear();
            situation_changed();
        }

    }
}
