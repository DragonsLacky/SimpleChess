# SimpleChess
<h3>1. Објаснување на проблемот</h3>
Апликацијата претставува една класична игра на шах што вообичаено се игра во живо со 2 играчи, за игра потребни се една табла 8x8 и има 6 различни фигури и сите имаат свои предодредени потези, целта во играта е да се оневозможи противничкиот крал, играчите имаат по еден потег наизменично, таблата секогаш има предодредени позиции на која бројот го означува редот а буквата колоната, и секогаш се почнува од левиот десен агол.

<h3>2. Решавање на проблемот</h3>
Проблемот е решен со тоа што на почеток се поставуваат елементи од типот PIctureBox за секое поле на таблата со користење на x,y координатите од формата, на надворешноста од таблата поставени се контроли од типот Label за да го означуваат редот и колоната на таблата, исто така и почетните позиции на фигурите, кои се прикажани на формата преку контрола PictureBox, оваа контрола се чува во посебна класа наречена ChessPiece, која е абстрактна, од оваа класа наследуваат класите на сите фигури, и ги имплементираат абстрактните методи, односно методи за поместување на фигурите. Поместувањето на фигурите е реализирано така што кога се иницијализираат фигурите на контролата PictureBox се додава click евент во кој кога ќе се притисне фигура се селектира, се зачувува во променливата selected, потоа се земаат сите валидни потези на тој елемент преку соодветниот метод, и се прикажуваат визуелно преку својството BackColor на контролата PictureBox, за пристигнување до соодветната контрола се користи мапирање од позициите на таблата до соодветниот PictureBox елемент, потоа за поместување има друг click евент поставен на секој PictureBox од таблата, кога ќе се кликне на таблата се прави проверка дали потегот е валиден пред да се изврши, доколку се притисе еден од прикажаните потези се променуваат вредностите во соодветниот ChessPiece објект, но и вредностите во сите искористени мапи, ова се прави во позадина, додека визуелно се прикажува постепеното поместување на објектот до дестинацијата, изведено е така што е пуштен еден while циклус кој ја променува локацијата на PictureBox за 1 на секое вртење и врти се додека не пристигне до дестинацијата, а доколку дојде до случај да се зема фигура од противникот се прави во click методот на фигури од каде се повикува метод да ја отстрани фигурата и да ја помести предходната. За достигнување на целта на играта, да се оневозможи кралот, поставени се методи во класата на кралот, кои одредуваат дали кралот е во позиција на Check, или CheckMate, и дали може да се излези од оваа состојба, доколку не може играта ќе заврши, Доколку кралот е во состојба Check оневозможени се сите потези кои ќе предизвикаат кралот да остани во таа позиција. Исто така имплементирана е класа Moves која ги следи потезите и ги прикажува во компонента ListBox. Се обидовме да додадеме и mode на игра во која би можел да игра 1 играч против компјутер, за кое се направени 2 класи, тоа се класата Computer која ги извршува потезите на црните фигури и во неа чува некои неопходни мапирања за извршување на сите можни потези, другата класа е MovesTree која претставува дрво од потези, за секој потег се чува позицијата на таблата за фигурите, и листа од бели и кој потег бил направен во тој дел од дрвото, класата исто така соджи метод за евалуација на потезите, со што би се добило отценка, во дрво класата имплементиран е алгоритам за наоѓање на најоптималниот потег, во Computer класата се чува една инстанца од MovesTree класата која се иницијализира со метод, при што се зема нова инстанца од мапирањето за таблата и позициите за секој node во дрвото, потоа во главниот метод на Computer класата, а тоа е evaluate методот, тој служи за генерирање и избирање на потег, и потоа негово извршување, во главната Computer ги извшува потезите во тајмер кој е поставен да ги избројува секундиде на секој играч, на 1 играч дозволено му е 60 секунди време за 1 потег, овој тајмер исто така служи за компјутерскиот играч да ги извршува своите потези, коге му е ред. Но во компјутер класата има проблеми со евалуација на потезите при што не ги евалуире сите потези и цело време извршува фиксни потези.

<h3>2. Oпис на функцијата за поместување</h3><br>

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
Оваа функција се користи за поместување на селектирана фигура на едно од предодредените позиции на таблата,
прво се врши проверка дали игра компјутер и доколку игра дали е ред на црни фигури, доколку би играл компјутерот и на ред е играчот со црни фигури оваа функција нема да прави ништо, исто така се врши проверка на селектираниот елемент дали е валиден, потоа се проверува дали сопствениот крал на играчот на потег е во позиција Check,
доколку е се симулира поместување на  елементот во позадина со што за пиони се чува bool променлива која кажува дали е на почеток и доколку е се користи за да се ресетира пионот, почетната и крајната позиција се земаат од соодветната мапа, односно од мапа char во друга мапа која мапира int вредност во елемент од PositionInfo кој содржи информации за соодветното поле дали е зафатено и доколку е зафатено го содржи соодветниот елемент од типот ChessPiece, до оваа мапа се пристапува преку друга мапа која мапира од PictureBox во соодветна фигура (само за фигурите), бидејќи само PictureBox имаат click евенти, а за притиснатиот PictureBox се користи мапата positionOnBoard која мапира од PictureBox во соодветна позиција на таблата, потоа се изменуваат вредностите и се поместува фигурата во позадина и се извршува уште една проверка за дали кралот е во Check после извршениот потег доколку е, ќе се врати почетната состојба на таблата и нема да дозволи да се направи тој потег, потоа доколку кралот не бил од почеток во состојба Check, се проверува дали селектираното поле е валидно за поместување на фигурата, доколку е селектирано од обележаните места тој услов би поминал, сега исто како и претходниот пат се земаат стартната позиција и крајната позиција, се променуваат во позадина и се проверува дали кралот на тековниот играч влегол во Check по извршувањето на позицијата, во внатрешноста Check функцијата ги добива сите бели и црни фигури и поставувањето на таблата, и во зависност кој е на ред се земаат сите фигури од противникот, потоа за сите нивни можни потези се проверува дали може да ја стигнат позицијата на кралот доколку една фигура може  оваа функција враќа вредност true, со што ќе се ресетира позицијата на поместената фигура и нема да се дозволи поместување, но доколку кралот не доаѓа до состојба Check, поместената фигура останува, и се прави записник на потегот, потоа се врши проверка дали поместената фигура била пион, доколку е се проверува дали стигнала до крајот на таблата, доколку е стигната се отвора форма за избор на фигура за замена, се заменува така што комплетно се брише пионот и ново креираната фигура, која е направена и сместена во формата за промена се користи за да се иницијализира тотално нова фигура на иста позиција но од различен тип, и се поставува за селектирана, исто така се прави и визуелниот приказ за корисникот дека фигурата е поместена, потоа се прави проверка по поместувањето на фигурата дали спротивниот крал е во check доколку е се прави уште една проверка за дали може да спречи загуба, оваа функција е имплементирана така што се проверува прво дали е CheckMate (тоа значи дека има фигура која го достигнува кралот а кралот неможе да направи сопствен потег бидејќи е блокиран) единствена шанса е да се помести друга фигура за одбрана на кралот, па поради тоа поставен е услов, бројот на фигури кои го достигаат кралот да е 1, бигејќи е невозможно со еден потег да се блокираат 2 фигури, доколку е само една фигура се земаат сите позиции кои се потребни на противникот да стигне до кралот (Од соодветната фигура до кралот, другите се игнорираат), и се проверува дали некоја друга со иста боја фигура може да пристигне до неа (за Rook, Bishop и Queen), а за Knight се гледа дали може да биде достигнат од друга фигура, доколку е можно ова се продолжува со игра како и нормално, но доколку се одлучи дека победил играчот се појавува нова форма за победа на која го пишува победникот и има 2 копчуња за избор, едното е за нова игра да започне, а другото е за да се исклучи играта, доколку играта продолжува нормално, се поставува тајмерот на 0 за следниот играч и се променува вредноста на turn променливата која одредува кој играч е на ред. Потоа за крај се повикува DeselectBoard функцијата која само ги променува боите на позадината на прикажаните потези за асоодветниот играч.

<img src="https://i.imgur.com/2OICd2V.png" alt="SelectMode"/>
<img src="https://i.imgur.com/GkIJC1T.png" alt="ChessGame"/>
<img src="https://i.imgur.com/6D8Zugt.png" alt="Gamplay Look"/>
<imt src="https://i.imgur.com/d3XPVOR.png" alt="Victory screen">


<h3>Упатство за играње</h3>

Се игра како класична игра на шах, прво се притиска на фигурата која сакаме да ја селектираме, потоа кога ќе се прикажат потезите се очекува играчот да избере еден од нив, во случај на клик надвот од избраните валидни потези се деселектира фигурата, исто така Player v Computer е нефунционално со што компјутерот повторува одреден борј на потези цело време, за двата играчи има опција да се предат, со што е поставено скриено копче, које се прикажува првиот пат кога се направи Check на кралот, но оваа опција постои цело време во менито, Home -> Surrender.
