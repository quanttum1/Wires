using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wires;

public class Panel
{
    public Panel(RenderTexture texture, RenderWindow window)
    {
        _texture = texture;

        window.MouseMoved += (object? sender, MouseMoveEventArgs e) => {
            if (e.X < (_texture.Size.X - _texture.Size.Y * 0.95))
            {
                _isDarkThemeSwitchButtonHovered = false;
                return;
            }
            
            if (e.X > (_texture.Size.X - _texture.Size.Y * 0.95 + _texture.Size.Y * 0.9))
            {
                _isDarkThemeSwitchButtonHovered = false;
                return;
            }

            if (e.Y < (_texture.Size.Y * 0.05))
            {
                _isDarkThemeSwitchButtonHovered = false;
                return;
            }

            if (e.Y < (_texture.Size.Y * 0.95))
            {
                _isDarkThemeSwitchButtonHovered = false;
                return;
            }

            _isDarkThemeSwitchButtonHovered = true;
        };
    }

    public void Draw() {
        _texture.Clear(new Color(0xeeeeeeee));

        float buttonSize = (float)(_texture.Size.Y * 0.9);
        RectangleShape darkThemeSwitchButton = new RectangleShape(new Vector2f(buttonSize, buttonSize));
        darkThemeSwitchButton.Position = new Vector2f(
            (float)(_texture.Size.X - _texture.Size.Y * 0.95),
            (float)(_texture.Size.Y * 0.05)
        );
        darkThemeSwitchButton.Texture = new Texture(
            "./assets/day-night-icon-dark.png",
            new IntRect(
                0, 0,
                (int)(buttonSize/0.5),
                (int)(buttonSize/0.5)
            )
        );

        if (_isDarkThemeSwitchButtonHovered)
        {
            darkThemeSwitchButton.FillColor = new Color(0x666666ff);
        }

        _texture.Draw(darkThemeSwitchButton);

        _texture.Display();
    }

    bool _isDarkThemeSwitchButtonHovered = false;
    RenderTexture _texture;
}
