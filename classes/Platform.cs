using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

internal class Platform
{
    private Texture2D texture;
    private short widthMultiplier;

    public Collider collider;
    private Vector2 _position; 
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            collider.Position = new Vector2(value.X, value.Y);
        }
    }
    private float scaleFactor;

    private Rectangle[] platformTextureTile = new Rectangle[3];
    public Platform(Game1 game, ContentManager content, Vector2 position, float scaleFactor, short widthMultiplier)
    {
        collider = new Collider(game, position, 32 * widthMultiplier, 10, Color.Yellow);
        this.scaleFactor = scaleFactor;
        this.widthMultiplier = widthMultiplier;
        Position = position;
        texture = content.Load<Texture2D>("platform");
        for (int i = 0; i < platformTextureTile.Length; i++)
        {
            platformTextureTile[i] = new Rectangle(0 + (16 * i), 0, 16, 5);
        }
    }

    public void Draw(SpriteBatch sb)
    {
        for (short i = 0; i < widthMultiplier; i++)
        {

            if (i == 0)
            {
                sb.Draw(texture, Position, platformTextureTile[0], Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            }
            else if (i >= widthMultiplier - 1)
            {
                sb.Draw(texture, new Vector2(Position.X + (32 * i), Position.Y), platformTextureTile[2], Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            }
            else
            {
                sb.Draw(texture, new Vector2(Position.X + (32 * i), Position.Y), platformTextureTile[1], Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            }
        }
        collider.Draw(sb);
    }

    public void Dispose()
    {
        texture.Dispose();
        collider.Dispose();
    }
}