using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPJR
{
    class UserInfo
    {
        string UserName;
        string FirstName;
        string ClassCode;
        string PasswordHash;
        int RecentScore;
        int CurrentLevel;
        int HighScore;
        int GamesPlayed;

        public UserInfo()
        {
        }

        public void setupUI(string userName, string firstName, string classCode, string passwordHash, int recentScore, int currentLevel, int highScore, int gamesPlayed)
        {
            UserName = userName;
            FirstName = firstName;
            ClassCode = classCode;
            PasswordHash = passwordHash;
            RecentScore = recentScore;
            CurrentLevel = currentLevel;
            HighScore = highScore;
            GamesPlayed = gamesPlayed;
        }

        public string GetUserName()
        {
            return UserName;
        }
        public void setUsername(string userName)
        {
            UserName = userName;
        }


        public void setFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public string getFirstName()
        {
            return FirstName;
        }


        public void setClassCode(string classCode)
        {
            ClassCode = classCode;
        }
        public string getClassCode()
        {
            return ClassCode;
        }


        public void setPasswordHash(string passHash)
        {
            PasswordHash = passHash;
        }
        public string getPasswordHash()
        {
            return PasswordHash;
        }


        public int GetLevel()
        {
            return CurrentLevel;
        }
        public void SetLevel(int NewCurrentLevel)
        {
            CurrentLevel = NewCurrentLevel;
        }


        public int GetGamesPlayed()
        {
            return GamesPlayed;
        }
        public void SetGamesPlayed(int NewGamesPlayed)
        {
            CurrentLevel = NewGamesPlayed;
        }


        public int GetRecentScore()
        {
            return RecentScore;
        }
        public void SetRecentScore(int NewRecentScore)
        {
            RecentScore = NewRecentScore;
            if (RecentScore > HighScore)
            {
                setHighScore(RecentScore);
            }
        }


        public int getHighScore()
        {
            return HighScore;
        }
        public void setHighScore(int newHighScore)
        {
            HighScore = newHighScore;
        }
        
    }
}
