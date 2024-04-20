public abstract class Player
{ 
    public string Name { get; set; }
    protected static int _turn;

    static Player()
    {
        _turn = 0;
    }

    public abstract int columnChoosen();
}

class HumanPlayer : Player
{
    public override int columnChoosen()
    {
        int columnNum;
        Console.WriteLine("Enter column number: ");
        columnNum = int.Parse(Console.ReadLine());
        return columnNum;
    }
}


class Program
{
    static void Main(string[] args)
    {
        
    }
}
