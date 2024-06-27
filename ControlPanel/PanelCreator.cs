using SFML.Graphics;

namespace Wires.ControlPanel;

public class PanelCreator
{
    static public Panel Create(RenderTexture texture, RenderWindow window)
    {
        Panel panel = new Panel(texture, window);

        panel[-1] = new TextureButton("./assets/day-night-icon-dark.png");

        return panel;
    }
}
