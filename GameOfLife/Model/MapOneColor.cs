namespace GameOfLife.Model;

public class MapOneColor : Map
{
    public void NextState(Neighbours newNeighbours, Neighbours oldNeighbours)
    {
        int w = Size;

        Parallel.For(1, w - 1, x =>
        {
            for (int y = 1; y < w - 1; y++)
            {
                int pos = y * w + x;
                next[pos] = (byte)(
                    current[pos - w - 1] + current[pos - w] + current[pos - w + 1] +
                    current[pos - 1] + current[pos + 1] +
                    current[pos + w - 1] + current[pos + w] + current[pos + w + 1]);

                bool makeNewLife = current[pos] == 0 && newNeighbours.Create(next[pos]);
                bool keepAlive = current[pos] == 1 && oldNeighbours.Create(next[pos]);

                next[pos] = (byte)(makeNewLife | keepAlive ? 1 : 0);
            }
        });

        (current, next) = (next, current);
    }
}