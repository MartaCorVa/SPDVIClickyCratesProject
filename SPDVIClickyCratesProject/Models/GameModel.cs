using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPDVIClickyCratesProject.Models
{
    public class GameModel
    {
        public int Id;
        public string IdUser;
        public DateTime DateStart;
        public DateTime DateStop;
        public int Difficulty;
        public int Score;
        public int Duration;
    }
}