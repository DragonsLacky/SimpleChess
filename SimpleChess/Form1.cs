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
    public enum PieceType { KING, QUEEN, PAWN, BISHOP, ROOK, KNIGHT }
    public partial class fSimpleChess : Form
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
        List<Moves> MovesMade;
        ChessColor turn = ChessColor.WHITE;
        Dictionary<ChessColor,King> Kings = new Dictionary<ChessColor, King>();
        private static readonly int turn_time = 60;
        private int timeElapsed = 0;
        public fSimpleChess()
        {
            InitializeComponent();
            Board = new Dictionary<char, Dictionary<int, PictureBox>>();
            PieceImages = new Dictionary<PieceType, Dictionary<Color, Image>>();
            positionOnBoard = new Dictionary<PictureBox, ChessPosition>();
            BoardPieces = new Dictionary<PictureBox, ChessPiece>();
            piecePositions = new Dictionary<char, Dictionary<int, positionInfo>>();
            white_pieces = new List<ChessPiece>();
            black_pieces = new List<ChessPiece>();
            MovesMade = new List<Moves>();
            Moves move = new Moves(null, 'A', 1, 'A', 1);
            move.init = true;
            MovesMade.Add(move);
            loadMoves();
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
            if(turn != BoardPieces[(PictureBox)sender].Color)
            {
                TakeEnemyPiece((PictureBox)sender);
                return;
            }
            PictureBox Piece = (PictureBox)sender;
            
            if (Piece == selected)
            {
                DeselectBoard();
                selected = null;
                return;
            }
            selected = Piece;
            if (e.Button != MouseButtons.Left)
                return;
            if (Kings[BoardPieces[selected].Color].CheckMate(white_pieces, black_pieces, piecePositions))
            {
                btnSurrender.Visible = true;
            }
            selectedMovable = BoardPieces[selected].getValidMoves(white_pieces, black_pieces, piecePositions);
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
            if(selected != null && ((PictureBox)sender) != selected)
            {
                if (BoardPieces[selected].Type != PieceType.KING && Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                {
                    bool isPawnAtStart = false;
                    positionInfo startPos = piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y];
                    positionInfo endPos = piecePositions[positionOnBoard[(PictureBox)sender].X][positionOnBoard[(PictureBox)sender].Y];

                    startPos.ocupied = false;
                    startPos.piece = null;
                    endPos.ocupied = true;
                    endPos.piece = BoardPieces[selected];
                    if (BoardPieces[selected].Type == PieceType.PAWN)
                    {
                        isPawnAtStart = ((Pawn)BoardPieces[selected]).startingPos;
                    }
                    BoardPieces[selected].MovePiece(endPos.piece.Position.X, endPos.piece.Position.Y);
                    
                    if(Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                    {
                        startPos.ocupied = true;
                        startPos.piece = BoardPieces[selected];
                        endPos.ocupied = false;
                        endPos.piece = null;
                        BoardPieces[selected].MovePiece(startPos.piece.Position.X, startPos.piece.Position.Y);
                        if (BoardPieces[selected].Type == PieceType.PAWN && isPawnAtStart)
                        {
                            ((Pawn)BoardPieces[selected]).startingPos = true;
                        }
                        DeselectBoard();
                        return;
                    }
                }
                if (BoardPieces[selected].checkValidMove(positionOnBoard[(PictureBox)sender], white_pieces, black_pieces, piecePositions))
                {
                    
                    positionInfo startPos = piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y];
                    positionInfo endPos = piecePositions[positionOnBoard[(PictureBox)sender].X][positionOnBoard[(PictureBox)sender].Y];
                    bool isPawnAtStart = false;
                    if (BoardPieces[selected].Type == PieceType.PAWN)
                    {
                        isPawnAtStart = ((Pawn)BoardPieces[selected]).startingPos;
                    }
                    startPos.ocupied = false;
                    startPos.piece = null;
                    endPos.ocupied = true;
                    endPos.piece = BoardPieces[selected];
                    BoardPieces[selected].MovePiece(endPos.piece.Position.X, endPos.piece.Position.Y);
                    if (BoardPieces[selected].Type != PieceType.KING && Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                    {
                        startPos.ocupied = true;
                        startPos.piece = BoardPieces[selected];
                        endPos.ocupied = false;
                        endPos.piece = null;
                        BoardPieces[selected].MovePiece(startPos.piece.Position.X, startPos.piece.Position.Y);
                        if (BoardPieces[selected].Type == PieceType.PAWN)
                        {
                            ((Pawn)BoardPieces[selected]).startingPos = isPawnAtStart;
                        }
                        DeselectBoard();
                        return;
                    }
                    MovesMade.Add(new Moves(BoardPieces[selected], BoardPieces[selected].Position.X, BoardPieces[selected].Position.Y,
                        positionOnBoard[(PictureBox)sender].X, positionOnBoard[(PictureBox)sender].Y));
                    loadMoves();
                    BoardPieces[selected].MovePiece(positionOnBoard[(PictureBox)sender].X, positionOnBoard[(PictureBox)sender].Y);
                    if (BoardPieces[selected].Type == PieceType.PAWN && BoardPieces[selected].Color == ChessColor.WHITE && BoardPieces[selected].Position.Y == 8 ||
                        BoardPieces[selected].Type == PieceType.PAWN && BoardPieces[selected].Color == ChessColor.BLACK && BoardPieces[selected].Position.Y == 1)
                    {
                        PawnChange changePawnForm = new PawnChange(BoardPieces[selected]);
                        if (changePawnForm.ShowDialog() == DialogResult.OK)
                        {
                            piecePositions[positionOnBoard[(PictureBox)sender].X][positionOnBoard[(PictureBox)sender].Y].ocupied = false;
                            piecePositions[positionOnBoard[(PictureBox)sender].X][positionOnBoard[(PictureBox)sender].Y].piece = null;
                            if(BoardPieces[selected].Color == ChessColor.WHITE)
                            {
                                white_pieces.Remove(BoardPieces[selected]);
                            }
                            else
                            {
                                black_pieces.Remove(BoardPieces[selected]);
                            }
                            Controls.Remove(selected);
                            BoardPieces.Remove(selected);
                            InitializeSinglePiece(changePawnForm.chosen.Position.X, changePawnForm.chosen.Position.Y, changePawnForm.chosen.Type, changePawnForm.chosen.Color, changePawnForm.chosen.Color == ChessColor.WHITE ? Color.White : Color.Black);
                            selected = piecePositions[changePawnForm.chosen.Position.X][changePawnForm.chosen.Position.Y].piece.Piece;
                        }
                    }
                    if(Kings[BoardPieces[selected].Color == ChessColor.WHITE ? ChessColor.BLACK : ChessColor.WHITE].Check(white_pieces, black_pieces, piecePositions))
                    {
                        if(Kings[BoardPieces[selected].Color == ChessColor.WHITE ? ChessColor.BLACK : ChessColor.WHITE].victoryCondition(white_pieces, black_pieces, piecePositions))
                        {
                            Victory victoryForm = new Victory(BoardPieces[selected].Color == ChessColor.WHITE ? ChessColor.BLACK : ChessColor.WHITE);
                            if (victoryForm.ShowDialog() == DialogResult.OK)
                            {
                                NewGame();
                                return;
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                    AnimateMovement((PictureBox)sender);
                    timeElapsed = 0;
                    turn = turn == ChessColor.BLACK ? ChessColor.WHITE : ChessColor.BLACK;
                }
                DeselectBoard();
            }
            selected = null;

        }

        private void TakeEnemyPiece(PictureBox sender)
        {
            bool pass = false;
            if (selected != null && (sender) != selected)
            {
                if (BoardPieces[selected].Type != PieceType.KING && Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                {
                    bool isPawnAtStart = false;
                    positionInfo startPos = piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y];
                    positionInfo endPos = piecePositions[BoardPieces[sender].Position.X][BoardPieces[sender].Position.Y];
                    if (BoardPieces[sender].Color == ChessColor.WHITE)
                    {
                        white_pieces.Remove(BoardPieces[sender]);
                    }
                    else
                    {
                        black_pieces.Remove(BoardPieces[sender]);
                    }
                    startPos.ocupied = false;
                    startPos.piece = null;
                    endPos.ocupied = true;
                    endPos.piece = BoardPieces[selected];
                    if (BoardPieces[selected].Type == PieceType.PAWN)
                    {
                        isPawnAtStart = ((Pawn)BoardPieces[selected]).startingPos;
                    }
                    BoardPieces[selected].MovePiece(endPos.piece.Position.X, endPos.piece.Position.Y);

                    if (Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                    {
                        startPos.ocupied = true;
                        startPos.piece = BoardPieces[selected];
                        endPos.ocupied = true;
                        endPos.piece = BoardPieces[sender];
                        BoardPieces[selected].MovePiece(startPos.piece.Position.X, startPos.piece.Position.Y);
                        if (BoardPieces[sender].Color == ChessColor.WHITE)
                        {
                            white_pieces.Remove(BoardPieces[sender]);
                        }
                        else
                        {
                            black_pieces.Remove(BoardPieces[sender]);
                        }
                        if (BoardPieces[selected].Type == PieceType.PAWN && isPawnAtStart)
                        {
                            ((Pawn)BoardPieces[selected]).startingPos = true;
                        }
                        DeselectBoard();
                        return;
                    }else
                    pass = true;
                }
                if (pass || BoardPieces[selected].checkValidMove(BoardPieces[sender].Position, white_pieces, black_pieces, piecePositions))
                {
                    if(BoardPieces[selected].Color != BoardPieces[sender].Color)
                    {
                        positionInfo startPos = piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y];
                        positionInfo endPos = piecePositions[BoardPieces[sender].Position.X][BoardPieces[sender].Position.Y];
                        bool isPawnAtStart = false;
                        if (BoardPieces[selected].Type == PieceType.PAWN)
                        {
                            isPawnAtStart = ((Pawn)BoardPieces[selected]).startingPos;
                        }
                        if (BoardPieces[sender].Color == ChessColor.WHITE)
                        {
                            white_pieces.Remove(BoardPieces[sender]);
                        }
                        else
                        {
                            black_pieces.Remove(BoardPieces[sender]);
                        }
                        startPos.ocupied = false;
                        startPos.piece = null;
                        endPos.ocupied = true;
                        endPos.piece = BoardPieces[selected];
                        BoardPieces[selected].MovePiece(endPos.piece.Position.X, endPos.piece.Position.Y);
                        if (BoardPieces[selected].Type != PieceType.KING && Kings[BoardPieces[selected].Color].Check(white_pieces, black_pieces, piecePositions))
                        {
                            startPos.ocupied = true;
                            startPos.piece = BoardPieces[selected];
                            endPos.ocupied = true;
                            endPos.piece =  BoardPieces[sender];
                            BoardPieces[selected].MovePiece(startPos.piece.Position.X, startPos.piece.Position.Y);
                            if (BoardPieces[sender].Color == ChessColor.WHITE)
                            {
                                white_pieces.Add(BoardPieces[sender]);
                            }
                            else
                            {
                                black_pieces.Add(BoardPieces[sender]);
                            }
                            if (BoardPieces[selected].Type == PieceType.PAWN)
                            {
                                ((Pawn)BoardPieces[selected]).startingPos = isPawnAtStart;
                            }
                            DeselectBoard();
                            return;
                        }
                        Moves move = new Moves(BoardPieces[selected], BoardPieces[selected].Position.X, BoardPieces[selected].Position.Y, BoardPieces[sender].Position.X, BoardPieces[sender].Position.Y);
                        move.TakenType = BoardPieces[sender].getType();
                        move.PieceTaken = true;
                        MovesMade.Add(move);
                        loadMoves();
                        if (BoardPieces[sender].Color == ChessColor.WHITE)
                        {
                            white_pieces.Remove(BoardPieces[sender]);
                        }
                        else
                        {
                            black_pieces.Remove(BoardPieces[sender]);
                        }
                        piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y].ocupied = false;
                        piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y].piece = null;
                        piecePositions[BoardPieces[sender].Position.X][BoardPieces[sender].Position.Y].ocupied = true;
                        piecePositions[BoardPieces[sender].Position.X][BoardPieces[sender].Position.Y].piece = BoardPieces[selected];
                        BoardPieces[selected].MovePiece(BoardPieces[sender].Position.X, BoardPieces[sender].Position.Y);
                        Controls.Remove(sender);
                        BoardPieces.Remove(sender);
                        AnimateMovement(sender);
                        if (BoardPieces[selected].Type == PieceType.PAWN && BoardPieces[selected].Color == ChessColor.WHITE && BoardPieces[selected].Position.Y == 8 ||
                        BoardPieces[selected].Type == PieceType.PAWN && BoardPieces[selected].Color == ChessColor.BLACK && BoardPieces[selected].Position.Y == 1)
                        {
                            PawnChange changePawnForm = new PawnChange(BoardPieces[selected]);
                            if (changePawnForm.ShowDialog() == DialogResult.OK)
                            {
                                piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y].ocupied = false;
                                piecePositions[BoardPieces[selected].Position.X][BoardPieces[selected].Position.Y].piece = null;
                                if (BoardPieces[selected].Color == ChessColor.WHITE)
                                {
                                    white_pieces.Remove(BoardPieces[selected]);
                                }
                                else
                                {
                                    black_pieces.Remove(BoardPieces[selected]);
                                }
                                Controls.Remove(selected);
                                BoardPieces.Remove(selected);
                                InitializeSinglePiece(changePawnForm.chosen.Position.X, changePawnForm.chosen.Position.Y, changePawnForm.chosen.Type, changePawnForm.chosen.Color, changePawnForm.chosen.Color == ChessColor.WHITE ? Color.White : Color.Black);
                                selected = piecePositions[changePawnForm.chosen.Position.X][changePawnForm.chosen.Position.Y].piece.Piece;
                            }
                        }
                        if (Kings[BoardPieces[selected].Color == ChessColor.WHITE ? ChessColor.BLACK : ChessColor.WHITE].Check(white_pieces, black_pieces, piecePositions))
                        {
                            if (Kings[BoardPieces[selected].Color == ChessColor.WHITE ? ChessColor.BLACK : ChessColor.WHITE].victoryCondition(white_pieces, black_pieces, piecePositions))
                            {
                                Victory victoryForm = new Victory(turn == ChessColor.BLACK ? ChessColor.WHITE : ChessColor.BLACK);
                                if (victoryForm.ShowDialog() == DialogResult.OK)
                                {
                                    NewGame();
                                    return;
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        timeElapsed = 0;
                        turn = turn == ChessColor.BLACK ? ChessColor.WHITE : ChessColor.BLACK;
                    }
                }
                DeselectBoard();
            }
        }

        private void AnimateMovement(PictureBox sender)
        {
            Color color = selected.BackColor;
            selected.BackColor = Color.Transparent;
            while (selected.Location.X != sender.Location.X || selected.Location.Y != ((PictureBox)sender).Location.Y)
            {
                if (selected.Location.X != sender.Location.X && selected.Location.Y != ((PictureBox)sender).Location.Y)
                {
                    if (selected.Location.X < sender.Location.X && selected.Location.Y < sender.Location.Y)
                    {
                        selected.Location = new Point(selected.Location.X + 1, selected.Location.Y + 1);
                    }
                    else if (selected.Location.X < sender.Location.X && selected.Location.Y > sender.Location.Y)
                    {
                        selected.Location = new Point(selected.Location.X + 1, selected.Location.Y - 1);
                    }
                    else if (selected.Location.X > sender.Location.X && selected.Location.Y < sender.Location.Y)
                    {
                        selected.Location = new Point(selected.Location.X - 1, selected.Location.Y + 1);
                    }
                    else if (selected.Location.X > sender.Location.X && selected.Location.Y > sender.Location.Y)
                    {
                        selected.Location = new Point(selected.Location.X - 1, selected.Location.Y - 1);
                    }
                }
                else if (selected.Location.X != sender.Location.X)
                {
                    if (selected.Location.X < sender.Location.X)
                    {
                        selected.Location = new Point(selected.Location.X + 1, selected.Location.Y);
                    }
                    else
                    {
                        selected.Location = new Point(selected.Location.X - 1, selected.Location.Y);
                    }
                }
                else if (selected.Location.Y != sender.Location.Y)
                {
                    if (selected.Location.Y < sender.Location.Y)
                    {
                        selected.Location = new Point(selected.Location.X, selected.Location.Y + 1);
                    }
                    else
                    {
                        selected.Location = new Point(selected.Location.X, selected.Location.Y - 1);
                    }
                }
            }
            selected.BackColor = color;
        }

        private void DeselectBoard()
        {

            if (selected != null && BoardPieces.ContainsKey(selected) && BoardPieces[selected].Position.X % 2 != 0)
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
            else if(selected != null && BoardPieces.ContainsKey(selected))
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
            if(selectedMovable != null)
            foreach(ChessPosition pos in selectedMovable)
            {
                if (pos.X % 2 != 0)
                {
                    if (pos.Y % 2 != 0)
                    {
                        if(piecePositions[pos.X][pos.Y].ocupied)
                        {
                            piecePositions[pos.X][pos.Y].piece.Piece.BackColor = Color.Black;

                        }
                    }
                    else
                    {
                        if (piecePositions[pos.X][pos.Y].ocupied)
                        {
                            piecePositions[pos.X][pos.Y].piece.Piece.BackColor = Color.White;

                        }
                    }
                }
                else
                {
                    if (pos.Y % 2 != 0)
                    {
                        if (piecePositions[pos.X][pos.Y].ocupied)
                        {
                            piecePositions[pos.X][pos.Y].piece.Piece.BackColor = Color.White;

                        }
                    }
                    else
                    {
                        if (piecePositions[pos.X][pos.Y].ocupied)
                        {
                            piecePositions[pos.X][pos.Y].piece.Piece.BackColor = Color.Black;

                        }
                    }
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
                            picbox.BackColor = Color.Black;
                        }
                        else
                        {
                            picbox.BackColor = Color.White;
                        }
                    }
                    else
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

        private void InitializeSinglePiece(char i, int y, PieceType type, ChessColor color,Color col)
        {
            PictureBox chessPiece = new PictureBox { Location = Board[i][y].Location, Size = Board[i][y].Size, BackColor = Board[i][y].BackColor, BorderStyle = Board[i][y].BorderStyle, Image = PieceImages[type][col] };
            ChessPiece piece = null;
            switch (type)
            {
                case PieceType.PAWN: piece = new Pawn(i, y, color, chessPiece); break;
                case PieceType.ROOK: piece = new Rook(i, y, color, chessPiece); break;
                case PieceType.BISHOP: piece = new Bishop(i, y, color, chessPiece); break;
                case PieceType.KING: piece = new King(i, y, color, chessPiece); break;
                case PieceType.KNIGHT: piece = new Knight(i, y, color, chessPiece); break;
                case PieceType.QUEEN: piece = new Queen(i, y, color, chessPiece); break;
            }
            if(color == ChessColor.WHITE)
            {
                white_pieces.Add(piece);
            }
            else
            {
                black_pieces.Add(piece);
            }
            if(piece.Type == PieceType.KING)
            {
                Kings.Add(piece.Color,(King)piece);
            }
            BoardPieces.Add(chessPiece, piece);
            piecePositions[i][y].piece = piece;
            piecePositions[i][y].ocupied = true;
            chessPiece.MouseClick += Piece_MouseClick;
            Controls.Add(chessPiece);
            chessPiece.BringToFront();
        }
        private void InitializePawns()
        {
            int y = 2;
            for (int i = 'A'; i <= 'H'; i++)
            {
                InitializeSinglePiece((char)i, y, PieceType.PAWN, ChessColor.WHITE, Color.White);
            }
            y = 7;
            for (int i = 'A'; i <= 'H'; i++)
            {
                InitializeSinglePiece((char)i, y, PieceType.PAWN, ChessColor.BLACK, Color.Black);
            }
        }
        private void InitializeRooks()
        {
            int y = 1;
            int i = 'A';
            InitializeSinglePiece((char)i, y, PieceType.ROOK, ChessColor.WHITE, Color.White);
            i = 'H';
            InitializeSinglePiece((char)i, y, PieceType.ROOK, ChessColor.WHITE, Color.White);
            y = 8;
            i = 'A';
            InitializeSinglePiece((char)i, y, PieceType.ROOK, ChessColor.BLACK, Color.Black);
            i = 'H';
            InitializeSinglePiece((char)i, y, PieceType.ROOK, ChessColor.BLACK, Color.Black);
        }

        private void InitializeKnight()
        {
            int y = 1;
            int i = 'B';
            InitializeSinglePiece((char)i, y, PieceType.KNIGHT, ChessColor.WHITE, Color.White);
            i = 'G';
            InitializeSinglePiece((char)i, y, PieceType.KNIGHT, ChessColor.WHITE, Color.White);
            y = 8;
            i = 'B';
            InitializeSinglePiece((char)i, y, PieceType.KNIGHT, ChessColor.BLACK, Color.Black);
            i = 'G';
            InitializeSinglePiece((char)i, y, PieceType.KNIGHT, ChessColor.BLACK, Color.Black);
        }

        private void InitializeBishop()
        {
            int y = 1;
            int i = 'C';
            InitializeSinglePiece((char)i, y, PieceType.BISHOP, ChessColor.WHITE, Color.White);
            i = 'F';
            InitializeSinglePiece((char)i, y, PieceType.BISHOP, ChessColor.WHITE, Color.White);
            y = 8;
            i = 'C';
            InitializeSinglePiece((char)i, y, PieceType.BISHOP, ChessColor.BLACK, Color.Black);
            i = 'F';
            InitializeSinglePiece((char)i, y, PieceType.BISHOP, ChessColor.BLACK, Color.Black);
        }
        private void InitializeKing()
        {
            int y = 1;
            int i = 'E';
            InitializeSinglePiece((char)i, y, PieceType.KING, ChessColor.WHITE, Color.White);
            y = 8;
            i = 'E';
            InitializeSinglePiece((char)i, y, PieceType.KING, ChessColor.BLACK, Color.Black);
        }
        private void InitializeQueen()
        {
            int y = 1;
            int i = 'D';
            InitializeSinglePiece((char)i, y, PieceType.QUEEN, ChessColor.WHITE, Color.White);

            y = 8;
            i = 'D';
            InitializeSinglePiece((char)i, y, PieceType.QUEEN, ChessColor.BLACK, Color.Black);
        }

        private void DrawBoard()
        {
            int nextPosX = 50;
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
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            Board[(char)j][i].BackColor = Color.White;
                        }
                        else
                        {
                            Board[(char)j][i].BackColor = Color.Black;
                        }
                    }
                    Controls.Add(Board[(char)j][i]);
                    positionOnBoard.Add(Board[(char)j][i], new ChessPosition((char)j,i));
                    nextPosX += 75;
                }
                nextPosX = 50;
                nextPosY += 75;
            }

            nextPosX = 20;
            nextPosY = 110;
            for (int i = 8; i >= 1; i--)
            {
                Label label = new Label { Name = i.ToString(), Text = i.ToString(), Location = new Point(nextPosX, nextPosY), Size = new Size(75, 75), Font = new Font(Font.FontFamily, 16)};
                Controls.Add(label);
                nextPosY += 75;
            }
            nextPosX = 75;
            nextPosY -= 20;
            for (int i = 'A'; i <= 'H'; i++)
            {
                Label label = new Label { Name = ((char)i).ToString(), Text = ((char)i).ToString(), Location = new Point(nextPosX, nextPosY), Size = new Size(75, 75), Font = new Font(Font.FontFamily, 16) };
                Controls.Add(label);
                label.BringToFront();
                nextPosX += 75;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 27)
            {
                Application.Exit();
            }
        }

        private void PlayerTime_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            if(timeElapsed == 30)
            {
                pbTimeLeft.Visible = true;
            }
            if(timeElapsed >= 30)
            pbTimeLeft.Value = (turn_time - timeElapsed);
            if(timeElapsed == turn_time)
            {
                timeElapsed = 0;
                pbTimeLeft.Visible = false;
                turn = turn == ChessColor.BLACK ? ChessColor.WHITE : ChessColor.BLACK;
                DeselectBoard();
            }
        }

        private void loadMoves()
        {
            lbMoves.DataSource = null;
            lbMoves.DataSource = MovesMade;
        }

        private void NewGame()
        {
            foreach(PictureBox pictureBox in BoardPieces.Keys)
            {
                Controls.Remove(pictureBox);
            }
            BoardPieces = new Dictionary<PictureBox, ChessPiece>();
            foreach (var i in piecePositions.Values)
            {
                foreach(positionInfo info in i.Values)
                {
                    info.ocupied = false;
                    info.piece = null;
                }
            }
            white_pieces = new List<ChessPiece>();
            black_pieces = new List<ChessPiece>();
            Kings = new Dictionary<ChessColor, King>();
            turn = ChessColor.WHITE;
            timeElapsed = 0;
            pbTimeLeft.Visible = false;
            InitializePieces();
            MovesMade = new List<Moves>();
            Moves move = new Moves(null, 'A', 1, 'A', 1);
            move.init = true;
            MovesMade.Add(move);
            loadMoves();
        }

        private void btnSurrender_Click(object sender, EventArgs e)
        {
            Victory victoryForm = new Victory(turn);
            if(victoryForm.ShowDialog() == DialogResult.OK)
            {
                NewGame();
            }
            else
            {
                Application.Exit();
            }
        }

        private void v2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void surrenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Victory victoryForm = new Victory(turn);
            if (victoryForm.ShowDialog() == DialogResult.OK)
            {
                NewGame();
            }
            else
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void movesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbMoves.Visible = !lbMoves.Visible;
        }
    }
}
