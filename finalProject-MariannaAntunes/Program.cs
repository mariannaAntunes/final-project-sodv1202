using System.Security.Cryptography.X509Certificates;

public static class Model
{
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

public abstract class Player
{ 
    public string Name { get; set; }
    public char Symbol { get; set; } //This variable allows further improvements to the code

    public Player(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    public abstract int ColumnChosen(); //Abstract class to be ovverriden according to the type of player.
                                        //A human player will choose the column through the console and a computer player will choose the column through AI algorithm
}

class HumanPlayer : Player
{
    public HumanPlayer (string name,  char symbol) : base(name, symbol)
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

class Controller
{
    public static void Play(Player player1, Player player2)
    {

    }
}


class Program
{
    static void Main(string[] args)
    {
        string namePlayer1, namePlayer2;
        char symbolPlayer1='X', symbolPlayer2='O';
        
        Console.WriteLine("Connect 4 Game");
        Console.WriteLine();

        Console.WriteLine("Enter first player's name: ");
        namePlayer1 = Console.ReadLine();
        Console.WriteLine("Enter second player's name: ");
        namePlayer2 = Console.ReadLine();

        Player player1 = new HumanPlayer(namePlayer1, symbolPlayer1);
        Player player2 = new HumanPlayer(namePlayer2, symbolPlayer2);

        Controller.Play(player1, player2);
    }
}
