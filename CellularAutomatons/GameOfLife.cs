using System.Numerics;
using System.Linq;
using SFML.Graphics;

namespace Wires.CellularAutomatons;

class GameOfLife : CellularAutomaton
{
    List<Vector2> aliveCells = new List<Vector2>();

    public override Color GetCellColor(Vector2 index) 
    {
        // TODO: make it normal, y'know
        int x = (int)index.X;
        int y = (int)index.Y;
        bool lightTheme = true;

        var aliveColor = lightTheme ? defaultDarkColor : defaultLightColor;
        var deadColor = !lightTheme ? defaultDarkColor : defaultLightColor;

        if (x == 1 && y == 1) return aliveColor;
        if (aliveCells.Contains(new Vector2(x, y))) return aliveColor;
        return deadColor;
    }

    public override CellType[] CellTypes 
    {
        get {
            return [
                new CellType(defaultLightColor, defaultDarkColor, false), // dead
                new CellType(defaultLightColor, defaultDarkColor, true) // alive
            ];
        }
    }

    public override object fillCell(int x, int y, object cell) 
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

    public override void Update() => throw new NotImplementedException();
}
