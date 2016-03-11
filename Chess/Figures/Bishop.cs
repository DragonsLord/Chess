using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Chess.Properties;

namespace Chess.Figures
{
    public class Bishop: Figure
    {
        public Bishop(FigureColor color, Position pos)
            : base(color,pos)
        {
            
            _type = FigureType.Bishop;
            if (color == FigureColor.White)
                _image = Resources.white_bishop;
            else _image = Resources.black_bishop;
            Value = 500;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.Bishop(Position);
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();
            int step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row + step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row + step), king))
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step],this));
            step = 0;
            while (CheckBoard(Position.Column + (++step), Position.Row - step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row - step), king))
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row - step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step],this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row - step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row - step), king))
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row - step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row - step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row - step],this));
            step = 0;
            while (CheckBoard(Position.Column + (--step), Position.Row + step, this, king, CellCondition.IsEmpty))
            {
                if (!IfCouseToCheck(new Position(Position.Column + step, Position.Row + step), king))
                    res.Add(new MoveAction(board[Position.Column + step, Position.Row + step], this));
            }
            if (CheckBoard(Position.Column + step, Position.Row + step, this, king, CellCondition.IsOponentFigure, CellCondition.CouseToCheck))
                res.Add(new MoveAction(board[Position.Column + step, Position.Row + step],this));
            return res;
        }
        public override void Set_under_attack_cells()
        {
            int step = 0;
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
