using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

internal class ScaledSprite : Sprite
{
    private Rectangle Rect;
    public Vector2 Position
    {
        get => position;
        set
        {
            position = value;
            Rect.X = (int) value.X;
            Rect.Y = (int) value.Y;
        }
    }

    public ScaledSprite(Texture2D texture, Vector2 position, float scaleFactor)
        : base(texture, position)
    {
        Rect = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scaleFactor), (int)(texture.Height * scaleFactor));
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, Rect, Color.White);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}