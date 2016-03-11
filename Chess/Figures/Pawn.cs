using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Chess.Properties;

namespace Chess.Figures
{
    class Pawn : Figure, IFirstMove
    {
        private int first_move;
        public bool FirstMove
        {
            get
            {
                return (first_move == 0);
            }
            set
            {
                if(value)
                {
                    if (first_move > 0)
                        first_move--;
                }
                else
                    first_move++;
            }
        }

        public Pawn(FigureColor color, Position pos)
            : base(color, pos)
        {
            if (color == FigureColor.White)
                _image = Resources.white_pawn;
            else _image = Resources.black_pawn;
            _type = FigureType.Pawn;
            first_move = 0;
            Value = 300;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.Pawn(Position);
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();
            int step;
            if (Color == FigureColor.White)
                step = 1;
            else step = -1;
            if (CheckBoard(Position.Column, Position.Row + step, this, king, CellCondition.IsEmpty, CellCondition.CouseToCheck))
            {
                res.Add(new MoveAction(board[Position.Column, Position.Row + step],this));
                if (FirstMove && CheckBoard(Position.Column, Position.Row + 2 * step, this, king, CellCondition.IsEmpty, CellCondition.CouseToCheck))
                {
                    res.Add(new MoveAction(board[Position.Column, Position.Row + 2 * step],this));
                }
            }
            if (CheckBoard(Position.Column + step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step],this));
            if (CheckBoard(Position.Column - step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column - step, Position.Row + step],this)); 
            return res;
        }
        public override void Move(Position pos)
        {
            base.Move(pos);
            FirstMove = false;
            if ((Position.Row == 7 && Color == FigureColor.White) || (Position.Row == 0 && Color == FigureColor.Black))
                transform();
        }
        public override void Set_under_attack_cells()
        {
            int step;
            if (Color == FigureColor.White)
                step = 1;
            else step = -1;

            if (board[Position.Column + 1, Position.Row + step] != null)
                board[Position.Column + 1, Position.Row + step].Attack(Color);
            if (board[Position.Column - 1, Position.Row + step] != null)
                board[Position.Column - 1, Position.Row + step].Attack(Color);
        }
        private void default_transform()
        {
            board[Position].ChessFigure = new Queen(Color, Position);
        }
        private event Action transform;
        public event Action Transform
        {
            add
            {
                transform += value;
                transform -= default_transform;
            }
            remove
            {
                transform -= value;
                transform += default_transform;
            }
        }
    }
}
