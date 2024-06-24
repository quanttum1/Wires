namespace Wires.ControlPanel;

public class ButtonInitializedException : System.Exception
{
    public ButtonInitializedException() { }
    public ButtonInitializedException(string message) : base(message) { }
    public ButtonInitializedException(string message, System.Exception inner) : base(message, inner) { }
}
