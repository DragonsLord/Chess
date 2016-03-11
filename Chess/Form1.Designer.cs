namespace Chess
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.game_name = new System.Windows.Forms.Label();
            this.fullscreen_checkBox = new System.Windows.Forms.CheckBox();
            this.DrawZone = new System.Windows.Forms.PictureBox();
            this.board_position_comboBox = new System.Windows.Forms.ComboBox();
            this.board_position_label = new System.Windows.Forms.Label();
            this.animation_checkBox = new System.Windows.Forms.CheckBox();
            this.info_paper = new System.Windows.Forms.PictureBox();
            this.ChessEngineBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DrawZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.info_paper)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.SandyBrown;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.CausesValidation = false;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.SeaShell;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Chocolate;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Chocolate;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.LightGray;
            this.button1.Location = new System.Drawing.Point(331, 152);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.play_button_Click);
            this.button1.Enter += new System.EventHandler(this.glow_text);
            this.button1.Leave += new System.EventHandler(this.unglow_text);
            this.button1.MouseEnter += new System.EventHandler(this.glow_text);
            this.button1.MouseLeave += new System.EventHandler(this.unglow_text);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.SandyBrown;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.CausesValidation = false;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.SeaShell;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Chocolate;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Chocolate;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.ForeColor = System.Drawing.Color.LightGray;
            this.button2.Location = new System.Drawing.Point(331, 213);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Settings";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.settings_button_Click);
            this.button2.Enter += new System.EventHandler(this.glow_text);
            this.button2.Leave += new System.EventHandler(this.unglow_text);
            this.button2.MouseEnter += new System.EventHandler(this.glow_text);
            this.button2.MouseLeave += new System.EventHandler(this.unglow_text);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.SandyBrown;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.CausesValidation = false;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.SeaShell;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.ForeColor = System.Drawing.Color.LightGray;
            this.button3.Location = new System.Drawing.Point(331, 273);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(172, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "About";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.about_button_Click);
            this.button3.Enter += new System.EventHandler(this.glow_text);
            this.button3.Leave += new System.EventHandler(this.unglow_text);
            this.button3.MouseEnter += new System.EventHandler(this.glow_text);
            this.button3.MouseLeave += new System.EventHandler(this.unglow_text);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button4.AutoSize = true;
            this.button4.BackColor = System.Drawing.Color.SandyBrown;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.CausesValidation = false;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.SeaShell;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button4.ForeColor = System.Drawing.Color.LightGray;
            this.button4.Location = new System.Drawing.Point(331, 334);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(172, 49);
            this.button4.TabIndex = 3;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.exit_button_Click);
            this.button4.Enter += new System.EventHandler(this.glow_text);
            this.button4.Leave += new System.EventHandler(this.unglow_text);
            this.button4.MouseEnter += new System.EventHandler(this.glow_text);
            this.button4.MouseLeave += new System.EventHandler(this.unglow_text);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button5.AutoSize = true;
            this.button5.BackColor = System.Drawing.Color.SandyBrown;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.CausesValidation = false;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.SeaShell;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button5.ForeColor = System.Drawing.Color.LightGray;
            this.button5.Location = new System.Drawing.Point(331, 396);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(172, 49);
            this.button5.TabIndex = 4;
            this.button5.Text = "Back";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.back_button_Click);
            this.button5.Enter += new System.EventHandler(this.glow_text);
            this.button5.Leave += new System.EventHandler(this.unglow_text);
            this.button5.MouseEnter += new System.EventHandler(this.glow_text);
            this.button5.MouseLeave += new System.EventHandler(this.unglow_text);
            // 
            // game_name
            // 
            this.game_name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.game_name.AutoSize = true;
            this.game_name.BackColor = System.Drawing.Color.Transparent;
            this.game_name.Font = new System.Drawing.Font("Buxton Sketch", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.game_name.Location = new System.Drawing.Point(274, 9);
            this.game_name.Margin = new System.Windows.Forms.Padding(0);
            this.game_name.Name = "game_name";
            this.game_name.Size = new System.Drawing.Size(328, 33);
            this.game_name.TabIndex = 5;
            this.game_name.Text = "Amazing Chess by DragonLord";
            this.game_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fullscreen_checkBox
            // 
            this.fullscreen_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fullscreen_checkBox.AutoSize = true;
            this.fullscreen_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.fullscreen_checkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fullscreen_checkBox.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fullscreen_checkBox.Location = new System.Drawing.Point(331, 64);
            this.fullscreen_checkBox.Name = "fullscreen_checkBox";
            this.fullscreen_checkBox.Size = new System.Drawing.Size(132, 33);
            this.fullscreen_checkBox.TabIndex = 7;
            this.fullscreen_checkBox.Text = "FullScreen";
            this.fullscreen_checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fullscreen_checkBox.UseVisualStyleBackColor = false;
            this.fullscreen_checkBox.Visible = false;
            this.fullscreen_checkBox.CheckedChanged += new System.EventHandler(this.fullscreen_checkBox_CheckedChanged);
            // 
            // DrawZone
            // 
            this.DrawZone.BackColor = System.Drawing.Color.Transparent;
            this.DrawZone.BackgroundImage = global::Chess.Properties.Resources.chessboard;
            this.DrawZone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DrawZone.Location = new System.Drawing.Point(83, 464);
            this.DrawZone.Margin = new System.Windows.Forms.Padding(0);
            this.DrawZone.Name = "DrawZone";
            this.DrawZone.Size = new System.Drawing.Size(598, 598);
            this.DrawZone.TabIndex = 8;
            this.DrawZone.TabStop = false;
            this.DrawZone.Visible = false;
            this.DrawZone.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawZone_Paint);
            this.DrawZone.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawZone_MouseClick);
            // 
            // board_position_comboBox
            // 
            this.board_position_comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.board_position_comboBox.BackColor = System.Drawing.Color.PeachPuff;
            this.board_position_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.board_position_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.board_position_comboBox.Font = new System.Drawing.Font("Buxton Sketch", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.board_position_comboBox.FormattingEnabled = true;
            this.board_position_comboBox.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.board_position_comboBox.Location = new System.Drawing.Point(418, 142);
            this.board_position_comboBox.Name = "board_position_comboBox";
            this.board_position_comboBox.Size = new System.Drawing.Size(121, 28);
            this.board_position_comboBox.TabIndex = 9;
            this.board_position_comboBox.Visible = false;
            this.board_position_comboBox.SelectedIndexChanged += new System.EventHandler(this.board_position_comboBox_SelectedIndexChanged);
            // 
            // board_position_label
            // 
            this.board_position_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.board_position_label.AutoSize = true;
            this.board_position_label.BackColor = System.Drawing.Color.Transparent;
            this.board_position_label.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.board_position_label.Location = new System.Drawing.Point(243, 139);
            this.board_position_label.Name = "board_position_label";
            this.board_position_label.Size = new System.Drawing.Size(159, 29);
            this.board_position_label.TabIndex = 10;
            this.board_position_label.Text = "Board Position";
            this.board_position_label.Visible = false;
            // 
            // animation_checkBox
            // 
            this.animation_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.animation_checkBox.AutoSize = true;
            this.animation_checkBox.BackColor = System.Drawing.Color.Transparent;
            this.animation_checkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.animation_checkBox.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.animation_checkBox.Location = new System.Drawing.Point(339, 103);
            this.animation_checkBox.Name = "animation_checkBox";
            this.animation_checkBox.Size = new System.Drawing.Size(124, 33);
            this.animation_checkBox.TabIndex = 11;
            this.animation_checkBox.Text = "Animation";
            this.animation_checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.animation_checkBox.UseVisualStyleBackColor = false;
            this.animation_checkBox.Visible = false;
            this.animation_checkBox.CheckedChanged += new System.EventHandler(this.animation_checkBox_CheckedChanged);
            // 
            // info_paper
            // 
            this.info_paper.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.info_paper.BackColor = System.Drawing.Color.Transparent;
            this.info_paper.BackgroundImage = global::Chess.Properties.Resources.paper_transparent;
            this.info_paper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.info_paper.Location = new System.Drawing.Point(558, 64);
            this.info_paper.Name = "info_paper";
            this.info_paper.Size = new System.Drawing.Size(123, 198);
            this.info_paper.TabIndex = 12;
            this.info_paper.TabStop = false;
            this.info_paper.Visible = false;
            this.info_paper.Paint += new System.Windows.Forms.PaintEventHandler(this.info_papper_Paint);
            // 
            // ChessEngineBox
            // 
            this.ChessEngineBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChessEngineBox.AutoSize = true;
            this.ChessEngineBox.BackColor = System.Drawing.Color.Transparent;
            this.ChessEngineBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChessEngineBox.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChessEngineBox.Location = new System.Drawing.Point(248, 177);
            this.ChessEngineBox.Name = "ChessEngineBox";
            this.ChessEngineBox.Size = new System.Drawing.Size(273, 33);
            this.ChessEngineBox.TabIndex = 13;
            this.ChessEngineBox.Text = "Use StockFish ChessBot";
            this.ChessEngineBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChessEngineBox.UseVisualStyleBackColor = false;
            this.ChessEngineBox.Visible = false;
            this.ChessEngineBox.CheckedChanged += new System.EventHandler(this.ChessEngineBox_CheckedChanged);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ChessEngineBox);
            this.Controls.Add(this.info_paper);
            this.Controls.Add(this.animation_checkBox);
            this.Controls.Add(this.board_position_label);
            this.Controls.Add(this.board_position_comboBox);
            this.Controls.Add(this.DrawZone);
            this.Controls.Add(this.fullscreen_checkBox);
            this.Controls.Add(this.game_name);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Buxton Sketch", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.TransparencyKey = System.Drawing.SystemColors.HotTrack;
            this.Load += new System.EventHandler(this.Window_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Window_KeyDown);
            this.Resize += new System.EventHandler(this.Window_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DrawZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.info_paper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label game_name;
        private System.Windows.Forms.CheckBox fullscreen_checkBox;
        public System.Windows.Forms.PictureBox DrawZone;
        private System.Windows.Forms.ComboBox board_position_comboBox;
        private System.Windows.Forms.Label board_position_label;
        private System.Windows.Forms.CheckBox animation_checkBox;
        public System.Windows.Forms.PictureBox info_paper;
        private System.Windows.Forms.CheckBox ChessEngineBox;
    }
}

