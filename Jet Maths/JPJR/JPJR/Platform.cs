using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JPJR
{
    class Platform
    {
        int Width;
        int Height;
        int XValue;
        int YValue;
        float Rotation;
        Texture2D Texture;
        SpriteBatch sBatch;
        Rectangle PlatformR;
        //Constructor
        public Platform(int iWidth, int iHeight, float iRotation, int X, int Y, string ImageName, ref SpriteBatch isBatch, ContentManager content)
        {
            Width = iWidth;
            Height = iHeight;
            Rotation = iRotation;
            Texture = content.Load<Texture2D>(ImageName);
            sBatch = isBatch;
            XValue = X;
            YValue = Y;
            PlatformR = new Rectangle(XValue, YValue, Width, Height);
        }


        public void DrawPlatform()
        {
            sBatch.Draw(Texture, PlatformR, Color.White);
            //sBatch.Draw(Texture, PlatformR, null, Color.White, 0, new Vector2((PlatformR.X / 2), (PlatformR.Y / 2)), SpriteEffects.FlipVertically, 1);
        }

        public Rectangle GetRectangle()
        {
            return PlatformR;
        }

        public void SetRectangle(Rectangle r)
        {
            PlatformR = r;
        }

        public void IncreaseRectrangleX(int a)
        {
            PlatformR.X += a;
        }
    }
}
