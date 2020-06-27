using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleChess.Pieces;

namespace SimpleChess
{
    enum PieceType { KING, QUEEN, PAWN, BISHOP, ROOK, KNIGHT }
    public partial class Form1 : Form
    {
        Dictionary<char, Dictionary<int, PictureBox>> Board;
        Dictionary<PieceType, Dictionary<Color, Image>> PieceImages;
        Dictionary<PictureBox, ChessPiece> BoardPieces;
        List<ChessPiece> white_pieces;
        List<ChessPiece> black_pieces;
        
        public Form1()
        {
            InitializeComponent();
            Board = new Dictionary<char, Dictionary<int, PictureBox>>();
            PieceImages = new Dictionary<PieceType, Dictionary<Color, Image>>();
            white_pieces = new List<ChessPiece>();
            black_pieces = new List<ChessPiece>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadImages();
            DrawBoard();
            InitializePieces();
        }

        private void loadImages()
        {
            List<PieceType> types = new List<PieceType>();
            types.Add(PieceType.KING);
            types.Add(PieceType.QUEEN);
            types.Add(PieceType.BISHOP);
            types.Add(PieceType.KNIGHT);
            types.Add(PieceType.ROOK);
            types.Add(PieceType.PAWN);

            foreach (PieceType type in types)
            {
                PieceImages.Add(type, new Dictionary<Color, Image>());
                PieceImages[type].Add(Color.White, null);
                PieceImages[type].Add(Color.Black, null);
            }

            PieceImages[PieceType.KING][Color.White] = Image.FromFile(@"..\..\Images\White_King.png");
            PieceImages[PieceType.KING][Color.Black] = Image.FromFile(@"..\..\Images\Black_King.png");
            PieceImages[PieceType.QUEEN][Color.White] = Image.FromFile(@"..\..\Images\White_Queen.png");
            PieceImages[PieceType.QUEEN][Color.Black] = Image.FromFile(@"..\..\Images\Black_Queen.png");
            PieceImages[PieceType.BISHOP][Color.White] = Image.FromFile(@"..\..\Images\White_Bishop.png");
            PieceImages[PieceType.BISHOP][Color.Black] = Image.FromFile(@"..\..\Images\Black_Bishop.png");
            PieceImages[PieceType.KNIGHT][Color.White] = Image.FromFile(@"..\..\Images\White_Knight.png");
            PieceImages[PieceType.KNIGHT][Color.Black] = Image.FromFile(@"..\..\Images\Black_Knight.png");
            PieceImages[PieceType.ROOK][Color.White] = Image.FromFile(@"..\..\Images\White_Rook.png");
            PieceImages[PieceType.ROOK][Color.Black] = Image.FromFile(@"..\..\Images\Black_Rook.png");
            PieceImages[PieceType.PAWN][Color.White] = Image.FromFile(@"..\..\Images\White_Pawn.png");
            PieceImages[PieceType.PAWN][Color.Black] = Image.FromFile(@"..\..\Images\Black_Pawn.png");
        }


        public void Piece_MouseClick(Object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            PictureBox Piece = (PictureBox)sender;
            Piece.BackColor = Color.AliceBlue;
        }

        public void Board_MouseClick(Object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            PictureBox Piece = (PictureBox)sender;
            Piece.BackColor = Color.AliceBlue;
        }

        public void InitializePieces()
        {
            InitializePawns();
            InitializeRook();
        }

        public void InitializePawns()
        {
            int y = 2;
            for (int i = 'A'; i <= 'H'; i++)
            {
                PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.PAWN][Color.White] };
                white_pieces.Add(new Pawn((char)i, y, ChessColor.WHITE, chessPiece));
                chessPiece.MouseClick += Piece_MouseClick;
                Controls.Add(chessPiece);
                chessPiece.BringToFront();
            }
            y = 7;
            for (int i = 'A'; i <= 'H'; i++)
            {
                PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.PAWN][Color.Black] };
                black_pieces.Add(new Pawn((char)i, y, ChessColor.BLACK, chessPiece));
                chessPiece.MouseClick += Piece_MouseClick;
                Controls.Add(chessPiece);
                chessPiece.BringToFront();
            }
        }
        public void InitializeRook()
        {
            int y = 1;
            int i = 'A';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.White] };
            white_pieces.Add(new Pawn((char)i, y, ChessColor.WHITE, chessPiece));
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'H';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.White] };
            white_pieces.Add(new Pawn((char)i, y, ChessColor.WHITE, chessPiece));
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

            y = 8;
            i = 'A';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.Black] };
            white_pieces.Add(new Pawn((char)i, y, ChessColor.WHITE, chessPiece));
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'H';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.Black] };
            white_pieces.Add(new Pawn((char)i, y, ChessColor.BLACK, chessPiece));
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

        }

        public void DrawBoard()
        {
            int nextPosX = 40;
            int nextPosY = 80;
            for (int i = 8; i >= 1; i--)
            {
                for (int j = 'A'; j <= 'H'; j++)
                {
                    if(!Board.ContainsKey((char)j))
                    {
                        Board.Add((char)j, new Dictionary<int, PictureBox>());
                    }
                    Board[(char)j].Add(i, new PictureBox { Location = new Point(nextPosX, nextPosY), Size = new Size(75, 75) });
                    Board[(char)j][i].BorderStyle = BorderStyle.Fixed3D;
                    Board[(char)j][i].MouseClick += Board_MouseClick;
                    if (j % 2 != 0)
                    {
                        if(i % 2 != 0)
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                    }
                    Controls.Add(Board[(char)j][i]);
                    nextPosX += 75;
                }
                nextPosX = 40;
                nextPosY += 75;
            }
           // PictureBox piece = new PictureBox { Location = Board['A'][1].Location, Size = Board['A'][1].Size, Image = PieceImages[PieceType.PAWN][Color.White], BackColor = Board['A'][1].BackColor,  BorderStyle = Board['A'][1].BorderStyle };
            //Controls.Add(piece);
            //piece.BringToFront();

            //board.Add((char)('A'+1),new Dictionary<int, PictureBox>());
            //board['B'].Add(1, new PictureBox { Location = new Point(40, 300), Size = new Size(75, 75) });
            //Controls.Add(board['B'][1]);
            //Controls.Add(chesspiece);
            //board['B'][1].BackColor = Color.White;
            //board['B'][1].BorderStyle = BorderStyle.Fixed3D;

            //board['B'][1].BackColor = ;
        }
    }
}
