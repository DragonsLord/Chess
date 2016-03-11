using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Properties;
using System.Drawing;

namespace Chess.Figures
{
    public class Horse : Figure
    {
        public Horse(FigureColor color, Position pos)
            : base(color, pos)
        {
            _type = FigureType.Horse;
            if (color == FigureColor.White)
                _image = Resources.white_horse;
            else _image = Resources.black_horse;
            Value = 500;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.Horse(Position);
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();
            if (CheckBoard(Position.Column + 1, Position.Row + 2, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + 1, Position.Row + 2], this));

            if (CheckBoard(Position.Column + 2, Position.Row + 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + 2, Position.Row + 1], this));

            if (CheckBoard(Position.Column + 2, Position.Row - 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + 2, Position.Row - 1], this));

            if (CheckBoard(Position.Column + 1, Position.Row - 2, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + 1, Position.Row - 2], this));

            if (CheckBoard(Position.Column - 1, Position.Row - 2, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column - 1, Position.Row - 2], this));

            if (CheckBoard(Position.Column - 2, Position.Row - 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column - 2, Position.Row - 1], this));

            if (CheckBoard(Position.Column - 2, Position.Row + 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column - 2, Position.Row + 1], this));

            if (CheckBoard(Position.Column - 1, Position.Row + 2, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column - 1, Position.Row + 2], this));
            return res;
        }
        public override void Set_under_attack_cells()
        {
            if (board[Position.Column + 1, Position.Row + 2] != null && (board[Position.Column + 1, Position.Row + 2].IsEmpty || board[Position.Column + 1, Position.Row + 2].IsOponentFigure(Color)))
                board[Position.Column + 1, Position.Row + 2].Attack(Color);

            if (board[Position.Column + 2, Position.Row + 1] != null && (board[Position.Column + 2, Position.Row + 1].IsEmpty || board[Position.Column + 2, Position.Row + 1].IsOponentFigure(Color)))
                board[Position.Column + 2, Position.Row + 1].Attack(Color);

            if (board[Position.Column + 2, Position.Row - 1] != null && (board[Position.Column + 2, Position.Row - 1].IsEmpty || board[Position.Column + 2, Position.Row - 1].IsOponentFigure(Color)))
                board[Position.Column + 2, Position.Row - 1].Attack(Color);

            if (board[Position.Column + 1, Position.Row - 2] != null && (board[Position.Column + 1, Position.Row - 2].IsEmpty || board[Position.Column + 1, Position.Row - 2].IsOponentFigure(Color)))
                board[Position.Column + 1, Position.Row - 2].Attack(Color);

            if (board[Position.Column - 1, Position.Row - 2] != null && (board[Position.Column - 1, Position.Row - 2].IsEmpty || board[Position.Column - 1, Position.Row - 2].IsOponentFigure(Color)))
                board[Position.Column - 1, Position.Row - 2].Attack(Color);

            if (board[Position.Column - 2, Position.Row - 1] != null && (board[Position.Column - 2, Position.Row - 1].IsEmpty || board[Position.Column - 2, Position.Row - 1].IsOponentFigure(Color)))
                board[Position.Column - 2, Position.Row - 1].Attack(Color);

            if (board[Position.Column - 2, Position.Row + 1] != null && (board[Position.Column - 2, Position.Row + 1].IsEmpty || board[Position.Column - 2, Position.Row + 1].IsOponentFigure(Color)))
                board[Position.Column - 2, Position.Row + 1].Attack(Color);

            if (board[Position.Column - 1, Position.Row + 2] != null && (board[Position.Column - 1, Position.Row + 2].IsEmpty || board[Position.Column - 1, Position.Row + 2].IsOponentFigure(Color)))
                board[Position.Column - 1, Position.Row + 2].Attack(Color);
        }
    }
}
