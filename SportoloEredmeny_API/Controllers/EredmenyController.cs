using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SportoloEredmeny_API.Models;

namespace SportoloEredmeny_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        private readonly string ConnectionString = "server=localhost;database=blog13b;uid=root;password=";

        [HttpGet("eredmeny")]
        public List<EredmenyPost> GetAllPost()
        {
            List<EredmenyPost> eredmenyek = new();

            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = "SELECT * FROM sportolo13b;";

            var cmd = new MySqlCommand(sql, connector);

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var eredmeny = new EredmenyPost
                {
                    Id = dataReader.GetInt32(0),
                    Competition = dataReader.GetString(1),
                    Description = dataReader.GetString(2),
                    ResultTime = dataReader.GetDateTime(3),
                    UpdateTime = dataReader.GetDateTime(4),
                    SportoloId = dataReader.GetInt32(5),
                };

                eredmenyek.Add(eredmeny);
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
                ResultTime = datareader.GetString(1),
                UpdateTime = datareader.GetString(2)
            };

            connector.Close();

            return sportolo;
        }

    }
}
