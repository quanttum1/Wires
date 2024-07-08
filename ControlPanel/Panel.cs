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
    private RenderTexture _texture;
    public RenderTexture Texture
    {
        get
        {
            return _texture;
        }
        set
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].Target = value;
            }
            _texture = value;
        }
    }
    List<PanelButton> _buttons;
}
