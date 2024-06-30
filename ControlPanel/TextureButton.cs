using SFML.System;
using SFML.Window;
using SFML.Graphics;

using Wires;

namespace Wires.ControlPanel;

public class TextureButton : PanelButton
{
    public TextureButton(string imagePath, Action onClick) : base(onClick)
    {
        _imagePath = imagePath;
    }

    protected override Texture GetTexture(float size)
    {
        RenderTexture texture = new RenderTexture((uint)size, (uint)size);
        texture.Clear(Color.Transparent);

        Texture image = new Texture(_imagePath);

        if (isHovered)
        {
            RoundedRectangle buttonBackground = new RoundedRectangle(size, size, Config.PanelButtonsRoundness);
            buttonBackground.Color = new Color(127, 127, 127, 100);
            texture.Draw(buttonBackground);
        }

        RectangleShape button = new RectangleShape(new Vector2f(size, size));
        button.Texture = image;
        texture.Draw(button);

        texture.Display();

        return texture.Texture;
    }

    private string _imagePath;
}
