using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Wires.ControlPanel;

abstract public class PanelButton
{ 
    public int NextPosition 
    {
        get 
        {
            return Index + 1;
        }
    }

    public int PreviousPosition
    {
        get
        {
            return Index - 1;
        }
    }

    // Does NOT call clear and display
    public void Draw()
    {
        float buttonSize = (float)(Target.Size.Y * 0.9);

        float xPosition = (Target.Size.Y * Index + Target.Size.X) % Target.Size.X;
        if (Index >= 0) xPosition += ButtonSize * Target.Size.Y;
        float yPosition = ButtonSize * Target.Size.Y;

        RectangleShape button = new RectangleShape(new Vector2f(buttonSize, buttonSize));
        button.Position = new Vector2f(
            xPosition,
            yPosition
        );

        button.Texture = GetTexture(buttonSize);
        Target.Draw(button);
    }

    protected abstract Texture GetTexture(float size);

    // Should be multiplied by Y size of panel
    // TODO: Rename to Gap or make 0.95f
    private const float ButtonSize = 0.05f;

    private RenderWindow? _window;
    public RenderWindow Window
    {
        
        private get
        {
            if (_window == null) throw new ButtonUninitializedException("Window is not initialized");
            return (RenderWindow)_window;
        }
        set
        {
            if (_window != null) throw new ButtonInitializedException("Window is already not initialized");
            _window = value;
        }
    }

    private RenderTarget? _target;
    public RenderTarget Target
    {
        private get
        {
            if (_target == null) throw new ButtonUninitializedException("Target is not initialized");
            return (RenderTarget)_target;
        }
        set
        {
            if (_target != null) throw new ButtonInitializedException("Target is already not initialized");
            _target = value;
        }
    }
 
    private int? _index;
    public int Index
    {
        get
        {
            if (_index == null) throw new ButtonUninitializedException("Index is not initialized");
            return (int)_index;
        }
        set
        {
            if (_index != null) throw new ButtonInitializedException("Index is already initialized");
            _index = value;
        }
    }
}
