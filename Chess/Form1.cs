using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Window : Form
    {
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        /*protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (e.Shift)
                    {

                    }
                    else
                    {
                    }
                    break;
            }
        }*/

        public Window()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
        }
        public UserSession Session;
        private const float FontSizePart = 0.03F;
        private void Window_Load(object sender, EventArgs e)
        {

            FileManager.Read_settings_file();
            if (FileManager.options["Fullscreen"] == "True")
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.fullscreen_checkBox.Checked = true;
                float font_size = this.Size.Height * FontSizePart;
                this.game_name.Font = new System.Drawing.Font("Buxton Sketch", font_size, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
            Session = new UserSession();
            this.Cursor = new Cursor("MuCursor.cur");
        } 
        private void glow_text(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            temp.ForeColor = Color.Gold;
        }
        private void unglow_text(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            temp.ForeColor = Color.LightGray;
        }

        #region Main Menu Buttons
        private void play_button_Click(object sender, EventArgs e)
        {
            this.button1.Text = "One Player";
            this.button2.Text = "Two Players";
            this.button3.Text = "Saved Games";
            this.button4.Text = "Constructor";
            this.button5.Show();
            this.button1.Click -= play_button_Click;
            this.button1.Click += one_player_button_Click;
            this.button2.Click -= settings_button_Click;
            this.button2.Click += two_players_button_Click;
            this.button3.Click -= about_button_Click;
            this.button3.Click += saved_games_button_Click;
            this.button4.Click -= exit_button_Click;
            this.button4.Click += constructor_button_Click;
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            this.button1.Hide();
            this.button2.Hide();
            this.button3.Hide();
            this.button4.Text = "Save current srttings";
            this.button4.Click -= exit_button_Click;
            this.button4.Click += save_changes_button_Click;
            this.button5.Show();
            this.fullscreen_checkBox.Show();
            this.animation_checkBox.Show();
            if (FileManager.options["Animation"] == "True")
                this.animation_checkBox.Checked = true;
            this.board_position_label.Show();
            this.board_position_comboBox.Show();
            this.board_position_comboBox.Text = FileManager.options["BoardPosition"];
            this.ChessEngineBox.Show();
            if (FileManager.options["ChessEngine"] == "StockFish")
                this.ChessEngineBox.Checked = true;
        }

        private void about_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Yevhenii Serdiuk\nEmail: zheka2797@gmail.com\nTelephone: +380634769167");
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Play Menu Buttons
        private void one_player_button_Click(object sender, EventArgs e)
        {
            this.DrawZone.Location = Point.Ceiling(Session.Board.BoardPosition);
            this.DrawZone.Size = Size.Ceiling(Session.Board.BoardSize);
            this.DrawZone.Show();
            //this.Enabled = false;
            this.info_paper.Show();
            this.button1.Hide();
            this.button1.Enabled = false;
            this.button2.Hide();
            this.button2.Enabled = false;
            this.button3.Hide();
            this.button3.Enabled = false;
            this.button4.Hide();
            this.button4.Enabled = false;
            this.button5.Hide();
            this.button5.Enabled = false;
            Session.StartGame(false);
            this.Window_Resize(sender, e);
        }
        private void two_players_button_Click(object sender, EventArgs e)
        {
            this.DrawZone.Location = Point.Ceiling(Session.Board.BoardPosition);
            this.DrawZone.Size = Size.Ceiling(Session.Board.BoardSize);
            this.DrawZone.Show();
            //this.Enabled = false;
            this.info_paper.Show();
            this.button1.Hide();
            this.button1.Enabled = false;
            this.button2.Hide();
            this.button2.Enabled = false;
            this.button3.Hide();
            this.button3.Enabled = false;
            this.button4.Hide();
            this.button4.Enabled = false;
            this.button5.Hide();
            this.button5.Enabled = false;
            Session.StartGame(true);
            this.Window_Resize(sender, e);
        }
        private void saved_games_button_Click(object sender, EventArgs e)
        {
            this.info_paper.Size = Session.InfoPanel.Size;
        }
        private void constructor_button_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Settings

        private void fullscreen_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox temp = sender as CheckBox;
            if (temp.Checked)
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                FileManager.Save_option("Fullscreen", "True");
            }
            else
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                FileManager.Save_option("Fullscreen", "False");
            }
        }
        private void animation_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox temp = sender as CheckBox;
            if (temp.Checked)
                FileManager.Save_option("Animation", "True");
            else
                FileManager.Save_option("Animation", "False");
        }
        private void board_position_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox option = sender as ComboBox;
            FileManager.Save_option("BoardPosition", option.Text);
        }
        private void ChessEngineBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChessEngineBox.Checked)
                FileManager.Save_option("ChessEngine", "StockFish");
            else FileManager.Save_option("ChessEngine", "Default");
        }
        private void save_changes_button_Click(object sender, EventArgs e)
        {
            FileManager.Write_settings_file();
            this.button1.Show();
            this.button2.Show();
            this.button3.Show();
            this.button4.Text = "Exit";
            this.button4.Size = new System.Drawing.Size(172, 49);
            this.button4.Click -= save_changes_button_Click;
            this.button4.Click += exit_button_Click;
            this.fullscreen_checkBox.Hide();
            this.animation_checkBox.Hide();
            this.board_position_label.Hide();
            this.board_position_comboBox.Hide();
            this.ChessEngineBox.Hide();
            this.button5.Hide();
        }

        #endregion

        private void back_button_Click(object sender, EventArgs e)
        {
            //return from play menu
            if (button1.Visible)
            {
                this.button1.Text = "Play";
                this.button2.Text = "Settings";
                this.button3.Text = "About";
                this.button4.Text = "Exit";
                this.button1.Click += play_button_Click;
                this.button1.Click -= one_player_button_Click;
                this.button2.Click += settings_button_Click;
                this.button2.Click -= two_players_button_Click;
                this.button3.Click += about_button_Click;
                this.button3.Click -= saved_games_button_Click;
                this.button4.Click += exit_button_Click;
                this.button4.Click -= constructor_button_Click;
                this.button5.Hide();
            }
            else {
                this.button1.Show();
                this.button2.Show();
                this.button3.Show();
                this.button4.Text = "Exit";
                this.button4.Size = new System.Drawing.Size(172, 49);
                this.button4.Click -= save_changes_button_Click;
                this.button4.Click += exit_button_Click;
                this.fullscreen_checkBox.Hide();
                this.animation_checkBox.Hide();
                this.board_position_label.Hide();
                this.board_position_comboBox.Hide();
                this.ChessEngineBox.Hide();
                this.button5.Hide();
            }
        }

        private void DrawZone_Paint(object sender, PaintEventArgs e)
        {
            Session.Board.Draw(e.Graphics);
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            float font_size = this.Size.Height * FontSizePart;
            this.game_name.Font = new System.Drawing.Font("Buxton Sketch", font_size, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            if (Session.IsGameSession)
            {
                Session.Board.Reshape();
                this.DrawZone.Location = Point.Ceiling(Session.Board.BoardPosition);
                this.DrawZone.Size = Size.Ceiling(Session.Board.BoardSize);
                this.DrawZone.Update();
            }
            Session.InfoPanel.Reshape();
        }

        private void info_papper_Paint(object sender, PaintEventArgs e)
        {
            Session.InfoPanel.Draw(e.Graphics);
        }

        #region Controls
        private void DrawZone_MouseClick(object sender, MouseEventArgs e)
        {
            if (Session.IsGameSession)
            {
                Session.Mouse(e.X, e.Y);
                if (!Session.BlockedControl)
                    DrawZone.Invalidate();
            }
        }

        public void DrawOnBoard()
        {
            DrawZone.Invalidate();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !button4.Visible)  ////// FIX!!!!!
            {
                this.button1.Show();
                this.button1.Enabled = true;
                this.button2.Show();
                this.button2.Enabled = true;
                this.button3.Show();
                this.button3.Enabled = true;
                this.button4.Show();
                this.button4.Enabled = true;
                this.button5.Show();
                this.button5.Enabled = true;
                this.DrawZone.Hide();
                this.info_paper.Hide();
                Session.EndGame();
            }
            if (Session.IsGameSession)
            {
                Session.Keyboard(e.KeyCode);
                if (!Session.BlockedControl)
                    DrawZone.Invalidate();
            }
        }

        public void AllowContol()
        {
            //this.DrawZone.MouseClick += DrawZone_MouseClick;
            //this.KeyDown += Window_KeyDown;
        }

        public void ForbidControl()
        {
            //this.DrawZone.MouseClick -= DrawZone_MouseClick;
            //this.KeyDown -= Window_KeyDown;
        }
        #endregion
    }
}
