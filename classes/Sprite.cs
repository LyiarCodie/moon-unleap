using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

internal class Sprite
{
    public Texture2D texture;
    public Vector2 position;
    public Sprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public virtual void Update(GameTime gameTime) {}

    public virtual void Dispose()
    {
        texture.Dispose();
    }
}