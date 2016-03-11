using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Properties;
using System.Drawing;

namespace Chess.Figures
{
    public class King: Figure, IFirstMove
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
                    else first_move = 0;
                }
                else
                    first_move++;
            }
        }
        public bool IsCheck
        {
            get
            {
                return board[Position].Is_under_attack(Color);
            }
        }

        public King(FigureColor color, Position pos)
            : base(color,pos)
        {
            first_move = 0;
            _type = FigureType.King;
            if (color == FigureColor.White)
                _image = Resources.white_king;
            else _image = Resources.black_king;
            Value = 400;
        }

        public override int EvaluatePosition()
        {
            return PositionValues.King(Position);
        }
        public override void Move(Position pos)
        {
            #region Castle Case
            if (pos.Column - Position.Column == 2 && pos.Column == 6)
            {
                Position temp = new Position{Column = Position.Column +1, Row = Position.Row};
                board[pos.Column + 1, pos.Row].ChessFigure.Move(temp);
            }
            else if (pos.Column - Position.Column == -2 && pos.Column == 2) 
            {
                Position temp = new Position { Column = 3, Row = Position.Row };
                board[0, pos.Row].ChessFigure.Move(temp);
            }
            #endregion
            base.Move(pos);
            FirstMove = false;
        }
        public override List<MoveAction> GetPossibleMoves(King king)
        {
            List<MoveAction> res = new List<MoveAction>();
            if (CheckBoard(Position.Column, Position.Row + 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column, Position.Row + 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column, Position.Row + 1],this));

            if (CheckBoard(Position.Column + 1, Position.Row + 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column + 1, Position.Row + 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column + 1, Position.Row + 1],this));

            if (CheckBoard(Position.Column + 1, Position.Row, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column + 1, Position.Row].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column + 1, Position.Row],this));

            if (CheckBoard(Position.Column + 1, Position.Row - 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column + 1, Position.Row - 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column + 1, Position.Row - 1],this));

            if (CheckBoard(Position.Column, Position.Row - 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column, Position.Row - 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column, Position.Row - 1],this));

            if (CheckBoard(Position.Column - 1, Position.Row - 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column - 1, Position.Row - 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column - 1, Position.Row - 1],this));

            if (CheckBoard(Position.Column - 1, Position.Row, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column - 1, Position.Row].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column - 1, Position.Row],this));

            if (CheckBoard(Position.Column - 1, Position.Row + 1, this, king, CellCondition.IsEmpty, CellCondition.IsOponentFigure, CellCondition.CouseToCheck) && !board[Position.Column - 1, Position.Row + 1].Is_under_attack(Color))
                res.Add(new MoveAction(board[Position.Column - 1, Position.Row + 1],this));

            if (FirstMove)
            {
                res.AddRange(Castle());
            }
            return res;
        }
        public override void Set_under_attack_cells()
        {
            if (board[Position.Column, Position.Row + 1] != null)
                board[Position.Column, Position.Row + 1].Attack(Color);

            if (board[Position.Column + 1, Position.Row + 1] != null)
                board[Position.Column + 1, Position.Row + 1].Attack(Color);

            if (board[Position.Column + 1, Position.Row] != null)
                board[Position.Column + 1, Position.Row].Attack(Color);

            if (board[Position.Column + 1, Position.Row - 1] != null)
                board[Position.Column + 1, Position.Row - 1].Attack(Color);

            if (board[Position.Column, Position.Row - 1] != null)
                board[Position.Column, Position.Row - 1].Attack(Color);

            if (board[Position.Column - 1, Position.Row - 1] != null)
                board[Position.Column - 1, Position.Row - 1].Attack(Color);

            if (board[Position.Column - 1, Position.Row] != null)
                board[Position.Column - 1, Position.Row].Attack(Color);

            if (board[Position.Column - 1, Position.Row + 1] != null)
                board[Position.Column - 1, Position.Row + 1].Attack(Color);
        }

        private void Clear_attack_cells()
        {
            if (board[Position.Column, Position.Row + 1] != null)
                board[Position.Column, Position.Row + 1].Unattack(Color);

            if (board[Position.Column + 1, Position.Row + 1] != null)
                board[Position.Column + 1, Position.Row + 1].Unattack(Color);

            if (board[Position.Column + 1, Position.Row] != null)
                board[Position.Column + 1, Position.Row].Unattack(Color);

            if (board[Position.Column + 1, Position.Row - 1] != null)
                board[Position.Column + 1, Position.Row - 1].Unattack(Color);

            if (board[Position.Column, Position.Row - 1] != null)
                board[Position.Column, Position.Row - 1].Unattack(Color);

            if (board[Position.Column - 1, Position.Row - 1] != null)
                board[Position.Column - 1, Position.Row - 1].Unattack(Color);

            if (board[Position.Column - 1, Position.Row] != null)
                board[Position.Column - 1, Position.Row].Unattack(Color);

            if (board[Position.Column - 1, Position.Row + 1] != null)
                board[Position.Column - 1, Position.Row + 1].Unattack(Color);
        }
        private List<MoveAction> Castle()
        {
            List<MoveAction> res = new List<MoveAction>();
            Cell temp = board[Position.Column + 3, Position.Row];
            if (!temp.IsEmpty && temp.ChessFigure.Type == FigureType.Rook && (temp.ChessFigure as Rook).FirstMove)
            {
                bool possible_to_castle = true; ;
                for (int i = 0; i < 4; i++)
                {
                    possible_to_castle = possible_to_castle && !board[Position.Column + i, Position.Row].Is_under_attack(Color);
                    if (i < 3 && i > 0)
                        possible_to_castle = possible_to_castle && board[Position.Column + i, Position.Row].IsEmpty;
                }
                if (possible_to_castle)
                    res.Add(new MoveAction(board[Position.Column + 2, Position.Row], this));
            }
            temp = board[Position.Column - 4, Position.Row];
            if (!temp.IsEmpty && temp.ChessFigure.Type == FigureType.Rook && (temp.ChessFigure as Rook).FirstMove)
            {
                bool possible_to_castle = true; ;
                for (int i = 0; i < 5; i++)
                {
                    possible_to_castle = possible_to_castle && !board[Position.Column - i, Position.Row].Is_under_attack(Color);
                    if (i < 4 && i > 0)
                        possible_to_castle = possible_to_castle && board[Position.Column - i, Position.Row].IsEmpty;
                }
                if (possible_to_castle)
                    res.Add(new MoveAction(board[Position.Column - 2, Position.Row], this));
            }
            return res;
        }

        public bool Is_mate(Figure f)
        {
            bool answer = true;
            Clear_attack_cells();
            #region Try to escape
            int i = Position.Column;
            int j = Position.Row;
	        if (i<7 && j>0 && (board[i + 1,j - 1].IsEmpty || board[i + 1,j - 1].IsOponentFigure(Color)) && !board[i + 1,j - 1].Is_under_attack(Color))
		        answer = false;
	        else if (i<7 && (board[i + 1,j].IsEmpty || board[i + 1,j].IsOponentFigure(Color)) && !board[i + 1,j].Is_under_attack(Color))
		        answer = false;
	        else if (i<7 && j<7 &&(board[i + 1,j + 1].IsEmpty || board[i + 1,j + 1].IsOponentFigure(Color)) && !board[i + 1,j + 1].Is_under_attack(Color))
		        answer = false;
	        else if (j<7 &&(board[i,j + 1].IsEmpty || board[i,j + 1].IsOponentFigure(Color)) && !board[i,j + 1].Is_under_attack(Color))
		        answer = false;
            else if (i > 0 && j < 7 && (board[i - 1, j + 1].IsEmpty || board[i - 1, j + 1].IsOponentFigure(Color)) && !board[i - 1, j + 1].Is_under_attack(Color))
		        answer = false;
            else if (i > 0 && (board[i - 1, j].IsEmpty || board[i - 1, j].IsOponentFigure(Color)) && !board[i - 1, j].Is_under_attack(Color))
		        answer = false;
	        else if (i>0 && j>0 &&(board[i - 1,j - 1].IsEmpty || board[i - 1,j - 1].IsOponentFigure(Color)) && !board[i - 1,j - 1].Is_under_attack(Color))
		        answer = false;
	        else if (j>0 &&(board[i,j - 1].IsEmpty || board[i,j - 1].IsOponentFigure(Color)) && !board[i,j - 1].Is_under_attack(Color))
		        answer = false;
            #endregion

            #region Try to cover king
            if ((board[Position].NumOfBlackAttackers == 1 && Color == FigureColor.White) || (board[Position].NumOfWhiteAttackers == 1 && Color == FigureColor.Black))
	        {
		        int x = 0, y = 0;
		        if (i - f.Position.Column > 0)
				        x = -1;
		        else if (i - f.Position.Column < 0) x = 1;
		        else x = 0;

		        if (j - f.Position.Row > 0)
			        y = -1;
		        else if (j - f.Position.Row < 0) y = 1;
		        else y = 0;

		        if (f.Type == FigureType.Queen)
		        {
			        if (x == 0)
			        {
				        while(board[i,j+y].IsEmpty)
				        {
                            if (board[i, j + y].Is_under_attack(f.Color))
						        answer = false;
					        if (y>0) y++;
					        else y--;
				        }
				        if (board[i,j+y].Is_under_attack(f.Color))
					        answer = false;
			        }
			        else if (y == 0)
			        {
				        while(board[i+x,j].IsEmpty)
				        {
                            if (board[i + x, j].Is_under_attack(f.Color))
						        answer = false;
					        if (x>0) x++;
					        else x--;
				        }
				        if (board[i+x,j].Is_under_attack(f.Color))
					        answer = false;
			        }
			        else
			        {
				        while(board[i+x,j+y].IsEmpty)
				        {
					        if (board[i+x,j+y].Is_under_attack(f.Color))
						        answer = false;
					        if (y>0) y++;
					        else y--;
					        if (x>0) x++;
					        else x--;
				        }
				        if (board[i+x,j+y].Is_under_attack(f.Color))
					        answer = false;
			        }
		        }
		        else if (f.Type == FigureType.Rook)
		        {
			        if (x == 0)
			        {
				        while(board[i,j+y].IsEmpty)
				        {
					        if (board[i,j+y].Is_under_attack(f.Color))
						        answer = false;
					        if (y>0) y++;
					        else y--;
				        }
				        if (board[i,j+y].Is_under_attack(f.Color))
					        answer = false;
			        }
			        else if (y == 0)
			        {
				        while(board[i+x,j].IsEmpty)
				        {
					        if (board[i+x,j].Is_under_attack(f.Color))
						        answer = false;
					        if (x>0) x++;
					        else x--;
				        }
				        if (board[i+x,j].Is_under_attack(f.Color))
					        answer = false;
			        }
		        }
		        else if (f.Type == FigureType.Bishop)
		        {
			        while(board[i+x,j+y].IsEmpty)
			        {
				        if (board[i+x,j+y].Is_under_attack(f.Color))
					        answer = false;
				        if (y>0) y++;
				        else y--;
				        if (x>0) x++;
				        else x--;
			        }
			        if (board[i+x,j+y].Is_under_attack(f.Color))
				        answer = false;
		        }
                else if (f.Type == FigureType.Horse || f.Type == FigureType.Pawn)
		        {
			        if (board[f.Position].Is_under_attack(f.Color))
				        answer = false;
		        }
            }
            #endregion
            Set_under_attack_cells();

            return answer;
        }
    }

}
