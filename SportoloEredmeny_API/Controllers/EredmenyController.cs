using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SportoloEredmeny_API.Models;
using SportoloEredmeny_API.Models.DTOs;

namespace SportoloEredmeny_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        private readonly string ConnectionString = "server=localhost;database=sportolo13b;uid=root;password=";

        [HttpGet("eredmeny")]
        public List<Eredmenyek> GetAllPost()
        {
            List<Eredmenyek> eredmenyek = new();

            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = "SELECT * FROM eredmeny;";

            var cmd = new MySqlCommand(sql, connector);

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var e = new Eredmenyek
                {
                    Id = dataReader.GetInt32(0),
                    Competition = dataReader.GetString(1),
                    Description = dataReader.GetString(2),
                    ResultTime = dataReader.GetDateTime(3),
                    UpdateTime = dataReader.GetDateTime(4),
                    SportoloId = dataReader.GetInt32(5),
                };

                eredmenyek.Add(e);
            }

            connector.Close();
            return eredmenyek;
        }

        [HttpGet("eredmenybyid")]
        public object GetEredmenyekById(int id)
        {
            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            var sql = @"SELECT `Competition`, `ResultTime`, `UpdateTime` FROM `eredmeny` 
                        WHERE `id` = @id;";

            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();
            datareader.Read();
            var sportolo = new
            {
                Competiton = datareader.GetString(0),
                ResultTime = datareader.GetDateTime(1),
                UpdateTime = datareader.GetDateTime(2)
            };

            connector.Close();

            return sportolo;
        }

        [HttpPost]
        public Eredmenyek AddUjEredmeny(AddEredmenyDTO eredmeny)
        {
            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            var e = new Eredmenyek
            {
                Competition = eredmeny.Competition,
                Description = eredmeny.Description,
                ResultTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                SportoloId = eredmeny.SportoloId
            };

            var sql = $"INSERT INTO `eredmeny`(`Competition`, `Description`, `ResultTime`, `UpdateTime`, `SportoloId`) VALUES (@competition,@description,@resulttime,@updatetime,@sportoloid)";

            var cmd = new MySqlCommand(sql, connector);

            cmd.Parameters.AddWithValue("@competition", e.Competition);
            cmd.Parameters.AddWithValue("@description", e.Description);
            cmd.Parameters.AddWithValue("@resulttime", e.ResultTime);
            cmd.Parameters.AddWithValue("@updatetime", e.UpdateTime);
            cmd.Parameters.AddWithValue("@sportoloid", e.SportoloId);

            cmd.ExecuteNonQuery();

            connector.Close();

            return e;
        }

        [HttpDelete]
        public object deleteEredmeny(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();

            var sql = @"DELETE FROM `eredmeny` WHERE `id` = @id;";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            connector.Close();
            return new { message = "Eredmény törölve!" };



        }
    }
}
