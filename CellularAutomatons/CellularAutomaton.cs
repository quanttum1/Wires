using System.Numerics;

using SFML.Graphics;

using Wires.ControlPanel;

namespace Wires.CellularAutomatons;

public abstract class CellularAutomaton {
  
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

  protected abstract CellType[] _cellTypes { get; }

  public bool IsDragable { get; protected set; } = true;

  // Index of the selected cell within the CellTypes array
  protected uint _cellTypeSelected { get; set; } = 0; 

  public PanelButton[] CellButtons
  {
      get
      {
          List<PanelButton> buttons = _cellTypes.Select((cell, index) => (PanelButton)new ColoredButton(
                      cell.lightThemeColor,
                      () => {
                        _cellTypeSelected = (uint)index;
                        IsDragable = false;
                      }
                    )).ToList(); // TODO: Refactoring
          buttons.Insert(0, new TextureButton("./assets/move-icon-dark.png", () => IsDragable = true));
          
          return buttons.ToArray();
      }
  }

  public abstract void FillCell(int x, int y);

  public abstract void Update();
}
