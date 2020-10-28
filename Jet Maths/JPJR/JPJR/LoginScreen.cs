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
    class LoginScreen
    {
        DatabaseConnection dbc;
        NewAccount newAccountPage;
        Texture2D Logo;
        TextBox UserName;
        TextBox Password;
        SpriteBatch sBatch;
        int boxChangeBuffer;
        Button Login;
        Button newA;
        bool MakingNewA;
        UserInfo UI;

        public LoginScreen(ref SpriteBatch isBatch, ContentManager Content, ref DatabaseConnection DBC, ref UserInfo ui)
        {
            UI = ui;
            dbc = DBC;
            MakingNewA = false;
            Login = new Button(ref isBatch, Content, "loginButton", "loginButtonS", false, new Rectangle(1200, 800, 225, 150));
            newA = new Button(ref isBatch, Content, "NewAButton", "NewAButtonS", false, new Rectangle(1500, 800, 225, 150));

            sBatch = isBatch;
            Logo = Content.Load<Texture2D>("SplashLogo");
            boxChangeBuffer = 0;
            UserName = new TextBox("User Name", 16, false, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 500), new Vector2(400, 50), true, 5, false, false);
            Password = new TextBox("Password", 16, true, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 700), new Vector2(400, 50), false, 8, true, true);
            newAccountPage = new NewAccount(ref sBatch, Content, ref dbc, ref UI);
            UI = newAccountPage.GetUI();
        }

        public string UpdateLogin()
        {
            if (!MakingNewA)
            {
                KeyboardState KeyDown = Keyboard.GetState();
                if (boxChangeBuffer == 0)
                {
                    if (UserName.getSelected() && KeyDown.IsKeyDown(Keys.Down))
                    {
                        UserName.setSelected(false);
                        Password.setSelected(true);
                        boxChangeBuffer++;
                    }
                    else if (Password.getSelected() && KeyDown.IsKeyDown(Keys.Up))
                    {
                        Password.setSelected(false);
                        UserName.setSelected(true);
                        boxChangeBuffer++;
                    }
                    else if ((Password.getSelected() || UserName.getSelected()) && KeyDown.IsKeyDown(Keys.Right))
                    {
                        UserName.setSelected(false);
                        Password.setSelected(false);
                        Login.setSelected(true);
                        boxChangeBuffer++;
                    }
                    else if (Login.getSelected() && KeyDown.IsKeyDown(Keys.Left))
                    {
                        Login.setSelected(false);
                        UserName.setSelected(true);
                        boxChangeBuffer++;
                    }
                    else if (Login.getSelected() && KeyDown.IsKeyDown(Keys.Right))
                    {
                        Login.setSelected(false);
                        newA.setSelected(true);
                        boxChangeBuffer++;
                    }
                    else if (newA.getSelected() && KeyDown.IsKeyDown(Keys.Left))
                    {
                        newA.setSelected(false);
                        Login.setSelected(true);
                        boxChangeBuffer++;
                    }
                }

                if (boxChangeBuffer != 0)
                {
                    boxChangeBuffer++;
                    if (boxChangeBuffer >= 15)
                    {
                        boxChangeBuffer = 0;
                    }
                }


                UserName.UpdateTextBox();
                Password.UpdateTextBox();


                if (newA.UpdateButton())
                {
                    MakingNewA = true;
                    newA.setSelected(false);
                    UserName.setSelected(true);
                }


                if (Login.UpdateButton())
                {
                    if(dbc.CheckLogin(UserName.getCurrentText(), Password.getCurrentText()) == true)
                    {
                        return "bezzin";
                        //dbc.getAll();
                    }
                    else
                    {
                        IncorrectLogin();
                        return "X";
                    }
                }
                else
                {
                    return "not pressed";
                }
            }
            else
            {

                if (newAccountPage.Update())
                {
                    MakingNewA = false;
                }

                return "X";
            }
        }

        public void DrawLogin()
        {

            sBatch.Draw(Logo, new Vector2(20, 20), Color.White);
            if (!MakingNewA)
            {
                UserName.DrawTextBox();
                Password.DrawTextBox();
                Login.drawButton();
                newA.drawButton();
            }
            else
            {
                newAccountPage.Draw();
            }
        }

        private void IncorrectLogin()
        {

        }
    }
}
