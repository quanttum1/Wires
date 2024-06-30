using System.Numerics;
using System.Linq;
using SFML.Graphics;

namespace Wires.CellularAutomatons;

class GameOfLife : CellularAutomaton
{
    List<Vector2> aliveCells = new List<Vector2>();

    public override Color GetCellColor(Vector2 index) 
    {
        // TODO: I guess, there's no need to explain what's wrong with this code
        int x = (int)index.X;
        int y = (int)index.Y;
        bool lightTheme = true;

        var aliveColor = lightTheme ? defaultDarkColor : defaultLightColor;
        var deadColor = !lightTheme ? defaultDarkColor : defaultLightColor;

        if (aliveCells.Contains(new Vector2(x, y))) return aliveColor;
        return deadColor;
    }

    public override CellType[] CellTypes 
    {
        get {
            return [
                new CellType(defaultLightColor, defaultDarkColor, false), // dead
                new CellType(defaultDarkColor, defaultLightColor, true) // alive
            ];
        }
    }

    public override object FillCell(int x, int y, object cell) 
    {
        bool isAlive; // TODO: Rename to shouldBeAlive
        if (cell is bool _isAlive)
        {
            isAlive = (bool)_isAlive;
        } else
        {
            throw new InvalidCellGivenException("Invalid cell was given to fill for GameOfLife");
        }

        Vector2 cellCoord = new Vector2(x, y);
        bool isAlreadyAlive = aliveCells.Contains(cellCoord);

        if (isAlive)
        {
            if (!isAlreadyAlive) {
                aliveCells.Add(cellCoord);
            } else
            {
                aliveCells.Remove(cellCoord);
                return false;
            }
        } else
        {
            if (isAlreadyAlive) aliveCells.Remove(cellCoord);
        }
        return cell;
    }

    public override void Update() 
    {
        var newAliveCells = new HashSet<Vector2>();
        var cellsToCheck = new HashSet<Vector2>();

        foreach (var cell in aliveCells)
        {
            cellsToCheck.Add(cell);
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx != 0 || dy != 0)
                    {
                        cellsToCheck.Add(new Vector2(cell.X + dx, cell.Y + dy));
                    }
                }
            }
        }

        foreach (var cell in cellsToCheck)
        {
            int liveNeighbors = GetLiveNeighborsCount(cell);

            if (aliveCells.Contains(cell))
            {
                if (liveNeighbors == 2 || liveNeighbors == 3)
                {
                    newAliveCells.Add(cell);
                }
            }
            else
            {
                if (liveNeighbors == 3)
                {
                    newAliveCells.Add(cell);
                }
            }
        }

        aliveCells = newAliveCells.ToList();
    }

    private int GetLiveNeighborsCount(Vector2 cell)
    {
        int liveNeighbors = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx != 0 || dy != 0)
                {
                    if (aliveCells.Contains(new Vector2(cell.X + dx, cell.Y + dy)))
                    {
                        liveNeighbors++;
                    }
                }
            }
        }

        return liveNeighbors;
    }
}
