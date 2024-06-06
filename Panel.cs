using SFML.Graphics;

namespace Wires;

public class Panel
{
    public Panel(RenderTexture texture)
    {
        _texture = texture;
    }

    public void Draw() {
        _texture.Clear(new Color(0xeeeeeeee));
        _texture.Display();
    }

    RenderTexture _texture;
}
