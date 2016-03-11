using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Figures;
using Chess.Engines;
using System.Diagnostics;

namespace Chess
{
    public struct MoveAction
    {
        public Figure @Figure; ///< figure to move
        public Cell Goal; ///< position wher figure need to move
        public int value;
        public MoveAction(Cell g, Figure f)
        {
            Figure = f;
            Goal = g;
            value = 0;
        }
        public MoveAction(Cell g, Figure f, int v)
        {
            Figure = f;
            Goal = g;
            value = v;
        }
    }
    public class ChessBot
    {
        public GameBoard CurrentSituation { get; set; }
        public string Name { get; set; }
        private Position curr_pos;
        private static List<King> kings;
        public static Dictionary<int,List<Figure>> Figures { get; set; }
        private List<MoveAction> CurrentVariants = new List<MoveAction>();
        private int color;
        public int Color {
            get { return color; }
            set { color = value; }
        }
        private int look_depth;

        public King @King
        {
            get
            {
                return kings[color];
            }

        }

        private ChessEngine bot;

        public ChessBot(FigureColor clr, ref GameBoard board)
        {
            bot = new ChessEngine();
            Name = "Computer";
            CurrentSituation = board;
            kings = new List<King>();
            Figures = new Dictionary<int, List<Figure>>();
            Figures[0] = new List<Figure>();
            Figures[1] = new List<Figure>();
            for (int i = 0; i < 8; i++)
            {
                Figures[0].Add(board[i, 0].ChessFigure);
                Figures[0].Add(board[i, 1].ChessFigure);
                Figures[1].Add(board[i, 6].ChessFigure);
                Figures[1].Add(board[i, 7].ChessFigure);
            }
            kings.Add(board[4, 0].ChessFigure as King);
            kings.Add(board[4, 7].ChessFigure as King);
            if (clr == FigureColor.White)
            {
                color = 0;
            }
            else { color = 1;}
            look_depth = 3;
            //Pawn temp;
            //for (int i = 0; i < 8; i++)
            //{
            //    temp = board[i, row].ChessFigure as Pawn;
            //    temp.Transform += (() => { CurrentSituation[curr_pos].ChessFigure = new Queen(clr, curr_pos); });
            //}
            bot.Run();
        }

        private MoveAction CalculateValue()
        {
            MoveAction act = new MoveAction();
            int value = 0;
            foreach (Figure f in Figures[color])
            {
                value += 2 * f.Value;
                value += f.EvaluatePosition();
            }
            foreach (Figure f in Figures[1-color])
            {
                value -= f.Value;
            }
            if (kings[color].IsCheck)
                value -= 100;
            if (kings[1 - color].IsCheck)
                value += 50;
            act.value = value;
            return act;
        }

        static public List<MoveAction> GetAllPossibleMoves(int color) 
        {
            List<MoveAction> moves = new List<MoveAction>();
            foreach (Figure f in (from figure in Figures[color] where figure.Value != 0 select figure))
                moves.AddRange(f.GetPossibleMoves(kings[color]));
            return moves;
        }

        private List<MoveAction> SortMoves(List<MoveAction> moves)
        {
            MoveAction back = new MoveAction();
            Figure removed_figure;
            for (int i = 0; i < moves.Count; i++)
            {
                back.Figure = moves[i].Figure;
                back.Goal = CurrentSituation[moves[i].Figure.Position];
                removed_figure = DoMove(moves[i]);
                moves[i] = new MoveAction(moves[i].Goal, moves[i].Figure, CalculateValue().value);
                UndoMove(back, removed_figure);
            }
            List<MoveAction> res = new List<MoveAction>();
            for (int i = 0; i < Math.Min(5,moves.Count); i++)
            {
                MoveAction best = new MoveAction(null,null,int.MinValue);
                for (int j = 0; j < moves.Count; j++)
                {
                    if (best.value < moves[j].value)
                    {
                        best = moves[j];
                    }
                }
                res.Add(best);
                moves.Remove(best);
            }
            res.AddRange(moves);
            //var result = from move in moves orderby move.value descending select move;
            return res;
        }

        private Figure DoMove(MoveAction move)
        {
            Figure removed = null;
            if (!move.Goal.IsEmpty)
            {
                removed = move.Goal.ChessFigure;
                removed.Value = 0;
            }
            move.Figure.Move(move.Goal.Position);
            return removed;
        }
        private void UndoMove(MoveAction move, Figure removed)
        {
            if (move.Figure.Type == FigureType.King)
            {
                int delta = move.Figure.Position.Column - move.Goal.Position.Column;
                switch (delta)
                {
                    case 2:
                        {
                            Rook rook = CurrentSituation[5, move.Figure.Position.Row].ChessFigure as Rook;
                            rook.Move(new Position(7, move.Figure.Position.Row));
                            rook.FirstMove = true;
                            rook.FirstMove = true;
                            break;
                        }
                    case -2:
                        {
                            Rook rook = CurrentSituation[3, move.Figure.Position.Row].ChessFigure as Rook;
                            rook.Move(new Position(0, move.Figure.Position.Row));
                            rook.FirstMove = true;
                            rook.FirstMove = true;
                            break;
                        }
                }
            }
            move.Figure.Move(move.Goal.Position);
            if (removed != null)
            {
                CurrentSituation[removed.Position].ChessFigure = removed;
                CurrentSituation.SituationChanged += removed.Set_under_attack_cells;
                switch (removed.Type)
                {
                    case FigureType.Bishop: { removed.Value = 500; break; }
                    case FigureType.Horse: { removed.Value = 500; break; }
                    case FigureType.Pawn: { removed.Value = 300; break; }
                    case FigureType.Queen: { removed.Value = 900; break; }
                    case FigureType.Rook: { removed.Value = 800; break; }
                }
            }
            if (move.Figure is IFirstMove)
            {
                (move.Figure as IFirstMove).FirstMove = true;
                (move.Figure as IFirstMove).FirstMove = true;
            }
            CurrentSituation.SituationUpdate();
        }
        /// <summary>
        /// alphabeta algorythm
        /// </summary>
        /// <param name="alpha">best value for maximizer</param>
        /// <param name="beta">best value for minimizer</param>
        /// <param name="depth">current depth</param>
        /// <param name="color">color of current figures to move</param>
        /// <returns>best acction</returns>
        private MoveAction alpha_beta(int alpha, int beta, int depth, int color)
        {
            List<MoveAction> moves = GetAllPossibleMoves(color);
            if (moves.Count == 0 || depth >= look_depth)
            {
                Debug.WriteLine(depth);
                return CalculateValue();
            }
            else
            {
                moves = SortMoves(moves);
                MoveAction back = new MoveAction();
                MoveAction temp = new MoveAction();
                MoveAction best = new MoveAction();
                if (color == this.color)
                {
                    best.value = int.MinValue;
                    int max = alpha;
                    Figure removed_figure;
                    for (int i = 0; i < moves.Count && max < beta; i++)  // > alpha
                    {
                        back.Figure = moves[i].Figure;
                        back.Goal = CurrentSituation[moves[i].Figure.Position];
                        removed_figure = DoMove(moves[i]);
                        temp = alpha_beta(max, beta, depth + 1, 1 - color);
                        if (temp.value > max)
                        {
                            max = temp.value;
                            best = moves[i];
                            best.value = temp.value;
                        }
                        if (kings[1 - color].IsCheck && kings[1 - color].Is_mate(moves[i].Figure))
                        {
                            best = moves[i];
                            best.value = int.MaxValue;
                            //break;
                        }
                        else if (moves[i].Figure.Type == FigureType.Pawn && (moves[i].Goal.Position.Row == 0 || moves[i].Goal.Position.Row == 7))
                        {
                            best = moves[i];
                            best.value = int.MaxValue-10;
                        }
                        UndoMove(back, removed_figure);
                    }
                    return best;
                }
                else
                {
                    best.value = int.MaxValue;
                    int min = beta;
                    Figure removed_figure;
                    for (int i = 0; i < moves.Count && min > alpha; i++)
                    {
                        back.Figure = moves[i].Figure;
                        back.Goal = CurrentSituation[moves[i].Figure.Position];
                        removed_figure = DoMove(moves[i]);
                        temp = alpha_beta(alpha, min, depth + 1, 1 - color);
                        if (temp.value < min)
                        {
                            min = temp.value;
                            best = moves[i];
                            best.value = temp.value;
                        }
                        if (kings[1 - color].IsCheck && kings[1 - color].Is_mate(moves[i].Figure))
                        {
                            best = moves[i];
                            best.value = int.MinValue;
                            //break;
                        }
                        else if (moves[i].Figure.Type == FigureType.Pawn && (moves[i].Goal.Position.Row == 0 || moves[i].Goal.Position.Row == 7))
                        {
                            best = moves[i];
                            best.value = int.MinValue + 10;
                        }
                        UndoMove(back, removed_figure);
                    }
                    return best;
                }
            }
        }

        public void Control()
        {
            if (FileManager.options["ChessEngine"] == "Default")
            {
                string animation = FileManager.options["Animation"];
                FileManager.options["Animation"] = "False";
                MoveAction optimal_move = alpha_beta(int.MinValue, int.MaxValue, 0, this.color);
                FileManager.options["Animation"] = animation;
                curr_pos = optimal_move.Goal.Position;
                FileManager.WriteToLog(optimal_move.Figure.Color.ToString() + " " + optimal_move.Figure.Type.ToString() + ": " + CurrentSituation[optimal_move.Figure.Position].Name + optimal_move.Goal.Name);
                optimal_move.Figure.Move(optimal_move.Goal.Position);
            }
            else {
                string FENboard = ChessEngine.ToFEN(CurrentSituation, (FigureColor)Color);
                string best_move = bot.FindBestMove(FENboard);
                Figure f = CurrentSituation[best_move[0], (int)best_move[1] - 49].ChessFigure;
                FileManager.WriteToLog(f.Color.ToString() + " " + f.Type.ToString() + ": " + best_move);
                CurrentSituation[best_move[0], (int)best_move[1] - 49].ChessFigure.Move(CurrentSituation[best_move[2], (int)best_move[3] - 49].Position);
            }
        }

        public void EndProcess()
        {
            if (FileManager.options["ChessEngine"] == "StockFish")
                bot.End();
        }
    }
}
