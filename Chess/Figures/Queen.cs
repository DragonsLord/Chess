using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Properties;
using System.Drawing;

namespace Chess.Figures
{
    public class Queen: Figure
    {
        public Queen(FigureColor color, Position pos)
            : base(color,pos)
        {
            _type = FigureType.Queen;
            if (color == FigureColor.White)
                _image = Resources.white_queen;
            else _image = Resources.black_queen;
            Value = 900;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.Queen(Position);
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();

            #region Rook
            int step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            step = 0;
            while (CheckBoard(Position.Column, Position.Row + (++step), this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column, Position.Row + step), king)) 
                res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row], this));
            step = 0;
            while (CheckBoard(Position.Column, Position.Row + (--step), this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column, Position.Row + step), king)) 
                res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column, Position.Row + step], this));
            #endregion

            #region Bishop
            step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row + step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row + step), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row - step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row - step), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row - step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row - step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row - step), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row - step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row + step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row + step), king)) 
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            #endregion

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

            step = 0;
            while (board[Position.Column + (++step), Position.Row + step] != null && board[Position.Column + step, Position.Row + step].IsEmpty)
                board[Position.Column + step, Position.Row + step].Attack(Color);
            if (board[Position.Column + step, Position.Row + step] != null)
                board[Position.Column + step, Position.Row + step].Attack(Color);
            step = 0;
            while (board[Position.Column + (++step), Position.Row - step] != null && board[Position.Column + step, Position.Row - step].IsEmpty)
                board[Position.Column + step, Position.Row - step].Attack(Color);
            if (board[Position.Column + step, Position.Row - step] != null)
                board[Position.Column + step, Position.Row - step].Attack(Color);
            step = 0;
            while (board[Position.Column + (--step), Position.Row - step] != null && board[Position.Column + step, Position.Row - step].IsEmpty)
                board[Position.Column + step, Position.Row - step].Attack(Color);
            if (board[Position.Column + step, Position.Row - step] != null)
                board[Position.Column + step, Position.Row - step].Attack(Color);
            step = 0;
            while (board[Position.Column + (--step), Position.Row + step] != null && board[Position.Column + step, Position.Row + step].IsEmpty)
                board[Position.Column + step, Position.Row + step].Attack(Color);
            if (board[Position.Column + step, Position.Row + step] != null)
                board[Position.Column + step, Position.Row + step].Attack(Color);

        }
    }
}
