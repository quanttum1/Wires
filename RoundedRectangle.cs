using SFML.System;
using SFML.Graphics;
using System;

public class RoundedRectangle : Drawable
{
    private VertexArray vertices;
    private float width;
    private float height;
    private float roundness;
    private int pointCount;
    private Vector2f position;
    private Color color;

    public RoundedRectangle(float width, float height, float roundness, int pointCount = 30)
    {
        this.width = width;
        this.height = height;
        this.roundness = Math.Clamp(roundness, 0f, 1f);
        this.pointCount = pointCount;
        this.position = new Vector2f(0, 0);
        this.color = Color.White;
        this.vertices = new VertexArray(PrimitiveType.TriangleFan); // Initialize vertices
        CreateVertices();
    }

    private void CreateVertices()
    {
        vertices.Clear();

        // Calculate the maximum possible radius
        float maxRadius = Math.Min(width, height) * roundness;

        // Top-left corner
        AddCornerVertices(new Vector2f(maxRadius, maxRadius), 180f, 270f, maxRadius);
        // Top side
        AddSideVertices(new Vector2f(maxRadius, 0), new Vector2f(width - maxRadius, 0));
        // Top-right corner
        AddCornerVertices(new Vector2f(width - maxRadius, maxRadius), 270f, 360f, maxRadius);
        // Right side
        AddSideVertices(new Vector2f(width, maxRadius), new Vector2f(width, height - maxRadius));
        // Bottom-right corner
        AddCornerVertices(new Vector2f(width - maxRadius, height - maxRadius), 0f, 90f, maxRadius);
        // Bottom side
        AddSideVertices(new Vector2f(width - maxRadius, height), new Vector2f(maxRadius, height));
        // Bottom-left corner
        AddCornerVertices(new Vector2f(maxRadius, height - maxRadius), 90f, 180f, maxRadius);
        // Left side
        AddSideVertices(new Vector2f(0, height - maxRadius), new Vector2f(0, maxRadius));
    }

    private void AddCornerVertices(Vector2f center, float startAngle, float endAngle, float radius)
    {
        float increment = (endAngle - startAngle) / pointCount;
        for (float angle = startAngle; angle <= endAngle; angle += increment)
        {
            float radian = angle * (float)Math.PI / 180f;
            float x = center.X + radius * (float)Math.Cos(radian);
            float y = center.Y + radius * (float)Math.Sin(radian);
            vertices.Append(new Vertex(new Vector2f(x, y), color));
        }
    }

    private void AddSideVertices(Vector2f start, Vector2f end)
    {
        vertices.Append(new Vertex(start, color));
        vertices.Append(new Vertex(end, color));
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        Transform transform = Transform.Identity;
        transform.Translate(position);
        states.Transform *= transform;
        target.Draw(vertices, states);
    }

    public Vector2f Position
    {
        get { return position; }
        set { position = value; }
    }

    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            for (uint i = 0; i < vertices.VertexCount; i++)
            {
                Vertex v = vertices[i];
                v.Color = color;
                vertices[i] = v;
            }
        }
    }
}

