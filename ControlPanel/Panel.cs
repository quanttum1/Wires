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
        _texture = texture;
        _buttons = new List<PanelButton>();
    }

    public void Draw() {
        _texture.Clear(new Color(0xeeeeeeee));
        _texture.SetView(new View(new FloatRect(0, 0, _texture.Size.X, _texture.Size.Y)));

        foreach (var i in _buttons)
        {
            i.Draw();
        }

        _texture.Display();
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
            value.Target = _texture;
            value.Window = _window;

            _buttons.Add(value);
        }
    }

    RenderWindow _window;
    RenderTexture _texture;
    List<PanelButton> _buttons;
}
