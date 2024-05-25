using Raylib_cs;
using System.Numerics;
using Wires.CellularAutomatons;

namespace Wires;


class Field {
  public Field()
  {
    return;
  }

  public void Run() {
    // TODO: Split Raylib methods into separate class
    // TODO: Refactoring
    // TODO: Normal scale
    Raylib.InitWindow(800, 480, "Wires");
    Raylib.SetWindowState(ConfigFlags.ResizableWindow);
    Raylib.MaximizeWindow();

    int cellCount = 50;
    Vector2 offset = new Vector2(0, 0);

    int scale = 10;
    double scaleFactor = 1;

    CellularAutomaton ca = new GameOfLife();

    

      while (!Raylib.WindowShouldClose())
      {
        int cellSize = Raylib.GetRenderWidth() < Raylib.GetRenderHeight() ?
          Raylib.GetRenderWidth() / (cellCount + 1) :
          Raylib.GetRenderHeight() / (cellCount + 1);
        cellSize = (int)(cellSize * scaleFactor);

        
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
          offset += Raylib.GetMouseDelta();

        Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(127, 127, 127, 255));

        

        Color defaultColor = new Color(0, 0, 79, 255);
        (int, int, int) color;
        for (int i = 0; i < Raylib.GetRenderWidth() / cellSize + 3; i++)
        {
          for (int j = 0; j < Raylib.GetRenderHeight() / cellSize + 3; j++)
          {
            color = ca.GetCellColor(i - (int)offset.X / cellSize, j - (int)offset.Y / cellSize, false);

            Raylib.DrawRectangle(cellSize * (i-1) + (int)offset.X % cellSize, cellSize * (j-1) + (int)offset.Y % cellSize,
                cellSize - 1, cellSize - 1,
                new Color(color.Item1, color.Item2, color.Item3, 255));
          }
        }

        float scroll = Raylib.GetMouseWheelMove();

        if (scale - (int)scroll > 0) scale -= (int)scroll;

        scaleFactor = 1.0 / (scale * 0.1);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) /* || Raylib.IsMouseButtonDown(MouseButton.Left) */)
        {
          Vector2 mousePosition = Raylib.GetMousePosition();
          ca.fillCell((int)(mousePosition.X - offset.X) / cellSize + 1, (int)(mousePosition.Y - offset.Y) / cellSize + 1, true);
        }

        Raylib.EndDrawing();
      }

      Raylib.CloseWindow();
  }
}
