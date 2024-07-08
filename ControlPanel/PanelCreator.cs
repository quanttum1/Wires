using SFML.Graphics;

using Wires.CellularAutomatons;

namespace Wires.ControlPanel;

public class PanelCreator
{
    static public Panel Create(RenderTexture texture, RenderWindow window, CellularAutomaton ca)
    {
        Panel panel = new Panel(texture, window); 
        
        for (int i = 0; i < ca.CellButtons.Length; i++)
        {
            panel[i] = ca.CellButtons[i];
        }

        panel[-1] = new TextureButton("./assets/step-button.png", () => ca.Update());

        return panel;
    }
}
