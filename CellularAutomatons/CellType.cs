namespace Wires.CellularAutomatons;

class CellType {
  public (int, int, int) darkThemeColor { get; private set; }
  public (int, int, int) lightThemeColor { get; private set; }
  public object cell { get; private set; } // Will be downcasted by CellularAutomaton

  public CellType((int, int, int) ltc, (int, int, int) dtc, object c)
  {
    darkThemeColor = dtc;
    lightThemeColor = ltc;
    cell = c;
  }
}
