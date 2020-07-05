using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleChess.Pieces;

namespace SimpleChess
{
    class Computer
    {
        public  Dictionary<char,Dictionary<int, positionInfo>> Positions { get; set; }
        public Dictionary<char, Dictionary<int, PictureBox>> BoardPos { get; set; }
        public Control.ControlCollection Controls { get; set; }
        public Dictionary<PictureBox, ChessPosition> PositionOnBoard { get; set; }
        public Dictionary<PictureBox, ChessPiece> BoardPieces { get; set; }
        public List<ChessPiece> MyPieces { get; set; }
        public List<ChessPiece> EnemyPieces { get; set; }
        public MovesTree moves { get; set; }
        public Computer(Dictionary<char, Dictionary<int, positionInfo>> positions, Dictionary<PictureBox, ChessPosition> positionOnBoard, Dictionary<PictureBox, ChessPiece> boardPieces, List<ChessPiece> myPieces, List<ChessPiece> enemyPieces,Dictionary<char, Dictionary<int, PictureBox>> boardPos  )
        {
            Positions = positions;
            PositionOnBoard = positionOnBoard;
            BoardPieces = boardPieces;
            MyPieces = myPieces;
            EnemyPieces = enemyPieces;
            BoardPos = boardPos;
            moves = new MovesTree(Positions, EnemyPieces, MyPieces, null);
        }
        public Object InitializeTree(int depth, MovesNode node, bool turn, Dictionary<char, Dictionary<int, positionInfo>> pos)
        {
            if( depth == 0)
            {
                return null;
            }
            List<ChessPiece> white_Pieces = new List<ChessPiece>();
            List<ChessPiece> black_Pieces = new List<ChessPiece>();
            foreach (var x in pos.Values)
            {
                foreach (var y in x.Values)
                {
                    if (y.ocupied)
                    {
                        if (y.piece.Color == ChessColor.WHITE)
                        {
                            white_Pieces.Add(y.piece);
                        }
                        else
                        {
                            black_Pieces.Add(y.piece);
                        }
                    }
                }
            }
            foreach (var piece in turn ? black_Pieces : white_Pieces)
            {
                foreach (var move in piece.getValidMoves(white_Pieces, black_Pieces, pos))
                {
                    Dictionary<char, Dictionary<int, positionInfo>> positions = new Dictionary<char, Dictionary<int, positionInfo>>();
                    foreach (var chr in pos.Keys)
                    {
                        positions.Add(chr, new Dictionary<int, positionInfo>());
                        foreach (var intr in pos[chr].Keys)
                        {
                            if (pos[chr][intr].ocupied)
                            {
                                ChessPiece newPiece = null;
                                switch (pos[chr][intr].piece.Type)
                                {
                                    case PieceType.PAWN: newPiece = new Pawn(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); ((Pawn)newPiece).startingPos = ((Pawn)pos[chr][intr].piece).startingPos; break;
                                    case PieceType.ROOK: newPiece = new Rook(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); break;
                                    case PieceType.KNIGHT: newPiece = new Knight(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); break;
                                    case PieceType.BISHOP: newPiece = new Bishop(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); break;
                                    case PieceType.QUEEN: newPiece = new Queen(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); break;
                                    case PieceType.KING: newPiece = new King(pos[chr][intr].piece.Position.X, pos[chr][intr].piece.Position.Y, pos[chr][intr].piece.Color, pos[chr][intr].piece.Piece); break;
                                }
                                positions[chr].Add(intr, new positionInfo(newPiece, true));
                            }
                            else
                            {
                                positions[chr].Add(intr, new positionInfo(null, false));
                            }

                        }
                    }
                    List<ChessPiece> whitePieces = new List<ChessPiece>();
                    List<ChessPiece> blackPieces = new List<ChessPiece>();
                    foreach (var x in positions.Values)
                    {
                        foreach (var y in x.Values)
                        {
                            if (y.ocupied)
                            {
                                if (y.piece.Color == ChessColor.WHITE)
                                {
                                    whitePieces.Add(y.piece);
                                }
                                else
                                {
                                    blackPieces.Add(y.piece);
                                }
                            }
                        }
                    }
                    Moves movement = new Moves(positions[piece.Position.X][piece.Position.Y].piece, piece.Position.X, piece.Position.Y, move.X, move.Y);
                    
                    if (positions[piece.Position.X][piece.Position.Y].ocupied && positions[move.X][move.Y].ocupied)
                    {
                        movement.PieceTaken = true;
                        movement.TakenType = positions[move.X][move.Y].piece.Type.ToString();
                        if (positions[move.X][move.Y].piece.Color == ChessColor.WHITE)
                        {
                            whitePieces.Remove(positions[move.X][move.Y].piece);
                        }
                        else
                        {
                            whitePieces.Remove(positions[move.X][move.Y].piece);
                        }
                    }
                    if(positions[piece.Position.X][piece.Position.Y].ocupied)
                    {
                        positions[move.X][move.Y].ocupied = true;
                        positions[move.X][move.Y].piece = positions[piece.Position.X][piece.Position.Y].piece;
                        positions[piece.Position.X][piece.Position.Y].ocupied = false;
                        positions[piece.Position.X][piece.Position.Y].piece = null;
                        positions[move.X][move.Y].piece.MovePiece(move.X, move.Y);
                    }
                    
                    node.Children.Add(new MovesNode(positions, whitePieces, blackPieces, movement));
                }
            }
            foreach (var child in node.Children)
            {
                return InitializeTree(depth - 1, child, !turn, child.Board);
            }
            return null;
        }
        public Moves evaluate()
        {
            moves = new MovesTree(Positions, EnemyPieces, MyPieces, null);
            InitializeTree(11, moves.Root, true, Positions);
            List<int> nodes = new List<int>();
            foreach (var Child in moves.Root.Children)
            {
                nodes.Add(moves.AlphaBeta(Child, 10,int.MinValue, int.MaxValue, true));
            }
            int min = int.MaxValue;
            foreach (var nodeValues in nodes)
            {
                if(nodeValues < min)
                {
                    min = nodeValues;
                }
            }
            MovesNode move = moves.Root.Children[nodes.IndexOf(min)];
            if (move.move.PieceTaken)
            {
                EnemyPieces.Remove(Positions[move.move.destiny_X][move.move.destiny_Y].piece);
                Controls.Remove(Positions[move.move.destiny_X][move.move.destiny_Y].piece.Piece);
                BoardPieces.Remove(Positions[move.move.destiny_X][move.move.destiny_Y].piece.Piece);
                Positions[move.move.destiny_X][move.move.destiny_Y].ocupied = true;
                Positions[move.move.destiny_X][move.move.destiny_Y].piece = Positions[move.move.startX][move.move.startY].piece;
                Positions[move.move.startX][move.move.startY].ocupied = false;
                Positions[move.move.startX][move.move.startY].piece = null;
                Positions[move.move.destiny_X][move.move.destiny_Y].piece.MovePiece(move.move.destiny_X, move.move.destiny_Y);
                AnimateMovement(BoardPos[move.move.destiny_X][move.move.destiny_Y], Positions[move.move.destiny_X][move.move.destiny_Y].piece.Piece);
                return move.move;
            }
            else
            {
                Positions[move.move.destiny_X][move.move.destiny_Y].ocupied = true;
                Positions[move.move.destiny_X][move.move.destiny_Y].piece = Positions[move.move.startX][move.move.startY].piece;
                Positions[move.move.startX][move.move.startY].ocupied = false;
                Positions[move.move.startX][move.move.startY].piece = null;
                Positions[move.move.destiny_X][move.move.destiny_Y].piece.MovePiece(move.move.destiny_X, move.move.destiny_Y);
                AnimateMovement(BoardPos[move.move.destiny_X][move.move.destiny_Y], Positions[move.move.destiny_X][move.move.destiny_Y].piece.Piece);
                return move.move;
            }
        }
        private void AnimateMovement(PictureBox sender, PictureBox selected)
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
        public List<ChessPosition> getCurrentBoardValue()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            return positions;
        }
    }
}
