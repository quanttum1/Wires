using SFML.Graphics;

namespace Wires.ControlPanel;

public class TextureButton : PanelButton
{
    public TextureButton(string imagePath) : base()
    {
        _imagePath = imagePath;
    }

    protected override Texture GetTexture(float size)
    {
        return new Texture(_imagePath);
    }

    private string _imagePath;
}
