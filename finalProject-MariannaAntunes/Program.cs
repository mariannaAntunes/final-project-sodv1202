public abstract class Player
{ 
    public string Name { get; set; }
    protected static int _turn;

    static Player()
    {
        _turn = 0;
    }

    public abstract void playerType();
}



