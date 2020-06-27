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
        Dictionary<PictureBox, ChessPosition> positionOnBoard;
        Dictionary<PictureBox, ChessPiece> BoardPieces;
        Dictionary<char, Dictionary<int, positionInfo>> piecePositions;
        List<ChessPiece> white_pieces;
        List<ChessPiece> black_pieces;
        PictureBox selected;
        List<ChessPosition> selectedMovable;
        ChessColor turn = ChessColor.WHITE;
        public Form1()
        {
            InitializeComponent();
            Board = new Dictionary<char, Dictionary<int, PictureBox>>();
            PieceImages = new Dictionary<PieceType, Dictionary<Color, Image>>();
            positionOnBoard = new Dictionary<PictureBox, ChessPosition>();
            BoardPieces = new Dictionary<PictureBox, ChessPiece>();
            piecePositions = new Dictionary<char, Dictionary<int, positionInfo>>();
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


        private void Piece_MouseClick(Object sender, MouseEventArgs e)
        {
            
            if (selected != null)
            {
                DeselectBoard();
            }
            PictureBox Piece = (PictureBox)sender;
            selected = Piece;
            if (e.Button != MouseButtons.Left)
                return;
            selectedMovable = BoardPieces[selected].getValidMoves(white_pieces, black_pieces);
            foreach (ChessPosition pos in selectedMovable)
            {
                if(piecePositions[pos.X][pos.Y].ocupied && piecePositions[pos.X][pos.Y].piece.Color != BoardPieces[selected].Color)
                {
                    piecePositions[pos.X][pos.Y].piece.Piece.BackColor = Color.PaleVioletRed;
                }
                else
                {
                    Board[pos.X][pos.Y].BackColor = Color.AliceBlue;
                }
            }
            
            Piece.BackColor = Color.AliceBlue;
        }
        
        private void Board_MouseClick(Object sender, MouseEventArgs e)
        {
            if(selected != null)
            {
                //if (/*BoardPieces[selected].checkValidMove(positionOnBoard[(PictureBox)sender])*/)
                //{

                //}
                DeselectBoard();
            }
        }

        private void DeselectBoard()
        {

            if (BoardPieces[selected].Position.X % 2 != 0)
            {
                if (BoardPieces[selected].Position.Y % 2 != 0)
                {
                    selected.BackColor = Color.White;
                }
                else
                {
                    selected.BackColor = Color.Black;
                }
            }
            else
            {
                if (BoardPieces[selected].Position.Y % 2 != 0)
                {
                    selected.BackColor = Color.Black;
                }
                else
                {
                    selected.BackColor = Color.White;
                }
            }
            foreach (var item in Board.Values)
            {
                foreach (var picbox in item.Values)
                {
                    if (positionOnBoard[picbox].X % 2 != 0)
                    {
                        if (positionOnBoard[picbox].Y % 2 != 0)
                        {
                            picbox.BackColor = Color.White;
                        }
                        else
                        {
                            picbox.BackColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (positionOnBoard[picbox].Y % 2 != 0)
                        {
                            picbox.BackColor = Color.Black;
                        }
                        else
                        {
                            picbox.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void InitializePieces()
        {
            InitializePawns();
            InitializeRooks();
            InitializeKnight();
            InitializeBishop();
            InitializeKing();
            InitializeQueen();
        }

        private void InitializePawns()
        {
            int y = 2;
            for (int i = 'A'; i <= 'H'; i++)
            {
                PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.PAWN][Color.White] };
                ChessPiece piece = new Pawn((char)i, y, ChessColor.WHITE, chessPiece);
                white_pieces.Add(piece);
                BoardPieces.Add(chessPiece, piece);
                piecePositions[(char)i][y].piece = piece;
                piecePositions[(char)i][y].ocupied = true;
                chessPiece.MouseClick += Piece_MouseClick;
                Controls.Add(chessPiece);
                chessPiece.BringToFront();
            }
            y = 7;
            for (int i = 'A'; i <= 'H'; i++)
            {
                PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.PAWN][Color.Black] };
                ChessPiece piece = new Pawn((char)i, y, ChessColor.BLACK, chessPiece);
                black_pieces.Add(piece);
                BoardPieces.Add(chessPiece, piece);
                piecePositions[(char)i][y].piece = piece;
                piecePositions[(char)i][y].ocupied = true;
                chessPiece.MouseClick += Piece_MouseClick;
                Controls.Add(chessPiece);
                chessPiece.BringToFront();
            }
        }
        private void InitializeRooks()
        {
            int y = 1;
            int i = 'A';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.White] };
            ChessPiece piece = new Rook((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'H';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.White] };
            piece = new Rook((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

            y = 8;
            i = 'A';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.Black] };
            piece = new Rook((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'H';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.ROOK][Color.Black] };
            piece = new Rook((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }

        private void InitializeKnight()
        {
            int y = 1;
            int i = 'B';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KNIGHT][Color.White] };
            ChessPiece piece = new Knight((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'G';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KNIGHT][Color.White] };
            piece = new Knight((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

            y = 8;
            i = 'B';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KNIGHT][Color.Black] };
            piece = new Knight((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'G';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KNIGHT][Color.Black] };
            piece = new Knight((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }

        private void InitializeBishop()
        {
            int y = 1;
            int i = 'C';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.BISHOP][Color.White] };
            ChessPiece piece = new Bishop((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'F';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.BISHOP][Color.White] };
            piece = new Bishop((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

            y = 8;
            i = 'C';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.BISHOP][Color.Black] };
            piece = new Bishop((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            i = 'F';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.BISHOP][Color.Black] };
            piece = new Bishop((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }
        private void InitializeKing()
        {
            int y = 1;
            int i = 'E';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KING][Color.White] };
            ChessPiece piece = new King((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();

            y = 8;
            i = 'E';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.KING][Color.Black] };
            piece = new King((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }
        private void InitializeQueen()
        {
            int y = 1;
            int i = 'D';
            PictureBox chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.QUEEN][Color.White] };
            ChessPiece piece = new Queen((char)i, y, ChessColor.WHITE, chessPiece);
            white_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
            
            y = 8;
            i = 'D';
            chessPiece = new PictureBox { Location = Board[(char)i][y].Location, Size = Board[(char)i][y].Size, BackColor = Board[(char)i][y].BackColor, BorderStyle = Board[(char)i][y].BorderStyle, Image = PieceImages[PieceType.QUEEN][Color.Black] };
            piece = new Queen((char)i, y, ChessColor.BLACK, chessPiece);
            black_pieces.Add(piece);
            BoardPieces.Add(chessPiece, piece);
            piecePositions[(char)i][y].piece = piece;
            piecePositions[(char)i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }

        private void DrawBoard()
        {
            int nextPosX = 60;
            int nextPosY = 80;
            for (int i = 8; i >= 1; i--)
            {
                for (int j = 'A'; j <= 'H'; j++)
                {
                    if(!Board.ContainsKey((char)j))
                    {
                        Board.Add((char)j, new Dictionary<int, PictureBox>());
                    }
                    if(!piecePositions.ContainsKey((char)j))
                    {
                        piecePositions.Add((char)j, new Dictionary<int, positionInfo>());
                    }
                    piecePositions[(char)j].Add(i, new positionInfo(null, false));
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
                    positionOnBoard.Add(Board[(char)j][i], new ChessPosition((char)j,i));
                    nextPosX += 75;
                }
                nextPosX = 60;
                nextPosY += 75;
            }
        }
    }
}
