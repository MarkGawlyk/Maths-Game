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
    class ChangeScreen
    {
        TextBox newField;
        Button ConfirmChanges;
        Button GoBack;
        string Title;
        SpriteFont font;
        SpriteBatch sBatch;
        int BBuffer;

        public ChangeScreen(string title, int Length, int minLength, bool needsCaps, bool needsNums, ref SpriteBatch isBatch, ContentManager Content)
        {
            newField = new TextBox(title, Length, false, ref isBatch, Content, "LoginBox", "LoginSelected", new Vector2(400, 600), new Vector2(400, 50), true, minLength, needsNums, needsCaps); 
            sBatch = isBatch;
            BBuffer = 1;
            Title = title;
        }
        public string updateChangeScreen()
        {
            if (BBuffer == 0)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (KeyDown.IsKeyDown(Keys.Down))
                {

                }
            }
            else
            {
                if (BBuffer >= 10)
                {
                    BBuffer = 0;
                }
                else
                {
                    BBuffer++;
                }
            }
                return "x";
        }
        public void drawChangeScreen()
        {
            sBatch.DrawString(font, Title, new Vector2(100, 700), Color.Black);
            ConfirmChanges.drawButton();
            GoBack.drawButton();
        }
    }
}
