using System.Numerics;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

using Wires.CellularAutomatons;

namespace Wires;

class Field {
    public Field()
    {
        _window = Window.Open();        
        _window.Closed += (sender, e) => _window.Close();

        Vector2i pmp = Mouse.GetPosition(_window);
        _previousMousePosition = new Vector2(pmp.X, pmp.Y);
    }

    public void Run() 
    { 
        object cellToFill = true;

        CellularAutomaton ca = new GameOfLife();

        _window.MouseMoved += MouseMoveHandler;
        _window.MouseWheelScrolled += MouseWheelScrollHandler;

        int cellSize;
        while (_window.IsOpen)
        {
            cellSize = (int)(_window.Size.X < _window.Size.Y ? _window.Size.X : _window.Size.Y); // Takes width or heigt, whatever is smaller
            cellSize /= Config.CellCount; // Divides on the number of cell that is supposed to be displayed
            cellSize = (int)(cellSize * _scaleFactor); // And multiplies on the scale factor

            _window.DispatchEvents();
            _window.Clear(new Color(127, 127, 127, 255)); // TODO: Move to config and let the cellular automaton set the color

            Color currentCellColor;
            Vector2 absoluteCellIndex = new Vector2(); // The "number" of cell, cells-neighbours differ in one digit
            Vector2 cellPosition = new Vector2(); // The position of the cell within the window

            // Loop goes through all the cell that are going to be displayed
            // Have to add 2 for x and 3 for y, because when the field is not aligned, an extra cell is required
            // And with only 1 or 2 extra cell there might be glitches at the bottom and the right side of the window
            for (int i = 0; i < _window.Size.X / cellSize + 2; i++) 
            {
                for (int j = 0; j < _window.Size.Y / cellSize + 2; j++)
                {
                    absoluteCellIndex.X = i + (int)_offset.X / cellSize;
                    absoluteCellIndex.Y = j + (int)_offset.Y / cellSize;

                    currentCellColor = ca.GetCellColor(absoluteCellIndex);

                    cellPosition.X = (i - 1) * cellSize - (_offset.X % cellSize);
                    cellPosition.Y = (j - 1) * cellSize - (_offset.Y % cellSize);

                    RectangleShape cell = new RectangleShape();
                    cell.Position = new Vector2f(cellPosition.X, cellPosition.Y);
                    cell.Size = new Vector2f(cellSize - Config.GapBetweenCells, cellSize - Config.GapBetweenCells); // -1 to make a small gap
                    cell.FillColor = new Color(currentCellColor);

                    _window.Draw(cell);
                }
            }
            _window.Display();

            
            // if (Raylib.IsMouseButtonPressed(MouseButton.Left) /* || Raylib.IsMouseButtonDown(MouseButton.Left) */)
            // {
            //     Vector2 mousePosition = Raylib.GetMousePosition();
            //     Vector2 cellCoord = mousePosition - offset;
            //
            //     if (cellCoord.X > 0)
            //     {
            //         cellCoord.X += cellSize;
            //     }
            //
            //     if (cellCoord.Y > 0)
            //     {
            //         cellCoord.Y += cellSize;
            //     }
            //
            //     cellToFill = ca.fillCell((int)cellCoord.X / cellSize, (int)cellCoord.Y / cellSize, cellToFill);
            // }

            /* Raylib.EndDrawing(); */
        }

    }

    Vector2 _offset = new Vector2(0, 0);

    private int _scale = 0;
    private double _scaleFactor = 1;
    private void MouseWheelScrollHandler(object? sender, MouseWheelScrollEventArgs e)
    {
        if(_scale - (int)e.Delta > 0) _scale -= (int)e.Delta;
        _scaleFactor = 1.0 / (_scale * 0.1); // TODO: Get rid of magical number 0.1
    }

    private Vector2 _previousMousePosition;
    private void MouseMoveHandler(object? sender, MouseMoveEventArgs e)
    {
        Vector2 currentMousePosition = new Vector2(e.X, e.Y);
        if (Mouse.IsButtonPressed(Mouse.Button.Left))
        {
            _offset -= currentMousePosition - _previousMousePosition;
        }
        _previousMousePosition = currentMousePosition;
    }

    private RenderWindow _window;
}
