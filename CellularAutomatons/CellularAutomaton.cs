using System.Numerics;
using SFML.Graphics;

namespace Wires.CellularAutomatons;

abstract class CellularAutomaton {
  
  public class InvalidCellGivenException : System.Exception
  {
      public InvalidCellGivenException() { }
      public InvalidCellGivenException(string message) : base(message) { }
      public InvalidCellGivenException(string message, System.Exception inner) : base(message, inner) { }
  }

  // TODO: Move to Config
  protected readonly Color defaultDarkColor = new Color(0x00004FFF); // #00004F
  protected readonly Color defaultLightColor = new Color(0xFFFFFFFF); // #FFFFFF

  public abstract Color GetCellColor(Vector2 index);

  public abstract CellType[] CellTypes { get; }

  public abstract object fillCell(int x, int y, object cell); // Returns a new cell to be overrided instead of the previous one

  public abstract void Update();
}
