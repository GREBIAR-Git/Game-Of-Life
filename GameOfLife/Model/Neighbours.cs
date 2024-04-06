namespace GameOfLife.Model;

public class Neighbours
{
    public bool One { get; set; }

    public bool Two { get; set; }

    public bool Three { get; set; }

    public bool Four { get; set; }

    public bool Five { get; set; }

    public bool Six { get; set; }

    public bool Seven { get; set; }

    public bool Eight { get; set; }

    public bool Create(byte number)
    {
        switch (number)
        {
            case 1:
                return One;
            case 2:
                return Two;
            case 3:
                return Three;
            case 4:
                return Four;
            case 5:
                return Five;
            case 6:
                return Six;
            case 7:
                return Seven;
            case 8:
                return Eight;
            default:
                return false;
        }
    }
}
