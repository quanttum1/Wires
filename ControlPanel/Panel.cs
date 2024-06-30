using System.Linq;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wires.ControlPanel;

public class Panel
{
    public Panel(RenderTexture texture, RenderWindow window)
    {
        _window = window;
        Texture = texture;
        _buttons = new List<PanelButton>();
    }

    public void Draw() {
        Texture.Clear(new Color(0xeeeeeeee));
        Texture.SetView(new View(new FloatRect(0, 0, Texture.Size.X, Texture.Size.Y)));

        foreach (var i in _buttons)
        {
            i.Draw();
        }

        Texture.Display();
    }

    public PanelButton this[int index]
    {
        get
        {
            return _buttons.Where(i => i.Index == index).ToList()[0];
        }
        set
        {
            _buttons.RemoveAll(i => i.Index == index);

            value.Index = index;
            value.Target = Texture;
            value.Window = _window;

            _buttons.Add(value);
        }
    }

    RenderWindow _window;
    public RenderTexture Texture; // Public, because need to update Texture when window is resized
    List<PanelButton> _buttons;
}
