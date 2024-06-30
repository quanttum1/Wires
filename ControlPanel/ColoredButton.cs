using SFML.System;
using SFML.Window;
using SFML.Graphics;

using Wires;

namespace Wires.ControlPanel;

// This is a test class to be deleted in further versions
public class ColoredButton : PanelButton
{
    protected override Texture GetTexture(float size) 
    {
        RenderTexture button = new RenderTexture((uint)size, (uint)size);

        button.Clear(Color.Transparent);

        RoundedRectangle outerRect = new RoundedRectangle(size, size, Config.PanelButtonsRoundness);
        if (!isHovered) outerRect.Color = new Color(_color.R, _color.G, _color.B, (byte)(_color.A / 2));
        else outerRect.Color = new Color((byte)(_color.R - 50), (byte)(_color.G - 50), (byte)(_color.B - 50), (byte)(_color.A / 2));
        button.Draw(outerRect);

        RoundedRectangle innerRect = new RoundedRectangle(size / 2, size / 2, Config.PanelButtonsRoundness);
        innerRect.Color = _color;
        innerRect.Position = new Vector2f(size / 4, size / 4);
        button.Draw(innerRect);

        button.Display();

        return button.Texture;
    }

    public ColoredButton(Color buttonColor, Action onClick) : base(onClick)
    {
        _color = buttonColor;
    }

    private Color _color;
}
