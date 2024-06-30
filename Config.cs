namespace Wires;

// Use static fields if you need to get config data or create object and use set methods if you need to change config
// Object can be created only once for security reasons
class Config
{
    public static bool VerticalSync { get; private set; } = true;
    public static string WindowTitle { get; private set; } = "Wires";
    public static int CellCount { get; private set; } = 20; // The initial number of lines/columns of cells
    public static int GapBetweenCells { get; private set; } = 1; // Size of a gap between cells in pixels
    public static float PanelButtonsRoundness { get; private set; } = 0.3f;

    public void SetVerticalSync(bool newValue) => VerticalSync = newValue;
    public void SetWindowTitle(string newValue) => WindowTitle = newValue;
    public void SetCellCount(int newValue) => CellCount = newValue;
    public void SetGapBetweenCells(int newValue) => GapBetweenCells = newValue;
    public void SetPanelButtonsRoundness(float newValue) => PanelButtonsRoundness = newValue;

    public class ConfigAlreadyExistsException : System.Exception
    {
        public ConfigAlreadyExistsException() { }
        public ConfigAlreadyExistsException(string message) : base(message) { }
        public ConfigAlreadyExistsException(string message, System.Exception inner) : base(message, inner) { } 
    }

    private static bool isObjectCreated = false;
    public Config()
    {
        if (isObjectCreated) 
            throw new ConfigAlreadyExistsException("Cannot create more than one Conrig object for security reasons");
        isObjectCreated = true;
    }
}
