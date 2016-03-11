using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Chess.Figures;

namespace Chess
{
    public class Player
    {
        public string Name { get; set; }
        private FigureColor figure_color;
        private King king;
        public King @King
        {
            get { return king; }
        }
        public static bool Transforming { get; set; }
        static public Position current;
        static private Position previous;
        static private Figure choosen_figure;
        static private GameBoard board;
        static public GameBoard Board
        {
            set { board = value; }
        }
        static private Panel Info_panel;
        static public Panel InfoPanel
        {
            set { Info_panel = value; }
        }
        static Player()
        {
            choosen_figure = null;
            current.Column = 0;
            current.Row = 0;
            previous = current;
        }
        public Player(FigureColor color, ref King k)
        {
            figure_color = color;
            king = k;
            Transforming = false;
            int row;
            if (figure_color == FigureColor.White)
                row = 1;
            else
                row = 6;
            Pawn temp;
            for (int i = 0; i < 8; i++)
            {
                temp = board[i, row].ChessFigure as Pawn;
                temp.Transform += this.Transform;
            }
        }

        private void Transform()
        {
            Info_panel.Write("Choose new figure:", 3, false);
            Info_panel.Write("1. Pawn", 4);
            Info_panel.Write("2. Bishop", 5);
            Info_panel.Write("3. Horse", 6);
            Info_panel.Write("4. Rook", 7);
            Info_panel.Write("5. Queen", 8);
            Transforming = true;
        }

        /// <summary>
        /// Player control function
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Indicator for the next turn</returns>
        public bool mouse_control(int x, int y)
        {
            Cell current = board.GetCellByCoordinates(x, y);
            Player.current = current.Position;
            bool next_turn = false;
            board[previous].IsCurrent = false;
            current.IsCurrent = true;

            if (Transforming)
                //TODO
                next_turn = false;
            #region Move Figure
            else if (choosen_figure != null && current.IsGlowing)
            {
                //if (choosen_figure.IfCouseToCheck(Player.current, king))
                //{
                //    Info_panel.Write("Not allowed.", 4);
                //    Info_panel.Write("King will be under attack!!!", 5);
                //}
                //else
                {
                    choosen_figure.Move(Player.current);
                    choosen_figure = null;
                    if (Transforming)
                        next_turn = false;
                    else
                        next_turn = true;
                }
            }
            #endregion

            #region Choose Figure
            else if (!current.IsEmpty && !current.IsOponentFigure(figure_color))
            { 
                current.ChessFigure.Show_ways(king);
                choosen_figure = current.ChessFigure;
            }
            previous = Player.current;
            #endregion
            return next_turn;
        }
        public bool keyboard_control(Keys key)
        {
            if (Transforming)
            {
                switch(key)
                {
                    case Keys.D1: { break; }
                    case Keys.D2: { board[current].ChessFigure = new Bishop(figure_color, current); break; }
                    case Keys.D3: { board[current].ChessFigure = new Horse(figure_color, current); break; }
                    case Keys.D4: { board[current].ChessFigure = new Rook(figure_color, current); break; }
                    case Keys.D5: { board[current].ChessFigure = new Queen(figure_color, current); break; }
                    default: return false;
                }
                Transforming = false;
                Program.MainWindow.DrawZone.Refresh();
                return true;
            }
            return false;
        }

        public bool Control(Cell pos, ControlAction action)
        {
            bool next_turn = false;
            if (pos != null)
            {
                previous = current;
                current = pos.Position;
                board[current].IsCurrent = true;
                board[previous].IsCurrent = false;
            }
            switch (action)
            {
                case ControlAction.Down:
                    {
                        previous = current;
                        if (current.Row > 0)
                            current.Row--;
                        else current.Row = 7;
                        board[current].IsCurrent = true;
                        board[previous].IsCurrent = false;
                        break;
                    }
                case ControlAction.Up:
                    {
                        previous = current;
                        if (current.Row < 7)
                            current.Row++;
                        else current.Row = 0;
                        board[current].IsCurrent = true;
                        board[previous].IsCurrent = false;
                        break;
                    }
                case ControlAction.Left:
                    {
                        previous = current;
                        if (current.Column > 0)
                            current.Column--;
                        else current.Column = 7;
                        board[current].IsCurrent = true;
                        board[previous].IsCurrent = false;
                        break;
                    }
                case ControlAction.Right:
                    {
                        previous = current;
                        if (current.Column < 7)
                            current.Column++;
                        else current.Column = 0;
                        board[current].IsCurrent = true;
                        board[previous].IsCurrent = false;
                        break;
                    }
                case ControlAction.Choose:
                    {
                        if (choosen_figure != null && board[current].IsGlowing)
                        {
                            {
                                FileManager.WriteToLog(choosen_figure.Color.ToString() + " " + choosen_figure.Type.ToString() + ": " + board[choosen_figure.Position].Name + board[current].Name);
                                choosen_figure.Move(Player.current);
                                choosen_figure = null;
                                if (Transforming)
                                    next_turn = false;
                                else
                                    next_turn = true;
                            }
                        }
                        else if (!board[current].IsEmpty && !board[current].IsOponentFigure(figure_color))
                        {
                            board[current].ChessFigure.Show_ways(king);
                            choosen_figure = board[current].ChessFigure;
                        }
                        break;
                    }
            }
            return next_turn;
        }
    }
}
