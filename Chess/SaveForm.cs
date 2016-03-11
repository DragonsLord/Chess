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
    public partial class SaveForm : Form
    {
        public SaveForm()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (this.Name_textBox.Text != "")
            {
                FileManager.SaveGame(this.Name_textBox.Text);
                MessageBox.Show("Saved as " + Name_textBox.Text);
                this.Close();
            }
            else {
                MessageBox.Show("You need to enter a name first");
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
