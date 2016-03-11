using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Chess.Figures
{
    public enum FigureType {Pawn, Bishop, Horse, Rook, Queen, King}
    public enum FigureColor { White, Black}
    /// <summary>
    /// Possible condition need to check
    /// </summary>
    public enum CellCondition {IsEmpty, IsGlowing, IsUnderAttack, IsOponentFigure, CouseToCheck}

    public struct Position
    {
        public int Row;
        public int Column;
        public Position(int x, int y)
        {
            Row = y;
            Column = x;
        }
    }

    public abstract class Figure
    {
        protected Image _image; ///< index fo image in ImageList
        public Image Image
        {
            get
            {
                return _image;
            }
        }
        protected FigureType _type;
        public FigureType Type
        {
            get 
            {
                return _type;
            }
        }
        protected FigureColor _color;
        public FigureColor @Color
        {
            get { return _color; }
        }
        /// <summary>
        /// Need for animation
        /// </summary>
        protected PointF previous_position;
        protected Position _position;
        public Position @Position
        {
            get 
            {
                return _position;
            }
            set 
            {
                _position = value;
            }
        }

        static protected GameBoard board;
        static public GameBoard Board
        {
            set { board = value; }
        }

        public int Value { get; set; }

        public Figure(FigureColor color, Position position)
        {
            _color = color;
            previous_position = board[position].Coordinates;
            _position = position;
            board.SituationChanged += Set_under_attack_cells;
        }

        abstract public int EvaluatePosition();
        abstract public List<MoveAction> GetPossibleMoves(King king);
        public void Show_ways(King king)
        {
            board.Unglow();
            List<MoveAction> moves = GetPossibleMoves(king);
            foreach (var move in moves)
                move.Goal.IsGlowing = true;
        }
        public bool IfCouseToCheck(Position pos, King king)
        {
            Figure temp = null;
            Position previous = Position;
            Position = pos;
            if (!board[pos].IsEmpty)
            {
                //remove this figure from event hendler
                board.SituationChanged -= board[pos].ChessFigure.Set_under_attack_cells;
                temp = board[pos].ChessFigure;
            }
            board[previous].Remove_figure();
            board[Position].ChessFigure = this;
            board.SituationUpdate();
            bool answer = king.IsCheck;
            board[Position].ChessFigure = temp;
            board[previous].ChessFigure = this;
            Position = previous;
            if (temp != null)
            {
                //return this figure to event hendler
                board.SituationChanged += board[pos].ChessFigure.Set_under_attack_cells;
            }
            else board[pos].Remove_figure();
            board.SituationUpdate();
            return answer;
        }
        virtual public void Move(Position pos)
        {
            board[Position].Remove_figure();
            previous_position = board[Position].Coordinates;
            Position = pos;
            #region Animation
            if (FileManager.options["Animation"] == "True")
            {
                Program.MainWindow.DrawZone.Paint += DrawFrame;
                MoveAnimation();
                Program.MainWindow.DrawZone.Paint -= DrawFrame;
            }
            #endregion
            
            if (!board[pos].IsEmpty)
            {
                //remove this figure from event hendler
                board.SituationChanged -= board[pos].ChessFigure.Set_under_attack_cells;
                board[pos].ChessFigure.Value = 0;
                board[pos].Remove_figure();
            }

            board[Position].ChessFigure = this;
            board.SituationUpdate();
        }
        abstract public void Set_under_attack_cells();
        public void Draw(Graphics g, PointF coord, SizeF size)
        {
            g.DrawImage(Image, new RectangleF(coord, size));
        }
        protected void DrawFrame(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics,previous_position, Cell.Size);
        }
        private void MoveAnimation()
        {
            PointF speed_vector = new PointF();
            PointF final_pos = board[Position].Coordinates;

            #region Speed Vector Culculation
            float speed_value = board.BoardSize.Width / 100;
            PointF distance_vector = new PointF(final_pos.X - previous_position.X, final_pos.Y - previous_position.Y);
            float vector_angle;
            if (distance_vector.X >= 0)
                vector_angle = (float)Math.Atan(distance_vector.Y / distance_vector.X);
            else vector_angle = (float)(Math.PI + Math.Atan(distance_vector.Y / distance_vector.X));
            speed_vector.X = speed_value * (float)Math.Cos(vector_angle);
            speed_vector.Y = speed_value * (float)Math.Sin(vector_angle);
            #endregion
            Program.MainWindow.ForbidControl();
            var t = System.Threading.Tasks.Task.Run( () =>
            {
                while (!(Math.Abs(previous_position.X - final_pos.X) < speed_value && Math.Abs(previous_position.Y - final_pos.Y) < speed_value))
                {
                    previous_position.X += speed_vector.X;
                    previous_position.Y += speed_vector.Y;
                    //Program.MainWindow.DrawZone.Refresh();
                    Program.MainWindow.DrawOnBoard();
                    Thread.Sleep(100);
                }
            });
            t.Wait();
            Program.MainWindow.AllowContol();
        }

        /// <summary>
        /// Check cell condition save from OutOfRange exception
        /// </summary>
        /// <param name="i">column</param>
        /// <param name="j">raw</param>
        /// <param name="conditions">OR condition</param>
        /// <param name="color">figure color</param>
        /// <returns>indicator</returns>
        static public bool CheckBoard(int i, int j, Figure figure, King king, params CellCondition[] conditions)
        {
            bool answer = false;
            if (board[i, j] != null)
            {
                foreach (var cond in conditions)
                {
                    switch (cond)
                    {
                        case CellCondition.IsEmpty:
                            answer = answer || board[i, j].IsEmpty;
                            break;
                        case CellCondition.IsGlowing:
                            answer = answer || board[i, j].IsGlowing;
                            break;
                        case CellCondition.IsOponentFigure:
                            answer = answer || board[i, j].IsOponentFigure(figure.Color);
                            break;
                        case CellCondition.IsUnderAttack:
                            answer = answer || board[i, j].Is_under_attack(figure.Color);
                            break;
                        case CellCondition.CouseToCheck:
                            answer = answer && !figure.IfCouseToCheck(new Position() { Column = i, Row = j }, king);
                            break;
                        default: return false;
                    }
                }
                return answer;
            }
            else return false;
        }

    }
}
