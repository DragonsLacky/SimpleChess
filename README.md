# SimpleChess
<h3>1. Објаснување на проблемот</h3>
Апликацијата претставува една класична игра на шах што вообичаено се игра во живо со 2 играчи, за игра потребни се една табла 8x8 и има 6 различни фигури и сите имаат свои предодредени потези, целта во играта е да се оневозможи противничкиот крал, играчите имаат по еден потег наизменично, таблата секогаш има предодредени позиции на која бројот го означува редот а буквата колоната, и секогаш се почнува од левиот десен агол.

<h3>2. Решавање на проблемот</h3>
Проблемот е решен со тоа што на почеток се поставуваат елементи од типот PIctureBox за секое поле на таблата со користење на x,y координатите од формата, на надворешноста од таблата поставени се контроли од типот Label за да го означуваат редот и колоната на таблата, исто така и почетните позиции на фигурите, кои се прикажани на формата преку контрола PictureBox, оваа контрола се чува во посебна класа наречена ChessPiece, која е абстрактна, од оваа класа наследуваат класите на сите фигури, и имплементираат абстрактните методи, односно методи за поместување на фигурите. Поместувањето на фигурите е реализирано така што кога се иницијализираат фигурите на контролата PictureBox се додава click евент во кој кога ќе се притисне фигура се селектира, се зачувува во променливата selected, потоа се земаат сите валидни потези на тој елемент преку соодветниот метод, и се прикажуваат визуелно преку својството BackColor на контролата PictureBox, за пристигнување до соодветната контрола се користи мапирање од позициите на таблата до соодветниот PictureBox елемент, потоа за поместување има друг click евент поставен на секој PictureBox од таблата, кога ќе се кликне на таблата се прави проверка дали потегот е валиден пред да се изврши, доколку се притисе еден од прикажаните потези се променуваат вредностите во соодветниот ChessPiece објект, но и вредностите во сите искористени мапи, ова се прави во позадина, додека визуелно се прикажува постепеното поместување на објектот до дестинацијата, изведено е така што е пуштен еден while циклус кој ја променува локацијата на PictureBox за 1 на секое вртење и врти се додека не пристигне до дестинацијата, а доколку дојде до случај да се зема фигура од противникот се прави во click методот на фигури од каде се повикува метод да ја отстрани фигурата и да ја помести предходната. За достигнување на целта на играта, да се оневозможи кралот, поставени се методи во класата на кралот, кои одредуваат дали кралот е во позиција на Check, или CheckMate, и дали може да се излези од оваа состојба, доколку не може играта ќе заврши, Доколку кралот е во состојба Check оневозможени се сите потези кои ќе предизвикаат кралот да остани во таа позиција. Исто така имплементирана е класа Moves која ги следи потезите и ги прикажува во компонента ListBox. Се обидовме да додадеме и mode на игра во која би можел да игра 1 играч против компјутер, за кое се направени 2 класи, тоа се класата Computer која ги извршува потезите на црните фигури и во неа чува некои неопходни мапирања за извршување на сите можни потези, другата класа е MovesTree која претставува дрво од потези, за секој потег се чува позицијата на таблата за фигурите, и листа од бели и кој потег бил направен во тој дел од дрвото, класата исто така соджи метод за евалуација на потезите, со што би се добило отценка, во дрво класата имплементиран е алгоритам за наоѓање на најоптималниот потег, во Computer класата се чува една инстанца од MovesTree класата која се иницијализира со метод, при што се зема нова инстанца од мапирањето за таблата и позициите за секој node во дрвото, потоа во главниот метод на Computer класата, а тоа е evaluate методот, тој служи за генерирање и избирање на потег, и потоа негово извршување, во главната Computer ги извшува потезите во тајмер кој е поставен да ги избројува секундиде на секој играч, на 1 играч дозволено му е 60 секунди време за 1 потег, овој тајмер исто така служи за компјутерскиот играч да ги извршува своите потези, коге му е ред. Но во компјутер класата има проблеми со евалуација на потезите при што не ги евалуире сите потези и цело време извршува фиксни потези.

<h3>2. Oпис на функцијата за поместување</3>

```c#
private void Board_MouseClick(Object sender, MouseEventArgs e)
{
    if(!computer_enabled || turn != ChessColor.BLACK)
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
```

