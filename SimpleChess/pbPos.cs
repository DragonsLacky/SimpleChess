using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess
{
    class pbPos
    {
        public PictureBox pictureBox { get; set; }
        public ChessPosition position { get; set; }
        public pbPos(PictureBox pb, ChessPosition pos)
        {
            pictureBox = pb;
            position = pos;
        }
    }
}
