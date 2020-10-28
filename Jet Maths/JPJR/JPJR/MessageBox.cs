using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace JPJR
{
    class MessageBox
    {
        string Title;
        string Text;
        bool IsShowing;
        Texture2D BoxT;
        Rectangle BoxR;
        SpriteBatch sBatch;
        SpriteFont TitleFont;
        SpriteFont TextFont;
        Vector2 TitlePos;
        Vector2 TextPos;

        public MessageBox(ref SpriteBatch spriteBatch, ContentManager Content, string title, string text, bool isShowing)
        {
            TitlePos = new Vector2(((1920 / 2) - 8 * title.Length), 210);
            TextPos = new Vector2(570, 260);
            sBatch = spriteBatch;
            BoxR = new Rectangle(560, 200, 800, 700);
            BoxT = Content.Load<Texture2D>("MessageBox");
            Title = title;
            Text = text;
            IsShowing = isShowing;
            TitleFont = Content.Load<SpriteFont>("TitleFont");
            TextFont = Content.Load<SpriteFont>("TextFont");
        }

        public void UpdateMessage()
        {
            if (IsShowing)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (KeyDown.IsKeyDown(Keys.Space))
                {
                    IsShowing = false;
                }
            }
        }

        public void DrawMessage()
        {
            if (IsShowing)
            {
                sBatch.Draw(BoxT, BoxR, Color.White);
                sBatch.DrawString(TitleFont, Title, TitlePos, Color.Black);
                sBatch.DrawString(TextFont, Text, TextPos, Color.Black);
            }
        }

        public void setText(string NewText)
        {
            Text = NewText;
        }
        public void setTitle(string NewTitle)
        {
            Title = NewTitle;
        }
        public void setIsShowing(bool Showing)
        {
            IsShowing = Showing;
        }
    }
}
