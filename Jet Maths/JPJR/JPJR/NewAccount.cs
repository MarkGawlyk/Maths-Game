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
    class NewAccount
    {
        SpriteBatch sBatch;
        Button GoBack;
        Button Create;
        TextBox firstName;
        TextBox classCode;
        TextBox UserName;
        TextBox Password;
        int boxChangeBuffer;
        DatabaseConnection dbc;
        UserInfo UI;

        public NewAccount(ref SpriteBatch isBatch, ContentManager Content, ref DatabaseConnection DBC, ref UserInfo ui)
        {
            UI = ui;
            dbc = DBC;
            boxChangeBuffer = 0;
            sBatch = isBatch;
            firstName = new TextBox("Forename", 15, false, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 500), new Vector2(400, 50), true, 2, false, false);
            classCode = new TextBox("Class Code", 8, false, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 650), new Vector2(400, 50), false, 5, false, false);
            UserName = new TextBox("Username", 16, false, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 800), new Vector2(400, 50), false, 4, false, false);
            Password = new TextBox("Password", 16, false, ref sBatch, Content, "LoginBox", "LoginSelected", new Vector2(150, 950), new Vector2(400, 50), false, 8, true, true);
            GoBack = new Button(ref sBatch, Content, "GoBackBS", "GoBackB", false, new Rectangle(1500, 800, 230, 150));
            Create = new Button(ref sBatch, Content, "CreateB", "CreateBs", false, new Rectangle(1200, 800, 230, 150));
            firstName.setSelected(true);
        }

        public UserInfo GetUI()
        {
            return UI;
        }

        public bool Update()
        {
            firstName.UpdateTextBox();
            classCode.UpdateTextBox();
            UserName.UpdateTextBox();
            Password.UpdateTextBox();

            KeyboardState KeyDown = Keyboard.GetState();
            if (boxChangeBuffer == 0)
            {
                if (firstName.getSelected() && KeyDown.IsKeyDown(Keys.Down))
                {
                    firstName.setSelected(false);
                    classCode.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (classCode.getSelected() && KeyDown.IsKeyDown(Keys.Up))
                {
                    classCode.setSelected(false);
                    firstName.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (classCode.getSelected() && KeyDown.IsKeyDown(Keys.Down))
                {
                    classCode.setSelected(false);
                    UserName.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (UserName.getSelected() && KeyDown.IsKeyDown(Keys.Up))
                {
                    UserName.setSelected(false);
                    classCode.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (UserName.getSelected() && KeyDown.IsKeyDown(Keys.Down))
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
                else if ((Password.getSelected() || UserName.getSelected() || firstName.getSelected() || classCode.getSelected()) && KeyDown.IsKeyDown(Keys.Right))
                {
                    firstName.setSelected(false);
                    UserName.setSelected(false);
                    classCode.setSelected(false);
                    Password.setSelected(false);
                    Create.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (Create.getSelected() && KeyDown.IsKeyDown(Keys.Left))
                {
                    Create.setSelected(false);
                    firstName.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (Create.getSelected() && KeyDown.IsKeyDown(Keys.Right))
                {
                    Create.setSelected(false);
                    GoBack.setSelected(true);
                    boxChangeBuffer++;
                }
                else if (GoBack.getSelected() && KeyDown.IsKeyDown(Keys.Left))
                {
                    GoBack.setSelected(false);
                    Create.setSelected(true);
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

            if (Create.UpdateButton())
            {
                if(dbc.CreateAccount(UserName.getCurrentText(), firstName.getCurrentText(), Password.getCurrentText(), classCode.getCurrentText(), ref UI))
                {
                    Create.setSelected(false);
                    UserName.setSelected(true);
                    return true;
                }
                
            }

            if (GoBack.UpdateButton())
            {
                GoBack.setSelected(false);
                firstName.setSelected(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw()
        {
            firstName.DrawTextBox();
            UserName.DrawTextBox();
            Password.DrawTextBox();
            classCode.DrawTextBox();
            GoBack.drawButton();
            Create.drawButton();
        }



    }
}
