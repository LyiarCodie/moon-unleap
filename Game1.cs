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
    private Collider ground;

    private List<Collider> collidersGroup = new List<Collider>();

    private Dimensions2 windowSize;

    // [DllImport("SDL2.dll", CallingConvention = CallingConvention.Cdecl)]
    // public static extern void SDL_MaximizeWindow(IntPtr window);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // Window.AllowUserResizing = true;
        // Window.Position = new Point(100, 100);
    }

    protected override void Initialize()
    {
        // SDL_MaximizeWindow(Window.Handle);

        keyboard = new KeyboardManager();

        windowSize = new Dimensions2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var playerSpr = Content.Load<Texture2D>("player/moon_slipper");
        ground = new Collider(
            this, 
            new Vector2(64, windowSize.Height - 64), 
            32 * 16, 32, 
            Color.Black
        );
        collidersGroup.Add(ground);
        player = new Player(this, keyboard, playerSpr, new Vector2(200, 200), 2f, collidersGroup);

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
        player.Draw(_spriteBatch);
        ground.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        ground.Dispose();
        player.Dispose();

        base.Dispose(disposing);
    }
}
