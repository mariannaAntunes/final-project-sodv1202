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

    public abstract int ColumnChoosen();
}

class HumanPlayer : Player
{
    public HumanPlayer (string name,  char symbol) : base(name, symbol)
    {

    }
    
    public override int ColumnChoosen()
    {
        int columnNum;
        Console.WriteLine("Enter column number: ");
        columnNum = int.Parse(Console.ReadLine());
        return columnNum;
    }
}

class Controller
{

}


class Program
{
    static void Main(string[] args)
    {
        //Model.StartGame();
        //Model.DisplayBoard();
    }
}
