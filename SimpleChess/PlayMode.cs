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
    public partial class PlayMode : Form
    {
        public PlayMode()
        {
            InitializeComponent();
        }

        private void btnPvp_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnCvp_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
