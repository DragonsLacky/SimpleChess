using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess
{
    public partial class Victory : Form
    {
        public Victory(ChessColor color)
        {
            InitializeComponent();
            lblVictory.Text = string.Format("{0} Wins", color == ChessColor.WHITE? "Black" : "White");
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
