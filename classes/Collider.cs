using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

internal class Collider
{
    public Rectangle rect;

    private Color[] colorData;
    private Texture2D colliderTexture;
    private Color _color;
    private Color Color
    {
        get => _color;
        set
        {
            _color = value;
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = Color.Transparent;
            }
            // horizontal borders
            for (int i = 0; i < rect.Width; i++)
            {
                // top border
                colorData[i] = value;
                // bottom border
                colorData[(rect.Height * rect.Width) - rect.Width + i] = value;
            }
            // vertical borders
            for (int i = 0; i < rect.Height; i++)
            {
                // right border
                colorData[rect.Width - 1 + (rect.Width * i)] = value;
                // left border
                colorData[rect.Width * i] = value;
            }
            colliderTexture.SetData(colorData);
        }
    }

    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            rect.X = (int) value.X;
            rect.Y = (int) value.Y;
        }
    }
    public int Width { get => rect.Width; }
    public int Height { get => rect.Height; }
    public int Bottom { get => rect.Bottom; }
    public int Top { get => rect.Top; }
    public int Left { get => rect.Left; }
    public int Right { get => rect.Right; }

    public Collider(Game1 game, Vector2 position, int width, int height, Color color)
    {
        _position = position;
        rect = new Rectangle((int)position.X, (int)position.Y, width, height);

        colorData = new Color[rect.Width * rect.Height];
        colliderTexture = new Texture2D(game.GraphicsDevice, rect.Width, rect.Height);
        Color = color;
    }

    public bool Intersects(Rectangle other)
    {
        return rect.Intersects(other);
    }

    public bool IsTouching(float velocityX, float velocityY, Rectangle rect)
    {
        return Top + velocityY < rect.Bottom &&
                Right + velocityX > rect.Left &&
                Bottom + velocityY > rect.Top &&
                Left + velocityX < rect.Right;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(colliderTexture, _position, Color);
    }

    public void Dispose()
    {
        colliderTexture.Dispose();
    }
}