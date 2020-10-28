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
    class Button
    {
        bool isSelected;
        Texture2D ButtonT;
        Texture2D ButtonTs;
        Rectangle ButtonR;
        SpriteBatch sBatch;

        public Button(ref SpriteBatch spriteBatch, ContentManager Content, string imgName, string SelectedImgName, bool selected, Rectangle rectangle)
        {
            sBatch = spriteBatch;
            isSelected = selected;
            ButtonR = rectangle;
            ButtonT = Content.Load<Texture2D>(imgName);
            ButtonTs = Content.Load<Texture2D>(SelectedImgName);
        }

        public bool UpdateButton()
        {
            if (isSelected)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (KeyDown.IsKeyDown(Keys.Enter))
                {
                    return true;
                }
            }
            return false;
        }
        public void drawButton()
        {
            if (isSelected)
            {
                sBatch.Draw(ButtonT, ButtonR, Color.White);
            }
            else
            {
                sBatch.Draw(ButtonTs, ButtonR, Color.White);
            }
        }

        public bool getSelected()
        {
            return isSelected;
        }
        public void setSelected(bool state)
        {
            isSelected = state;
        }
    }
}
