namespace Wires.CellularAutomatons;

abstract class CellularAutomaton {
  
  public class InvalidCellGivenException : System.Exception
  {
      public InvalidCellGivenException() { }
      public InvalidCellGivenException(string message) : base(message) { }
      public InvalidCellGivenException(string message, System.Exception inner) : base(message, inner) { }
  }

  protected readonly (int, int, int) defaultDarkColor = (0, 0, 79);
  protected readonly (int, int, int) defaultLightColor = (255, 255, 255);

  public abstract (int, int, int) GetCellColor(int x, int y, bool lightTheme);

  public abstract CellType[] CellTypes { get; }

  public abstract void fillCell(int x, int y, object cell);

  public abstract void Update();
}
