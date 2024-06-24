using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Wires.ControlPanel;

// This is a test class to be deleted in further versions
public class SimpleButton : PanelButton
{
    protected override Texture GetTexture(float size) 
    {
        RenderTexture button = new RenderTexture((uint)size, (uint)size);

        RectangleShape coloredRect = new RectangleShape(new Vector2f(size, size));
        coloredRect.FillColor = _color;

        button.Draw(coloredRect);
        button.Display();

        return button.Texture;
    }

    public SimpleButton(Color buttonColor)
    {
        _color = buttonColor;
    }

    public SimpleButton() : this(Color.Black) {}


    private Color _color;
}
