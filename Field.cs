using System.Numerics;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

using Wires.CellularAutomatons;
using Wires.ControlPanel;

namespace Wires;

class Field {
    public Field()
    {
        _window = Window.Open();        
        _window.Closed += (sender, e) => _window.Close();

        _window.MouseMoved += MouseMoveHandler;
        _window.MouseWheelScrolled += MouseWheelScrollHandler;
        _window.MouseButtonPressed += MousePressHandler;

        Vector2i pmp = Mouse.GetPosition(_window);
        _previousMousePosition = new Vector2(pmp.X, pmp.Y);

        _ca = new GameOfLife();

        _panelTexture = new RenderTexture((uint)_window.Size.X, (uint)(_window.Size.Y / 15));
        _panel = PanelCreator.Create(_panelTexture, _window, _ca);
    }

    int _cellSize;
    public void Run() 
    {
        while (_window.IsOpen)
        {
            _window.SetView(new View(new FloatRect(0, 0, _window.Size.X, _window.Size.Y))); // To avoid build-in scale

            _panelTexture = new RenderTexture((uint)_window.Size.X, (uint)(_window.Size.Y / 15));
            _panel.Texture = _panelTexture;

            // Takes width or heigt, whatever is smaller
            _cellSize = (int)(_window.Size.X < _window.Size.Y ? _window.Size.X : _window.Size.Y); 
            _cellSize /= Config.CellCount; // Divides on the number of cell that is supposed to be displayed
            _cellSize = (int)(_cellSize * _scaleFactor); // And multiplies on the scale factor

            _window.DispatchEvents();
            _window.Clear(new Color(127, 127, 127, 255)); // TODO: Move to the config and let the cellular automaton set the color

            Color currentCellColor;
            Vector2 absoluteCellIndex = new Vector2(); // The "number" of cell, cells-neighbours differ in one digit
            Vector2 cellPosition = new Vector2(); // The position of the cell within the window

            // Loop goes through all the cell that are going to be displayed
            // Have to add 2 for x and y, because when the field is not aligned, an extra cell is required
            // But for some reason 2 is not always enough an have to add 3
            for (int i = 0; i < _window.Size.X / _cellSize + 3; i++) 
            {
                for (int j = 0; j < _window.Size.Y / _cellSize + 3; j++)
                {
                    absoluteCellIndex.X = i + (int)_offset.X / _cellSize;
                    absoluteCellIndex.Y = j + (int)_offset.Y / _cellSize;

                    currentCellColor = _ca.GetCellColor(absoluteCellIndex);

                    cellPosition.X = (i - 1) * _cellSize - (_offset.X % _cellSize);
                    cellPosition.Y = (j - 1) * _cellSize - (_offset.Y % _cellSize);

                    RectangleShape cell = new RectangleShape();
                    cell.Position = new Vector2f(cellPosition.X, cellPosition.Y);
                    cell.Size = new Vector2f(_cellSize - Config.GapBetweenCells, _cellSize - Config.GapBetweenCells);
                    cell.FillColor = new Color(currentCellColor);

                    _window.Draw(cell);
                }
            }

            _panel.Draw();

            RectangleShape panelRect = new RectangleShape(new Vector2f(_window.Size.X, _window.Size.Y / 15));
            panelRect.Position = new Vector2f(0, 0);
            panelRect.Texture = _panelTexture.Texture;

            _window.Draw(panelRect);

            _window.Display();
        }
    }

    Vector2 _offset = new Vector2(0, 0); // Offset due to dragging the map

    private int _scale = 10; // TODO: Create Config.InitialScale
    private double _scaleFactor = 1.0 / (10 * 0.1); // TODO: Replace 1 with Config.InitialScale
    private void MouseWheelScrollHandler(object? sender, MouseWheelScrollEventArgs e)
    {
        Vector2 zoomingPoint = new Vector2(e.X, e.Y) + _offset;
        double oldScaleFactor = _scaleFactor;
        
        if(_scale - (int)e.Delta > 0) _scale -= (int)e.Delta;
        _scaleFactor = 1.0 / (_scale * 0.1); // TODO: Get rid of magical number 0.1

        double scaleChanged = _scaleFactor / oldScaleFactor;
        Vector2 zoomingOffset = (zoomingPoint * (float)scaleChanged) - zoomingPoint;

        _offset += zoomingOffset;
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

    void MousePressHandler(object? sender, MouseButtonEventArgs e)
    {
        Vector2 clickPosition = new Vector2(e.X, e.Y);
        clickPosition += _offset;
        _ca.FillCell((int)clickPosition.X / _cellSize + 1, (int)clickPosition.Y / _cellSize + 1);
    }

    CellularAutomaton _ca;
    RenderTexture _panelTexture;
    Panel _panel;
    private RenderWindow _window;
}
