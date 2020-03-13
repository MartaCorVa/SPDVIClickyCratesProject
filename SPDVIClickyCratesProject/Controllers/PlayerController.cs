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
    [RoutePrefix("api/Player")]
    public class PlayerController : ApiController
    {
        [HttpPost]
        [Route("InsertNewPlayer")]
        public IHttpActionResult InsertNewPlayer(PlayerModel player)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Player (Id, FirstName, LastName, NickName, Email, DateOfBirth, City, BlobUri) " +
                    $"VALUES ('{player.Id}','{player.FirstName}','{player.LastName}','{player.NickName}','{player.Email}'," +
                    $"'{player.DateOfBirth}','{player.City}','{player.BlobUri}')";

                try
                {
                    con.Execute(sql);

                }
                catch (Exception e)
                {
                    return BadRequest("Error inserting player in database, " + e.Message);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetPlayer/{id}")]
        public PlayerModel GetPlayer(string Id)
        {
            PlayerModel player;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT * FROM dbo.Player WHERE id = '{Id}'";

                try
                {
                    player = con.Query<PlayerModel>(sql).FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e.Message);
                }
            }
            return player;
        }

        [HttpPost]
        [Route("UpdatePlayer")]
        public IHttpActionResult UpdatePlayer(PlayerModel player)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "UPDATE dbo.Player " +
                                $"SET FirstName = '{player.FirstName}', LastName = '{player.LastName}', NickName = '{player.NickName}', " +
                                $"City = '{player.City}' " +
                                $"WHERE Id = '{player.Id}'";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error Update player in database, " + e.Message);
                }
            }
            return Ok();
        }
    }
}
