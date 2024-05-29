using SFML.Graphics;

namespace Wires.CellularAutomatons;

// TODO: Make darkThemeColor and lightThemeColor private and make a field Color that will get the value using the future class ColorTheme
class CellType 
{
    public Color darkThemeColor { get; private set; }
    public Color lightThemeColor { get; private set; }
    public object cell { get; private set; } // Contains information about cell and will be downcasted by CellularAutomaton

    public CellType(Color ltc, Color dtc, object c)
    {
        darkThemeColor = dtc;
        lightThemeColor = ltc;
        cell = c;
    }
}
