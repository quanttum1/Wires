using SFML.Graphics;

namespace Wires.ControlPanel;

public class PanelCreator
{
    static public Panel Create(RenderTexture texture, RenderWindow window)
    {
        Panel panel = new Panel(texture, window);

        panel[0] = new SimpleButton();
        panel[-1] = new SimpleButton(Color.Red);

        return panel;
    }
}
