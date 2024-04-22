using System.Security.Cryptography.X509Certificates;

public abstract class Player
{
    public string Name { get; set; }
    public char Symbol { get; set; } //This variable allows further improvements to the code

    public Player(string name)
    {
        Name = name;
        Symbol = '?';
    }

    public Player(string name, char symbol) //Further improvement (constructor overloading): allows player to choose the symbol
    {
        Name = name;
        Symbol = symbol;
    }

    public abstract int ColumnChosen(); //Abstract class to be ovverriden according to the type of player.
                                        //A human player will choose the column through the console and a computer player will choose the column through AI algorithm
}

class HumanPlayer : Player
{
    public HumanPlayer(string name, char symbol) : base(name, symbol)
    {

    }
    public HumanPlayer(string name) : base(name)
    {

    }

    public override int ColumnChosen()
    {
        int columnNum;
        Console.WriteLine("Enter column number: ");
        columnNum = int.Parse(Console.ReadLine());
        return columnNum;
    }
}

public static class Model
{
    public static List<Player> _playersList;
    
    public static int _turn;
    /*static Model()
    {
        _turn = 0;
    }*/

    public static char[,] _board = new char[6, 7];

    public static void StartGame()
    {
        _turn = 0;
        
        for(int i=0; i<_board.GetLength(0);  i++)
        {
            for(int j=0; j<_board.GetLength(1); j++)
            {
                _board[i, j] = '#';
            }
        }
    }

    public static void DisplayBoard()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            Console.Write("| ");
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                Console.Write(_board[i, j] + " ");
            }
            Console.Write("|");
            Console.WriteLine();
        }
        Console.Write("  1 2 3 4 5 6 7  ");
        Console.WriteLine();
    }
}


class Controller
{
    private static bool CheckWinner() //This method checks if there is a connected 4 completed (horizontal, vertical or diagonally)
    {
        //Horizontal check
        for (int i = 0; i < Model._board.GetLength(0); i++)
        {
            for (int j = 0; j < Model._board.GetLength(1)-3; j++)
            {
                if(Model._board[i, j] == Model._playersList[Model._turn].Symbol && Model._board[i, j+1] == Model._playersList[Model._turn].Symbol && Model._board[i, j + 2] == Model._playersList[Model._turn].Symbol && Model._board[i, j + 3] == Model._playersList[Model._turn].Symbol)
                {
                    Console.WriteLine($"It is a Connect 4. {Model._playersList[Model._turn].Name} wins!");
                    return true;
                }
            }
        }

        //Vertical check
        for (int i = 0; i < Model._board.GetLength(0) - 3; i++)
        {
            for (int j = 0; j < Model._board.GetLength(1); j++)
            {
                if (Model._board[i, j] == Model._playersList[Model._turn].Symbol && Model._board[i + 1, j] == Model._playersList[Model._turn].Symbol && Model._board[i + 2, j] == Model._playersList[Model._turn].Symbol && Model._board[i + 3, j] == Model._playersList[Model._turn].Symbol)
                {
                    Console.WriteLine($"It is a Connect 4. {Model._playersList[Model._turn].Name} wins!");
                    return true;
                }
            }
        }

        //Diagonal Up check
        for (int i = Model._board.GetLength(0) - 1; i >= 3; i--)
        {
            for (int j = 0; j < Model._board.GetLength(1)-3; j++)
            {
                if (Model._board[i, j] == Model._playersList[Model._turn].Symbol && Model._board[i - 1, j+1] == Model._playersList[Model._turn].Symbol && Model._board[i - 2, j+2] == Model._playersList[Model._turn].Symbol && Model._board[i - 3, j+3] == Model._playersList[Model._turn].Symbol)
                {
                    Console.WriteLine($"It is a Connect 4. {Model._playersList[Model._turn].Name} wins!");
                    return true;
                }
            }
        }

        return false;
    }

    private static bool CheckGameOver() //This method checks if the board is already completed (return false) or not (return true)
    {
        for (int i = 0; i < Model._board.GetLength(0); i++)
        {
            for (int j = 0; j < Model._board.GetLength(1); j++)
            {
                if(Model._board[i, j] == '#')
                {
                    return false;
                }
            }
        }

        return true;
    }


    public static void Play()
    {
        string namePlayer1, namePlayer2;
        //char symbolPlayer1='X', symbolPlayer2='O';

        Console.WriteLine("Connect 4 Game");
        Console.WriteLine();

        Console.WriteLine("Enter first player's name: ");
        namePlayer1 = Console.ReadLine();
        Console.WriteLine("Enter second player's name: ");
        namePlayer2 = Console.ReadLine();

        Player player1 = new HumanPlayer(namePlayer1);
        Player player2 = new HumanPlayer(namePlayer2);

        player1.Symbol = 'X';
        player2.Symbol = 'O';

        Model._playersList = [player1, player2];

        Model.StartGame();
        Model.DisplayBoard();

        int columnNum;

        while (true)
        {
            Console.WriteLine($"{Model._playersList[Model._turn].Name}, enter column number (1-7).");
            columnNum = int.Parse(Console.ReadLine());
            if (columnNum >= 1 && columnNum <= 7)
            {
                for (int i = Model._board.GetLength(0)-1; i >=0 ; i--)
                {
                    if (Model._board[i, columnNum-1]=='#')
                    {
                        Model._board[i, columnNum - 1] = Model._playersList[Model._turn].Symbol;
                        Model.DisplayBoard();
                        if (CheckWinner())
                        {
                            return; //Ask if players want to play again
                        }
                        else
                        {
                            if(CheckGameOver())
                            {
                                return; //Ask if players want to play again
                            }
                            else
                            {
                                Model._turn = (Model._turn + 1) % Model._playersList.Count;
                                break;
                            }
                        }
                    }
                    else if(i==0)
                    {
                        Console.WriteLine("Choose another column.");
                    }
                }
            }
            else //May be an exception
            {
                Console.WriteLine("Invalid column number!");
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {       
        Controller.Play();
    }
}
