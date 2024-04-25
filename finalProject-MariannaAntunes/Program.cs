
//class Player is an abstract base class that contains fields and methods related to the players
public abstract class Player
{
    public string Name { get; set; }
    public char Symbol { get; set; } //This variable allows further extensions to the code

    public Player(string name)
    {
        Name = name;
        Symbol = '?'; //Default symbol (question mark) to identify in the board if someone's symbol was not defined properly
    }

    public Player(string name, char symbol) //Further improvement (constructor overloading): allows player to choose the symbol
    {
        Name = name;
        Symbol = symbol;
    }

    public abstract int ColumnChosen(); //Abstract class to be ovverriden according to the type of player.
                                        //A human player will choose the column through the console and a computer player will choose the column through AI algorithm
}

//class HumanPlayer inherits class Player 
class HumanPlayer : Player
{
    public HumanPlayer(string name, char symbol) : base(name, symbol) //Using base constructor when two parameters are passed
    {

    }
    public HumanPlayer(string name) : base(name) //Using base constructor when one parameter is passed
    {

    }

    public override int ColumnChosen() //This method overrides base class method for a human player that uses the console to input the chosen column number
    {
        int columnNum;
        Console.WriteLine("Enter column number: ");
        columnNum = int.Parse(Console.ReadLine());
        return columnNum;
    }
}

//Application Exception Class to handle column number outside the range of 1-7 
class MyColumnNumberException : ApplicationException
{
    public MyColumnNumberException(string message) : base(message) 
    {

    }
}

// class Model has all fields and methods that belong to the game/board. That is why they are all static.
public static class Model
{
    public static List<Player> _playersList; //Defining the list of objects of type Player
    
    public static int _turn; //This variable will define players turn and is related to the position of the player in the list
    
    public static char[,] _board = new char[6, 7]; //Creating an array to represent the board

    public static void StartGame() //This method will set turn to zero and all fields of the board to '#' to start or restart the game
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

    public static void DisplayBoard() //This method will display the board on the console
    {
        Console.WriteLine();
        Console.WriteLine("   Game Board    ");
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
        Console.WriteLine();
    }
}

//interface IGame defines methods related to a game to be completed in the class that implements it
public interface IGame
{
    bool CheckWinner();
    bool CheckGameOver();
    void Play();
}

//class Controller will play the game 
class Controller : IGame //class Controller implements interface IGame
{
    public bool CheckWinner() //This method checks if there is a connected 4 completed (horizontal, vertical or diagonally)
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

        //Diagonal Down check
        for (int i = 0; i < Model._board.GetLength(0) - 3; i++)
        {
            for (int j = 0; j < Model._board.GetLength(1) - 3; j++)
            {
                if (Model._board[i, j] == Model._playersList[Model._turn].Symbol && Model._board[i + 1, j + 1] == Model._playersList[Model._turn].Symbol && Model._board[i + 2, j + 2] == Model._playersList[Model._turn].Symbol && Model._board[i + 3, j + 3] == Model._playersList[Model._turn].Symbol)
                {
                    Console.WriteLine($"It is a Connect 4. {Model._playersList[Model._turn].Name} wins!");
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckGameOver() //This method checks if the board is already completed (return false) or not (return true)
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

        Console.WriteLine("Gameover!");
        return true;
    }


    public void Play()
    {
        string namePlayer1, namePlayer2;

        Console.WriteLine("Connect 4 Game");
        Console.WriteLine();

        //Getting players names through the console
        Console.WriteLine("Enter first player's name: ");
        namePlayer1 = Console.ReadLine();
        Console.WriteLine("Enter second player's name: ");
        namePlayer2 = Console.ReadLine();

        //Creating players objects
        Player player1 = new HumanPlayer(namePlayer1);
        Player player2 = new HumanPlayer(namePlayer2);

        //Defining players symbols
        player1.Symbol = 'X';
        player2.Symbol = 'O';

        //Initializing the list of players
        Model._playersList = [player1, player2];

        Model.StartGame();
        Model.DisplayBoard();

        int columnNum;
        int restart;

        while (true)
        {
            Console.WriteLine($"{Model._playersList[Model._turn].Name}, enter column number (1-7)."); //Ask the column number to the player according to the turn value
            columnNum = int.Parse(Console.ReadLine()); //save the chosen number

            //Application exception 
            if (columnNum < 1 || columnNum > 7)
            {
                try
                {
                    throw new MyColumnNumberException("Invalid column number!"); //throw exception when the user enters a number out of the range
                }
                catch(MyColumnNumberException me)
                {
                    Console.WriteLine(me.Message);
                    Console.WriteLine();
                }
            }

            else //Player enter a number from 1 to 7
            {
                for (int i = Model._board.GetLength(0)-1; i >=0 ; i--) //Runs the rows from the botton to the top of the board
                {
                    if (Model._board[i, columnNum-1]=='#') //Finds the empty spot
                    {
                        Model._board[i, columnNum - 1] = Model._playersList[Model._turn].Symbol; //replace # with the player's symbol
                        Model.DisplayBoard(); //Display board up to date
                        if (CheckWinner()) //Runs if the player wins
                        {
                            //Ask if players want to play again
                            Console.WriteLine("Restart? Yes(1) No (0):");
                            restart = int.Parse(Console.ReadLine());
                            if(restart == 1) //Restart the game
                            {
                                Console.Clear();
                                Console.WriteLine("\x1b[3J"); //Clear the console fully and reset the cursor to the top

                                Console.WriteLine("Connect 4 Game");
                                Model.StartGame();
                                Model.DisplayBoard();
                                break;
                            }
                            else //Any input except 1 will end the game
                            {
                                return;
                            }
                        }
                        else 
                        {
                            if(CheckGameOver()) //Runs if the board was completed in this round
                            {
                                //Ask if players want to play again
                                Console.WriteLine("Restart? Yes(1) No (0):");
                                restart = int.Parse(Console.ReadLine());
                                if (restart == 1) //Restart the game
                                {
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J"); //Clear the console fully and reset the cursor to the top

                                    Console.WriteLine("Connect 4 Game");
                                    Model.StartGame();
                                    Model.DisplayBoard();
                                    break;
                                }
                                else //Any input except 1 will end the game
                                {
                                    return;
                                }
                            }
                            else //Update the turn to call the next player
                            {
                                Model._turn = (Model._turn + 1) % Model._playersList.Count;
                                break;
                            }
                        }
                    }
                    else if(i==0) //If the last row was checked and none '#' was found, there is no more room in this column. So, the player must choose another column number.
                    {
                        Console.WriteLine("Completed column. Choose another column number.");
                        Console.WriteLine();
                    }
                }
            }
            
        }
    }
}


class Program
{
    static void Main(string[] args)
    {       
        Controller controller = new Controller();
        controller.Play();
    }
}
