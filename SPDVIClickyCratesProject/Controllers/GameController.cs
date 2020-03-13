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
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        [HttpPost]
        [Route("InsertNewGame")]
        public IHttpActionResult InsertNewGame(GameModel game)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Game (IdUser, Difficulty)" +
                $" VALUES ('{game.IdUser}',{game.Difficulty})";
                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error insert new game " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("UpdateGame")]
        public IHttpActionResult UpdateGame(GameModel game)
        {
            GameModel newGame;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string query = $"SELECT Id FROM dbo.Game WHERE IdUser = '{game.IdUser}' ORDER BY Id ASC";

                try
                {
                    newGame = con.Query<GameModel>(query).LastOrDefault();
                }
                catch (Exception ex)
                {
                    return BadRequest("Game not found " + ex.Message);
                }

                string sql = "UPDATE dbo.Game " +
                    $"SET DateStop = Convert(Datetime2,'{DateTime.Now}',103), Score = {game.Score}" +
                    $" WHERE Id = {newGame.Id}";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error updating game " + e.Message);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetGames/{Id}")]
        public List<GameModel> GetGames(string Id)
        {
            List<GameModel> games;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT * FROM dbo.Game WHERE IdUser = '{Id}' ORDER BY Score DESC";

                try
                {
                    games = con.Query<GameModel>(sql).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Error get all games " + e.Message);
                }
            }
            return games;
        }

    }
}
