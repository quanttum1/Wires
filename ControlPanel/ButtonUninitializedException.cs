namespace Wires.ControlPanel;

public class ButtonUninitializedException : System.Exception
{
    public ButtonUninitializedException() { }
    public ButtonUninitializedException(string message) : base(message) { }
    public ButtonUninitializedException(string message, System.Exception inner) : base(message, inner) { }
}
