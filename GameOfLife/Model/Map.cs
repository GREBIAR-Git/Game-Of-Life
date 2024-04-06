namespace GameOfLife.Model;

public abstract class Map
{
    public const int Size = 512;

    protected byte[] current = new byte[Size * Size];
    protected byte[] next = new byte[Size * Size];

    public byte this[int index]
    {
        get => current[index];
        set => current[index] = value;
    }

    public void Clear()
    {
        current = new byte[Size * Size];
        next = new byte[Size * Size];
    }

    public void SetRandom(int max = 2)
    {
        Random random = new();
        for (int i = 1; i < Size - 1; i++)
        {
            for (int j = 1; j < Size - 1; j++)
            {
                current[j * Size + i] = (byte)random.Next(max);
            }
        }
    }

    public void Block()
    {
        current[Size / 2 * Size + Size / 2] = 1;
        current[Size / 2 * Size + Size / 2 + 1] = 1;
        current[(Size / 2 + 1) * Size + Size / 2] = 1;
        current[(Size / 2 + 1) * Size + Size / 2 + 1] = 1;
    }

    public void Beacon()
    {
        current[Size / 2 * Size + Size / 2] = 1;
        current[Size / 2 * Size + Size / 2 + 1] = 1;
        current[(Size / 2 + 1) * Size + Size / 2] = 1;
        current[(Size / 2 + 1) * Size + Size / 2 + 1] = 1;

        current[(Size / 2 + 2) * Size + Size / 2 + 2] = 1;
        current[(Size / 2 + 2) * Size + Size / 2 + 3] = 1;
        current[(Size / 2 + 3) * Size + Size / 2 + 2] = 1;
        current[(Size / 2 + 3) * Size + Size / 2 + 3] = 1;
    }
}