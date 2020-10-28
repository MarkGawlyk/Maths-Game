using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace JPJR
{
    class DatabaseConnection
    {
        //const string CONNECTION_STRING = @"Provider=SQLOLEDB; Data Source=.\SQLEXPRESS; Initial Catalog=MathsGame; User ID=sa; Password=Pa$$w0rd";
        const string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS; Initial Catalog=MathsGame; User ID=sa; Password=Pa$$w0rd";
        const string SQL_LOGIN_QUERY = @"SELECT * FROM UserLogin WHERE UserName = @username AND PasswordHash = @passwordhash";
        const string SQL_INSERT_NEWACCOUNT = @"INSERT INTO UserLogin VALUES (@username, @firstname, @classcode, @passwordhash, 0, 1, 0, 0)";
        //const string SQL_INSERT_DETAILS = @"INSERT INTO UserDetails VALUES(@username, 0, 1, 0, 0)";
        //const string SQL_UPDATE_DETAILS = @"UPDATE UserDetails SET HighScore = @highScore, CurrentLevel = @currentLevel, RecentScore = @recentScore, GamesPlayed = @gamesPlayed  WHERE UserName = @userName";
        const string SQL_UPDATE_LOGIN = @"UPDATE UserLogin SET FirstName = @firstName, ClassCode = @classCode, PasswordHash = @passwordHash, HighScore = @highScore, CurrentLevel = @currentLevel,  WHERE UserName = @userName";
        SqlConnection myConnection;
        SqlCommand myCommand;
        DataTable dataTable;
        SqlDataAdapter sda;
        UserInfo UI;

        public DatabaseConnection()
        {
            //myConnection = new SqlConnection(CONNECTION_STRING);
            //myCommand = new SqlCommand(SQL_DETAILS_QUERY);
            //myCommand.Connection = myConnection;

            //sda = new SqlDataAdapter(myCommand);
            //dataTable = new DataTable();
            //sda.Fill(dataTable);

            //myConnection.Close();

        }

        public bool CheckLogin(string Username, string Password)
        {
            return true;
            string hashPW = HashPassword(Password);
            myConnection = new SqlConnection(CONNECTION_STRING);
            myConnection.Open();
            dataTable = new DataTable();
            sda = new SqlDataAdapter(SQL_LOGIN_QUERY, myConnection);
            sda.SelectCommand.Parameters.AddWithValue("@username", Username);
            sda.SelectCommand.Parameters.AddWithValue("@passwordhash", hashPW);

            sda.Fill(dataTable);
            myConnection.Close();
            try
            {
                if (dataTable.Rows[0][0].ToString() != "")
                {
                    //UI.setupUI(dataTable.Rows[0][0].ToString(), dataTable.Rows) ////download data from database
                    UI = new UserInfo();
                    UI.setUsername(dataTable.Rows[0][0].ToString());
                    UI.setFirstName(dataTable.Rows[0][1].ToString());
                    UI.setClassCode(dataTable.Rows[0][2].ToString());
                    UI.setPasswordHash(dataTable.Rows[0][3].ToString());
                    UI.setHighScore(int.Parse(dataTable.Rows[0][4].ToString()));
                    UI.SetLevel(int.Parse(dataTable.Rows[0][5].ToString()));
                    UI.SetRecentScore(int.Parse(dataTable.Rows[0][6].ToString()));
                    UI.SetGamesPlayed(int.Parse(dataTable.Rows[0][7].ToString()));

                    string TEST = dataTable.Rows[0][5].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool CreateAccount(string Username, string Firstname, string Password, string Classcode, ref UserInfo UI)
        {
            myConnection = new SqlConnection(CONNECTION_STRING);
            myCommand = new SqlCommand(SQL_INSERT_NEWACCOUNT, myConnection);

            myCommand.Parameters.AddWithValue("@username", Username);
            myCommand.Parameters.AddWithValue("@firstname", Firstname);
            myCommand.Parameters.AddWithValue("@classcode", Classcode);
            myCommand.Parameters.AddWithValue("@passwordhash", HashPassword(Password));
            string debug = HashPassword(Password);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch
            {
                myConnection.Close();
                return false;
            }
        }

        private string HashPassword(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = null;

            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public void UpdateDetails(UserInfo UI)
        {
            myConnection = new SqlConnection(CONNECTION_STRING);
            myCommand = new SqlCommand(SQL_UPDATE_LOGIN, myConnection);


            myCommand.Parameters.AddWithValue("@userName", UI.GetUserName());
            myCommand.Parameters.AddWithValue("@highScore", UI.getHighScore());
            myCommand.Parameters.AddWithValue("@currentLevel", UI.GetLevel());
            myCommand.Parameters.AddWithValue("@recentScore", UI.GetRecentScore());
            myCommand.Parameters.AddWithValue("@gamesPlayed", UI.GetGamesPlayed());
            
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

        }

        public void UpdateLogin(UserInfo UI) //useless??
        {
            myConnection = new SqlConnection(CONNECTION_STRING);
            myCommand = new SqlCommand(SQL_UPDATE_LOGIN, myConnection);
            myCommand.Parameters.AddWithValue("@firstName", UI.getFirstName());
            myCommand.Parameters.AddWithValue("@classCode", UI.getClassCode());
            myCommand.Parameters.AddWithValue("@passwordHash", UI.getPasswordHash());
            myCommand.Parameters.AddWithValue("@userName", UI.GetUserName());
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public UserInfo getUI()
        {
            return UI;
        }


    }
}