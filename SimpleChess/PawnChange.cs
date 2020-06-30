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
            initializeChoices();
        }
        private void InitializeSingleChoice(int xpos, int ypos, ChessColor color, PieceType type)
        {
            PictureBox choicePiece = null;
            if (color == ChessColor.WHITE)
            {
               switch (type)
                {
                    case PieceType.PAWN: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Pawn.png") }; piece = new Pawn(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.ROOK: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Rook.png") }; piece = new Rook(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.BISHOP: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Bishop.png") }; piece = new Bishop(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.KING: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_King.png") }; piece = new King(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.KNIGHT: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Knight.png") }; piece = new Knight(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.QUEEN: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\White_Queen.png") }; piece = new Queen(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                }
            }
            else
            {
                switch (type)
                {
                    case PieceType.PAWN: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_Pawn.png") }; piece = new Pawn(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.ROOK: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_Rook.png") }; piece = new Rook(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.BISHOP: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_Bishop.png") }; piece = new Bishop(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.KING: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_King.png") }; piece = new King(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.KNIGHT: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_Knight.png") }; piece = new Knight(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                    case PieceType.QUEEN: choicePiece = new PictureBox { Location = new Point(xpos, ypos), Size = new Size(75, 75), Image = Image.FromFile(@"..\..\Images\Black_Queen.png") }; piece = new Queen(piece.Position.X, piece.Position.Y, color, choicePiece); break;
                }
            }
            Controls.Add(choicePiece);
            choicePiece.Click += choose_onClick;
        }
        private void initializeChoices()
        {
            int xpos = 60;
            int ypos = 40;
            InitializeSingleChoice(xpos, ypos, piece.Color, PieceType.KNIGHT);
            xpos += 100;
            InitializeSingleChoice(xpos, ypos, piece.Color, PieceType.BISHOP);
            xpos += 100;
            InitializeSingleChoice(xpos, ypos, piece.Color, PieceType.ROOK);
            xpos += 100;
            InitializeSingleChoice(xpos, ypos, piece.Color, PieceType.QUEEN);
        }
        private void choose_onClick(Object sender,EventArgs e) 
        {
            PictureBox chosenPiece = (PictureBox)sender;
            ((Pawn)piece).Changed = true;
            ((Pawn)piece).ChangedPiece = choices[chosenPiece];
            piece.Piece = choices[chosenPiece].Piece;
        }

        private void PawnChange_Load(object sender, EventArgs e)
        {

        }
    }
}
