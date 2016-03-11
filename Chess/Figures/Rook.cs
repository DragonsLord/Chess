using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Properties;
using System.Drawing;

namespace Chess.Figures
{
    public class Rook: Figure, IFirstMove
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
                if (value)
                {
                    if (first_move > 0)
                        first_move--;
                }
                else
                    first_move++;
            }
        }

        public Rook(FigureColor color, Position pos)
            : base(color,pos)
        {
            first_move = 0;
            _type = FigureType.Rook;
            if (color == FigureColor.White)
                _image = Resources.white_rook;
            else _image = Resources.black_rook;
            Value = 800;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.Rook(Position);
        }
        public override void Move(Position pos)
        {
            base.Move(pos);
            FirstMove = false;
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();
            int step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row), king))
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row],this));
            step = 0;
            while (CheckBoard(Position.Column, Position.Row + (++step), this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column, Position.Row + step), king)) 
                    res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column, Position.Row + step],this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row), king)) 
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row],this));
            step = 0;
            while (CheckBoard(Position.Column, Position.Row + (--step), this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column, Position.Row + step), king)) 
                    res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column, Position.Row + step],this));
            return res;
        }
        public override void Set_under_attack_cells()
        {
            int step = 0;
            while (board[Position.Column + (++step), Position.Row] != null && board[Position.Column + step, Position.Row].IsEmpty)
                board[Position.Column + step, Position.Row].Attack(Color);
            if (board[Position.Column + step, Position.Row] != null)
                board[Position.Column + step, Position.Row].Attack(Color);
            step = 0;
            while (board[Position.Column, Position.Row + (++step)] != null && board[Position.Column, Position.Row + step].IsEmpty)
                board[Position.Column, Position.Row + step].Attack(Color);
            if (board[Position.Column, Position.Row + step] != null)
                board[Position.Column, Position.Row + step].Attack(Color);
            step = 0;
            while (board[Position.Column + (--step), Position.Row] != null && board[Position.Column + step, Position.Row].IsEmpty)
                board[Position.Column + step, Position.Row].Attack(Color);
            if (board[Position.Column + step, Position.Row] != null)
                board[Position.Column + step, Position.Row].Attack(Color);
            step = 0;
            while (board[Position.Column, Position.Row + (--step)] != null && board[Position.Column, Position.Row + step].IsEmpty)
                board[Position.Column, Position.Row + step].Attack(Color);
            if (board[Position.Column, Position.Row + step] != null)
                board[Position.Column, Position.Row + step].Attack(Color);
        }
    }
}
