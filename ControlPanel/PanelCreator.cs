using SFML.Graphics;

using Wires.CellularAutomatons;

namespace Wires.ControlPanel;

public class PanelCreator
{
    static public Panel Create(RenderTexture texture, RenderWindow window, CellularAutomaton ca)
    {
        CellType[] cellTypes = ca.CellTypes;
        Panel panel = new Panel(texture, window);

        panel[0] = new TextureButton("./assets/move-icon-dark.png", () => System.Console.WriteLine("Not implemented"));

        for (int i = 0; i < cellTypes.Length; i++)
        {
            panel[i + 1] = new ColoredButton(cellTypes[i].lightThemeColor, () => System.Console.WriteLine("Not implemented"));
        }

        panel[-1] = new TextureButton("./assets/step-button.png", () => ca.Update());

        return panel;
    }
}
