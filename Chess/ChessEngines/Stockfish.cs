using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using Chess.Figures;

namespace Chess.Engines
{
    public class ChessEngine
    {
        private ProcessStartInfo processStartInfo;
        private Process stockfish_process;
        private StreamWriter inputWriter; 
        private StreamReader outputReader;
        private StreamReader errorReader;
        private bool processStarted;

        public ChessEngine()
        {
            processStartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\stockfish 7 32bit.exe");
            processStartInfo.UseShellExecute = false;
            processStartInfo.ErrorDialog = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            stockfish_process = new Process();
            stockfish_process.StartInfo = processStartInfo;
        }

        public void Run()
        {
            processStarted = stockfish_process.Start();
            inputWriter = stockfish_process.StandardInput;
            outputReader = stockfish_process.StandardOutput;
            errorReader = stockfish_process.StandardError;
            outputReader.ReadLine();
            inputWriter.WriteLine("uci");
            while (outputReader.ReadLine() != "uciok") ;
            inputWriter.WriteLine("ucinewgame");
        }

        public string FindBestMove(string board)
        {
            if (processStarted)
            {
                inputWriter.WriteLine("position fen " + board);
                inputWriter.WriteLine("go");
                string best_move = outputReader.ReadLine();
                while (best_move[0] != 'b')
                    best_move = outputReader.ReadLine();
                string[] words = best_move.Split();
                return words[1];
            }
            else return null;
        }

        public void End()
        {
            stockfish_process.Kill();
            processStarted = false;
        }

        public static string GetFigureCharMark(Figure f)
        {
            string res = null;
            switch (f.Type)
            {
                case FigureType.Pawn: { res = "p";break; }
                case FigureType.Bishop: { res = "b";break; }
                case FigureType.Horse: { res = "n";break; }
                case FigureType.Rook: { res = "r";break; }
                case FigureType.Queen: { res = "q";break; }
                case FigureType.King: { res = "k";break; }
            }
            if (f.Color == FigureColor.White)
                res = res.ToUpper();
            return res;
        }
        /// <summary>
        /// get FEN string of board
        /// </summary>
        /// <param name="board">board need to transform</param>
        /// <param name="turn">wich turn to move</param>
        /// <returns>FEN string</returns>
        public static string ToFEN(GameBoard board, FigureColor turn)
        {
            StringBuilder result = new StringBuilder();
            int empty_cells_count = 0;
            for (int i = 7; i >= 0; i--)
            {
                empty_cells_count = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i].IsEmpty)
                    {
                        empty_cells_count++;
                        if (j == 7)
                        {
                            result.Append(empty_cells_count.ToString());
                            empty_cells_count = 0;
                        }
                    }
                    else
                    {
                        if (empty_cells_count > 0)
                            result.Append(empty_cells_count.ToString());
                        result.Append(GetFigureCharMark(board[j, i].ChessFigure));
                        empty_cells_count = 0;
                    }
                }
                if (i > 0)
                    result.Append("/");
            }
            if (turn == FigureColor.White)
                result.Append(" w");
            else result.Append(" b");
            result.Append(" ");
            if (board[4, 0].ChessFigure is King && (board[4, 0].ChessFigure as King).FirstMove)
            {
                if (board[7, 0].ChessFigure is Rook && (board[7, 0].ChessFigure as Rook).FirstMove)
                    result.Append("K");
                if (board[0, 0].ChessFigure is Rook && (board[0, 0].ChessFigure as Rook).FirstMove)
                    result.Append("Q");
            }
            if (board[4, 7].ChessFigure is King && (board[4, 7].ChessFigure as King).FirstMove)
            {
                if (board[7, 7].ChessFigure is Rook && (board[7, 7].ChessFigure as Rook).FirstMove)
                    result.Append("k");
                if (board[0, 7].ChessFigure is Rook && (board[0, 7].ChessFigure as Rook).FirstMove)
                    result.Append("q");
            }
            return result.ToString();
        }
    }
}
