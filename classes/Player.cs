using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace moon_unleap;

internal class Player : Sprite
{
    private List<Collider> collidersGroup = new List<Collider>();

    private float scaleFactor;
    private float gravity = 9.8f;
    private float jumpForce = -200f;
    private bool onFloor = false;
    private SpriteEffects flipX;

    private enum States
    {
        Idle,
        Move
    }
    private States currentState = States.Idle;

    private Vector2 velocity;
    public Collider collider;

    public KeyboardManager keyboard;

    public Vector2 Position
    {
        get => position; 
        set
        {
            position = value;
            collider.Position = new Vector2(position.X + 5, position.Y + 2);
        }
    }

    private float moveSpeed = 200f;

    public Player(Game1 game, KeyboardManager keyboard, Texture2D texture, Vector2 position, float scaleFactor, List<Collider> collidersGroup)
    : base (texture, position)
    {
        collider = new Collider(
            game, 
            Position, 
            (int) ((texture.Width - 5) * scaleFactor), (int) ((texture.Height - 1) * scaleFactor), 
            Color.Lime
        );
        velocity = Vector2.Zero;
        this.keyboard = keyboard;
        this.scaleFactor = scaleFactor;
        this.collidersGroup = collidersGroup;
        flipX = SpriteEffects.None;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        velocity.X = keyboard.GetAxis().X * moveSpeed * dt;

        if (velocity.Y < 10f) {
            velocity.Y += gravity * dt;
        }

        // horizontal collision
        foreach (var collider in collidersGroup)
        {
            if (this.collider.IsTouching(velocity.X, 0, collider.rect) && velocity.X != 0)
            {
                velocity.X = 0f;
            }
        }
        position.X += velocity.X;

        // vertical collision
        foreach (var collider in collidersGroup)
        {
            if (this.collider.IsTouching(0, velocity.Y, collider.rect) && velocity.Y != 0)
            {
                velocity.Y = 0f;
            }

            onFloor = this.collider.Bottom >= collider.Top;
        }
        position.Y += velocity.Y;

        if (keyboard.GetButtonPressed("Jump") > 0f && onFloor)
        {
            velocity.Y = jumpForce * dt;
        }

        Position = position;

        switch (currentState)
        {
            case States.Idle:
                if (keyboard.GetAxis() != Vector2.Zero)
                {
                    currentState = States.Move;
                }
            break;
            case States.Move:
                if (velocity.X < 0) { flipX = SpriteEffects.FlipHorizontally; }
                else if (velocity.X > 0) { flipX = SpriteEffects.None; }

                if (keyboard.GetAxis() == Vector2.Zero)
                {
                    currentState = States.Idle;
                }
            break;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scaleFactor, flipX, 0f);
        collider.Draw(sb);
    }
    public override void Dispose()
    {
       collider.Dispose();

       base.Dispose(); 
    }
}