using SFML.Graphics;
using SFML.Window;

namespace Wires;

class Window
{
    static public RenderWindow Open() {
        VideoMode mode = VideoMode.DesktopMode; // With FullscreenModes[0] I get weird bugs
        RenderWindow window = new RenderWindow(mode, Config.WindowTitle); // Won't be really fullscreen unless Styles.Fullscreen is given
        window.SetVerticalSyncEnabled(Config.VerticalSync);

        return window;
    }
}
