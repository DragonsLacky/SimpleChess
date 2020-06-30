using SimpleChess.Pieces;
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
    public partial class PawnChange : Form
    {
        Dictionary<PictureBox, ChessPiece> choices;
        ChessPiece piece;
        public PawnChange(ChessPiece piece)
        {
            InitializeComponent();
            this.piece = piece;
            choices = new Dictionary<PictureBox, ChessPiece>();
        }
        private void initializeChoises()
        {
           
            int startPoint=60;
            int endPoint=40;
            PictureBox choicePiece=new PictureBox{ Location = new Point(startPoint, endPoint), Size = new Size(75, 75),Image=Image.FromFile(@"..\..\Images\White_Queen.png") };
            Controls.Add(choicePiece);
            choices.Add(choicePiece, new Queen('A', 1, ChessColor.WHITE, choicePiece));
            choicePiece.Click += choose_onClick;
            startPoint += 100;
            choicePiece = new PictureBox { Location = new Point(startPoint, endPoint), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Rook.png") };
            Controls.Add(choicePiece);
            choices.Add(choicePiece, new Rook('A', 1, ChessColor.WHITE, choicePiece));
            startPoint += 100;
            choicePiece.Click += choose_onClick;
            choicePiece = new PictureBox { Location = new Point(startPoint, endPoint), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Bishop.png") };
            Controls.Add(choicePiece);
            choices.Add(choicePiece, new Bishop('A', 1, ChessColor.WHITE, choicePiece));
            startPoint += 100;
            choicePiece.Click += choose_onClick;
            choicePiece = new PictureBox { Location = new Point(startPoint, endPoint), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Knight.png") };
            Controls.Add(choicePiece);
            choices.Add(choicePiece, new Knight('A', 1, ChessColor.WHITE, choicePiece));
            choicePiece.Click += choose_onClick;
        }
        private void choose_onClick(Object sender,EventArgs e) 
        {
            PictureBox chosenPiece = (PictureBox)sender;
            ((Pawn)piece).Changed = true;
            ((Pawn)piece).ChangedPiece = choices[chosenPiece];
            ((Pawn)piece).ChangedPiece.Position.X = piece.Position.X;
            ((Pawn)piece).ChangedPiece.Position.Y = piece.Position.Y;
        }
    }
}
