using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace moon_unleap;

internal class KeyboardManager
{
    private class Key
    {
        public Keys key = Keys.None;
        public bool press = false;
        public bool hold = false;
        public Key(Keys key) {
            this.key = key;
        }
    }

    private Key jumpKey = new Key(Keys.Space);

    public Vector2 GetAxis(float diagonalNormalizationLength = 1.25f)
    {
        KeyboardState kbstate = Keyboard.GetState();
        int left = kbstate.IsKeyDown(Keys.A) ? -1 : 0;
        int right = kbstate.IsKeyDown(Keys.D) ? 1 : 0;
        int up = kbstate.IsKeyDown(Keys.W) ? -1 : 0;
        int down = kbstate.IsKeyDown(Keys.S) ? 1 : 0;

        Vector2 direction = new Vector2(left + right, up + down);

        if (direction.X != 0 && direction.Y != 0)
        {
            direction /= diagonalNormalizationLength;
        }
        
        return direction;
    }

    public float GetButtonPressed(string name)
    {
        if (name == "Jump")
        {
            return jumpKey.press ? 1f : 0f;
        }
        return 0f;
    }

    public float GetKeyDown(Keys key)
    {
        return Keyboard.GetState().IsKeyDown(key) ? 1f : 0f;
    }
    
    public void Update()
    {
        var kbstate = Keyboard.GetState();
        if (kbstate.IsKeyDown(jumpKey.key) && !jumpKey.hold)
        {
            jumpKey.hold = true;
            jumpKey.press = true;
        }

        if (kbstate.IsKeyUp(jumpKey.key) && jumpKey.hold)
        {
            jumpKey.hold = false;
            jumpKey.press = false;
        }
    }

    public void PostUpdate()
    {
        jumpKey.press = false;
    }
}