using Dapper;
using SPDVIClickyCratesProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPDVIClickyCratesProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Online")]
    public class OnlineController : ApiController
    {
        [HttpPost]
        [Route("InsertPlayerOnline")]
        public IHttpActionResult InsertarOnline(OnlineModel online)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Online (Id, State, NickName, Image, Level, LevelBadge)" +
                                $" VALUES ('{online.Id}','Menu','{online.NickName}','{online.Image}', 'Noob','{online.LevelBadge}')";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error insert new player online " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("UpdateOnline")]
        public IHttpActionResult UpdateOnline(OnlineModel online)
        {
            OnlineModel onlinePlayer;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT * FROM dbo.Online WHERE Id = '{online.Id}'";

                try
                {
                    onlinePlayer = con.Query<OnlineModel>(sql).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return BadRequest("Error Get User Online" + ex.Message);
                }

                if (!string.IsNullOrEmpty(online.State))
                {
                    onlinePlayer.State = online.State;
                }

                if (!string.IsNullOrEmpty(online.Level))
                {
                    onlinePlayer.Level = online.Level;
                }

                if (!string.IsNullOrEmpty(online.NickName))
                {
                    onlinePlayer.NickName = online.NickName;
                }
                if (!string.IsNullOrEmpty(online.Image))
                {
                    onlinePlayer.Image = online.Image;
                }

                if (!string.IsNullOrEmpty(online.LevelBadge))
                {
                    onlinePlayer.LevelBadge = online.LevelBadge;
                }

                string query = "UPDATE dbo.Online " +
                    $"SET Nickname = '{onlinePlayer.NickName}', State = '{onlinePlayer.State}', Level = '{onlinePlayer.Level}', " +
                    $"Image = '{onlinePlayer.Image}', LevelBadge = '{onlinePlayer.LevelBadge}'" +
                    $" WHERE Id = '{onlinePlayer.Id}'";

                try
                {
                    con.Execute(query);

                }
                catch (Exception e)
                {
                    return BadRequest("Error Update player online " + e.Message);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetPlayersOnline")]
        public List<OnlineModel> GetPlayersOnline()
        {
            List<OnlineModel> onlinePlayers;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "SELECT * FROM dbo.Online";

                try
                {
                    onlinePlayers = con.Query<OnlineModel>(sql).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Error Get List Players Online " + e.Message);
                }
            }
            return onlinePlayers;
        }

        [HttpPost]
        [Route("DeletePlayerOnline")]
        public IHttpActionResult DeletePlayerOnline(OnlineModel online)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "DELETE FROM dbo.Online" +
                $" WHERE Id = '{online.Id}'";

                try
                {

                    con.Execute(sql);

                }
                catch (Exception e)
                {
                    return BadRequest("Error delete player online " + e.Message);
                }
            }
            return Ok();
        }

    }
}
