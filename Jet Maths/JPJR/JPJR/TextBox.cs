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
using System.Diagnostics;

namespace JPJR
{
    class TextBox
    {
        string BoxTitle;
        string CurrentText;
        bool BlockOutInput;
        int MaxLength;
        int MinLength;
        bool RequiresNumbers;
        bool RequiresCaps;
        bool HasCaps;
        bool HasNums;
        bool isMinLength;
        int numOfErrors;
        SpriteFont Font;
        SpriteBatch sBatch;
        KeyboardState OldState;
        Rectangle BoxR;
        Texture2D BoxT;
        Texture2D BoxTs;
        bool isSelected;

        public TextBox(string Title, int Length, bool HideInput, ref SpriteBatch isBatch, ContentManager Content, string BoxImageName, string SelectedBoxName, Vector2 Position, Vector2 Size, bool selected, int minLength, bool needsNumbers, bool needsCaptials)
        {
            numOfErrors = 0;
            isSelected = selected;
            BoxT = Content.Load<Texture2D>(BoxImageName);
            BoxTs = Content.Load<Texture2D>(SelectedBoxName);
            BoxR = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Font = Content.Load<SpriteFont>("AnswerFont");
            sBatch = isBatch;
            BoxTitle = Title;
            CurrentText = "";
            BlockOutInput = HideInput;
            MaxLength = Length;
            MinLength = minLength;
            RequiresCaps = needsCaptials;
            RequiresNumbers = needsNumbers;
            if (RequiresCaps)
            {
                HasCaps = false;
            }
            else
            {
                HasCaps = true;
            }
            if (RequiresNumbers)
            {
                HasNums = false;
            }
            else
            {
                HasNums = true;
            }
        }

        public void UpdateTextBox()
        {
            if (isSelected)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                char KeyChar;
                if (OldState != KeyDown)
                {
                    if (!(KeyDown.GetPressedKeys().Length == 0))
                    {
                        KeyChar = KeyDown.GetPressedKeys()[0].ToString()[0];
                        if (KeyDown.IsKeyDown(Keys.LeftShift))
                        {
                            if (KeyDown.GetPressedKeys().Length > 1)
                            {
                                if (KeyDown.GetPressedKeys()[0].ToString().Length == 1 && CurrentText.Length < MaxLength)
                                {

                                    CurrentText = string.Concat(CurrentText, KeyChar);
                                }
                            }
                        }
                        else if (KeyDown.IsKeyDown(Keys.Back))
                        {
                            if (CurrentText.Length > 0)
                            {
                                CurrentText = CurrentText.Substring(0, CurrentText.Length - 1);
                            }
                        }
                        else if (KeyDown.IsKeyDown(Keys.Space) && CurrentText.Length < MaxLength)
                        {
                            CurrentText = string.Concat(CurrentText, " ");
                        }
                        else if (KeyDown.GetPressedKeys()[0].ToString().Length == 1 && CurrentText.Length < MaxLength)
                        {
                            KeyChar = (char)(KeyChar + 32);
                            CurrentText = string.Concat(CurrentText, KeyChar);
                        }
                        else if (KeyDown.GetPressedKeys()[0].ToString().Length == 2 && KeyDown.GetPressedKeys()[0].ToString()[0] == 'D' && CurrentText.Length < MaxLength)
                        {
                            KeyChar = KeyDown.GetPressedKeys()[0].ToString()[1];
                            CurrentText = string.Concat(CurrentText, KeyChar);
                        }

                    }
                }
                OldState = KeyDown;
            }
            if (RequiresCaps == true || RequiresNumbers == true)
            {
                if (RequiresCaps)
                {
                    HasCaps = false;
                }
                if (RequiresNumbers)
                {
                    HasNums = false;
                }
                foreach (char c in CurrentText)
                {
                    if (c >= 65 && c <= 90)
                    {
                        HasCaps = true;
                    }
                    if (c >= 48 && c <= 57)
                    {
                        HasNums = true;
                    }
                }
            }
            isMinLength = false;
            if (CurrentText.Length >= MinLength)
            {
                isMinLength = true;
            }
        }

        public void DrawTextBox()
        {
            numOfErrors = 0;
            if (isSelected)
            {
                sBatch.Draw(BoxTs, BoxR, Color.White);
            }
            else
            {
                sBatch.Draw(BoxT, BoxR, Color.White);
            }
            DrawCurrentText();
            if (isSelected)
            {
                if (!HasCaps)
                {
                    numOfErrors += 1;
                    sBatch.DrawString(Font, "You need to use capitals.", new Vector2(575, (BoxR.Y - 5) + (numOfErrors - 1) * 60), Color.Red);
                }
                if (!HasNums)
                {
                    numOfErrors += 1;
                    sBatch.DrawString(Font, "You need to use numbers.", new Vector2(575, (BoxR.Y - 5) + (numOfErrors - 1) * 60), Color.Red);
                }
                if (!isMinLength)
                {
                    numOfErrors += 1;
                    sBatch.DrawString(Font, "You need more characters.", new Vector2(575, (BoxR.Y - 5) + (numOfErrors - 1) * 60), Color.Red);
                }
            }

            sBatch.DrawString(Font, BoxTitle, new Vector2(BoxR.X + 5, BoxR.Y - 70), Color.Gray);

        }

        public string getCurrentText()
        {
            return CurrentText;
        }

        public void ClearText()
        {
            CurrentText = " ";
        }
        private void DrawCurrentText()
        {
            string Hidden = string.Empty;
            if (CurrentText == null)
            {
                sBatch.DrawString(Font, BoxTitle, new Vector2(BoxR.X + 3, BoxR.Y), Color.LightGray);
            }
            else if (BlockOutInput)
            {
                for (int x = 0; x < CurrentText.Length; x++)
                {
                    Hidden = string.Concat(Hidden, '*');
                }
                sBatch.DrawString(Font, Hidden, new Vector2(BoxR.X + 3, BoxR.Y), Color.Black);
            }
            else
            {
                sBatch.DrawString(Font, CurrentText, new Vector2(BoxR.X + 3, BoxR.Y), Color.Black);
            }
        }

        public void setSelected(bool Selected)
        {
            isSelected = Selected;
        }

        public bool getSelected()
        {
            return isSelected;
        }
    }
}
