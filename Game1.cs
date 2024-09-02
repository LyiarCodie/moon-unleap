using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private KeyboardManager keyboard;
    private Player player;

    private ScaledSprite level;

    private List<Collider> collidersGroup = new List<Collider>();
    private List <Platform> oneWayPlatforms = new List<Platform>();

    private Dimensions2 windowSize;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 352;
        _graphics.PreferredBackBufferHeight = 256;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        keyboard = new KeyboardManager();

        windowSize = new Dimensions2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var playerSpr = Content.Load<Texture2D>("player/moon_slipper");
        
        collidersGroup.Add(new Collider(this, Vector2.Zero, 32, windowSize.Height, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(0, 96), 32 * 3, 32, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(0, 224), 32 * 6, 32, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(128, 160), 32 * 2, 32 * 2, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(128, 160), 32 * 6, 32, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(288, 128), 32 * 2, 32 * 2, Color.HotPink));
        collidersGroup.Add(new Collider(this, new Vector2(320, 32), 32, 32 * 4, Color.HotPink));

        oneWayPlatforms.Add(new Platform(this, Content, new Vector2(32,192), 2f, 2));
        oneWayPlatforms.Add(new Platform(this, Content, new Vector2(160, 96), 2f, 3));

        player = new Player(this, keyboard, playerSpr, new Vector2(150, 50), 2f, collidersGroup, oneWayPlatforms);
        level = new ScaledSprite(Content.Load<Texture2D>("level"), Vector2.Zero, 2f);
    }

    protected override void Update(GameTime gameTime)
    {
        keyboard.Update();

        player.Update(gameTime);

        keyboard.PostUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        level.Draw(_spriteBatch);

        foreach (var collider in collidersGroup)
        {
            collider.Draw(_spriteBatch);
        }

        foreach (var platform in oneWayPlatforms)
        {
            platform.Draw(_spriteBatch);
        }

        player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        foreach (var collider in collidersGroup)
        {
            collider.Dispose();
        }
        foreach (var platform in oneWayPlatforms)
        {
            platform.Dispose();
        }
        level.Dispose();
        player.Dispose();

        base.Dispose(disposing);
    }
}
