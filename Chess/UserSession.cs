using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Chess.Figures;

namespace Chess
{
    public enum ControlAction{ Up, Down, Left, Right, Choose};

    public class UserSession
    {
        private GameBoard board;
        public GameBoard Board
        {
            get { return board; }
        }

        private Player player1;
        private Player player2;
        private ChessBot computer;

        private Panel info_panel;
        public Panel InfoPanel
        {
            get { return info_panel; }
        }

        private bool game_session;
        public bool IsGameSession
        {
            get
            {
                return game_session;
            }
        }

        private bool blocked_control = false;
        public bool BlockedControl
        {
            get
            {
                return blocked_control;
            }
        }

        private int turn;
        private bool two_players;
        private Action<Cell, ControlAction> ctrl_method;

        public UserSession()
        {
            info_panel = new Panel();
            Player.InfoPanel = info_panel;
            board = new GameBoard();
            Player.Board = board;
            Figure.Board = board;
            game_session = false;
            turn = 0;
            two_players = false;
        }

        public void Mouse(int x, int y)
        {
            if (!Player.Transforming)
            {
                Cell temp = board.GetCellByCoordinates(x, y);
                ControlAction act;
                act = ControlAction.Choose;
                ctrl_method(temp, act);
            }
        }
        public void Keyboard(Keys key)
        {
            if (Player.Transforming)
            {
                FigureColor clr = board[Player.current].ChessFigure.Color;
                Player.Transforming = false;
                switch (key)
                {
                    case Keys.D1: { break; }
                    case Keys.D2: { board[Player.current].ChessFigure = new Bishop(clr, Player.current); break; }
                    case Keys.D3: { board[Player.current].ChessFigure = new Horse(clr, Player.current); break; }
                    case Keys.D4: { board[Player.current].ChessFigure = new Rook(clr, Player.current); break; }
                    case Keys.D5: { board[Player.current].ChessFigure = new Queen(clr, Player.current); break; }
                    default: { Player.Transforming = true; break; }
                }
                if (!Player.Transforming)
                {
                    turn = 3 - turn;
                    Program.MainWindow.DrawZone.Refresh();
                }
            }
            else
                switch (key)
                {
                    case Keys.Left: { ctrl_method(null, ControlAction.Left);break; }
                    case Keys.Right: { ctrl_method(null, ControlAction.Right); break; }
                    case Keys.Up: { ctrl_method(null, ControlAction.Up); break; }
                    case Keys.Down: { ctrl_method(null, ControlAction.Down); break;}
                    case Keys.Space: { ctrl_method(null, ControlAction.Choose); break; }
                    default: { break; }
                }
        }
        private void Control_bots(Cell pos, ControlAction action)
        {
            while (true)
            {
                computer.Color = 0;
                FileManager.options["ChessEngine"] = "Default";
                computer.Control();
                info_panel.ClearData();
                info_panel.ShowLog();
                Program.MainWindow.info_paper.Refresh();
                computer.Color = 1;
                if (computer.King.IsCheck && ChessBot.GetAllPossibleMoves(computer.Color).Count == 0)
                {
                    info_panel.ClearData();
                    info_panel.Write("Mate!!!", 3);
                    info_panel.Write("White Win", 5);
                    game_session = false;
                    Program.MainWindow.info_paper.Refresh();
                    return;
                }
                Program.MainWindow.DrawZone.Refresh();
                FileManager.options["ChessEngine"] = "StockFish";
                //Thread.Sleep(2000);
                computer.Control();
                info_panel.ClearData();
                info_panel.ShowLog();
                Program.MainWindow.info_paper.Refresh();
                computer.Color = 0;
                if (computer.King.IsCheck && ChessBot.GetAllPossibleMoves(1 - computer.Color).Count == 0)
                {
                    info_panel.ClearData();
                    info_panel.Write("Mate!!!", 3);
                    info_panel.Write("Black Win", 5);
                    game_session = false;
                    Program.MainWindow.info_paper.Refresh();
                    return;
                }
                Program.MainWindow.DrawZone.Refresh();
                //Thread.Sleep(2000);
            }
        }
        private void Control_1(Cell pos, ControlAction action)
        {
            Task comp_task = new Task(() =>
            {
                computer.Control();
                if (ChessBot.GetAllPossibleMoves(1 - computer.Color).Count == 0)
                {
                    if (player1.King.IsCheck)
                    {
                        info_panel.Write("Mate!!!", 3);
                        info_panel.Write("You Lose", 5);
                    }
                    else { info_panel.Write("Pate!!!", 4); }
                    game_session = false;
                    EndState();
                }
                info_panel.ClearData();
                info_panel.Write(player1.Name, 0);
                info_panel.ShowLog();
                //Program.MainWindow.AllowContol();
                blocked_control = false;
                info_panel.Redraw();
                Program.MainWindow.DrawOnBoard();
                turn = 3 - turn;
            });
            Task player_task = new Task(() =>
            {
                try
                {
                    if (player1.Control(pos, action))
                    {
                        if (ChessBot.GetAllPossibleMoves(computer.Color).Count == 0)
                        {
                            if (computer.King.IsCheck)
                            {
                                info_panel.Write("Mate!!!", 3);
                                info_panel.Write("Congradulation " + player1.Name, 5);
                                info_panel.Write("You win!!!", 6);
                            }
                            else { info_panel.Write("Pate!!!", 4); }
                            info_panel.Redraw();
                            game_session = false;
                            EndState();
                        }
                        else
                        {

                            info_panel.ClearData();
                            info_panel.Write(computer.Name, 0);
                            info_panel.ShowLog();
                            info_panel.Redraw();
                            Program.MainWindow.DrawOnBoard();
                            blocked_control = true;
                            //Program.MainWindow.ForbidControl();
                            turn = 3 - turn;
                        }
                    }

                }
                catch (NullReferenceException) { };
            });

            if (turn == 1)
            {
                player_task.Start();
                player_task.Wait();
            }
            if (turn == 2)
            {
                comp_task.Start();
                comp_task.Wait();
            }
            //try
            //{
            //    if (turn == 1)
            //    {
            //        info_panel.ClearData();
            //        info_panel.Write(player1.Name, 0);
            //        if (player1.Control(pos, action))
            //        {
            //            if (computer.King.IsCheck && ChessBot.GetAllPossibleMoves(computer.Color).Count == 0)
            //            {
            //                info_panel.Write("Mate!!!", 3);
            //                info_panel.Write("Congradulation " + player1.Name, 5);
            //                info_panel.Write("You win!!!", 6);
            //                game_session = false;
            //            }
            //            else
            //            {

            //                info_panel.ClearData();
            //                info_panel.Write(computer.Name, 0);
            //                info_panel.Redraw();
            //                Program.MainWindow.DrawZone.Refresh();
            //                blocked_control = true;
            //                Program.MainWindow.ForbidControl();
            //                turn = 2;
            //                comp_task.Start();
            //            }
            //        }
            //        info_panel.Redraw();
            //    }
            //}
            //catch (NullReferenceException ex)
            //{
            //    MessageBox.Show(ex.TargetSite.ToString());
            //}
        }
        private void Control_2(Cell pos, ControlAction action)
        {
            try
            {
                if (turn == 1)
                {
                    info_panel.ClearData();
                    info_panel.Write(player1.Name, 0);
                    if (player1.Control(pos,action))
                    {
                        if (ChessBot.GetAllPossibleMoves((int)player2.King.Color).Count == 0)
                        {
                            if (player2.King.IsCheck)
                            {
                                info_panel.Write("Mate!!!", 3);
                                info_panel.Write("Congradulation " + player1.Name, 5);
                                info_panel.Write("You win!!!", 6);
                            }
                            else {
                                info_panel.Write("Pate!!!", 4);
                            }
                            game_session = false;
                            Program.MainWindow.DrawZone.Refresh();
                            EndState();
                        }
                        else
                        {
                            turn = 2;
                            info_panel.Write(player2.Name, 0);
                            info_panel.ShowLog();
                        }
                    }
                    else
                        info_panel.ShowLog();
                    info_panel.Redraw();
                }
                else if (turn == 2)
                {
                    info_panel.ClearData();
                    info_panel.Write(player2.Name, 0);
                    if (player2.Control(pos,action))
                    {
                        if (ChessBot.GetAllPossibleMoves((int)player1.King.Color).Count == 0)
                        {
                            if (player1.King.IsCheck)
                            {
                                info_panel.Write("Mate!!!", 3);
                                info_panel.Write("Congradulation " + player2.Name, 5);
                                info_panel.Write("You win!!!", 6);
                            }
                            else { info_panel.Write("Pate!!!", 4); }
                            game_session = false;
                            Program.MainWindow.DrawZone.Refresh();
                            EndState();
                        }
                        else
                        {
                            turn = 1;
                            info_panel.Write(player1.Name, 0);
                            info_panel.ShowLog();
                        }
                    }
                    else
                        info_panel.ShowLog();
                    info_panel.Redraw();
                }
            }
            catch (NullReferenceException)
            { }
        }

        public void StartGame(bool two_players)
        {
            this.two_players = two_players;
            FileManager.StartNewLog();
            game_session = true;
            turn = 1;
            board.Open();
            #region Figure Creating
            for (int i = 0; i < 8; i++)
            {
                board[i, 1].ChessFigure = new Pawn(FigureColor.White, new Position { Row = 1, Column = i });
                board[i, 6].ChessFigure = new Pawn(FigureColor.Black, new Position { Row = 6, Column = i });
            }

            board[0, 0].ChessFigure = new Rook(FigureColor.White, new Position { Row = 0, Column = 0 });
            board[7, 0].ChessFigure = new Rook(FigureColor.White, new Position { Row = 0, Column = 7 });
            board[0, 7].ChessFigure = new Rook(FigureColor.Black, new Position { Row = 7, Column = 0 });
            board[7, 7].ChessFigure = new Rook(FigureColor.Black, new Position { Row = 7, Column = 7 });

            board[1, 0].ChessFigure = new Horse(FigureColor.White, new Position { Row = 0, Column = 1 });
            board[6, 0].ChessFigure = new Horse(FigureColor.White, new Position { Row = 0, Column = 6 });
            board[1, 7].ChessFigure = new Horse(FigureColor.Black, new Position { Row = 7, Column = 1 });
            board[6, 7].ChessFigure = new Horse(FigureColor.Black, new Position { Row = 7, Column = 6 });

            board[2, 0].ChessFigure = new Bishop(FigureColor.White, new Position { Row = 0, Column = 2 });
            board[5, 0].ChessFigure = new Bishop(FigureColor.White, new Position { Row = 0, Column = 5 });
            board[2, 7].ChessFigure = new Bishop(FigureColor.Black, new Position { Row = 7, Column = 2 });
            board[5, 7].ChessFigure = new Bishop(FigureColor.Black, new Position { Row = 7, Column = 5 });

            King WhiteKing = new King(FigureColor.White, new Position { Row = 0, Column = 4 });
            King BlackKing = new King(FigureColor.Black, new Position { Row = 7, Column = 4 });

            board[3, 0].ChessFigure = new Queen(FigureColor.White, new Position { Row = 0, Column = 3 });
            board[4, 0].ChessFigure = WhiteKing;
            board[3, 7].ChessFigure = new Queen(FigureColor.Black, new Position { Row = 7, Column = 3 });
            board[4, 7].ChessFigure = BlackKing;
            #endregion

            #region Players Creation
            player1 = new Player(FigureColor.White, ref WhiteKing);
            player1.Name = "Player 1";
            if (two_players)
            {
                player2 = new Player(FigureColor.Black, ref BlackKing);
                player2.Name = "Player 2";
            }
            //else
            {
                computer = new ChessBot(FigureColor.Black, ref board);
            }
            #endregion
            if (two_players)
                ctrl_method = new Action<Cell, ControlAction>(Control_2);
            else
                ctrl_method = new Action<Cell, ControlAction>(Control_1);

            Board.SituationUpdate();
            info_panel.Write(player1.Name, 0, true);
            info_panel.Redraw();
        }

        /// <summary>
        /// Sugest to save game
        /// </summary>
        private void EndState()
        {
            SaveForm save = new SaveForm();
            save.StartPosition = Program.MainWindow.StartPosition;
            //save.TopMost = true;
            save.ShowDialog();  
        }

        public void EndGame()
        {
            board.Close();
            info_panel.ClearData();
            player1 = null;
            player2 = null;
            computer.EndProcess();
            game_session = false;
            //MessageBox.Show(System.GC.GetTotalMemory(false).ToString());
            // Forced call of Garbage Collector to delete figures and cells from memmory
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //MessageBox.Show(System.GC.GetTotalMemory(true).ToString());
        }
    }
}
