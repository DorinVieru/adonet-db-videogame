using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace adonet_db_videogame
{
    public class Videogame
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SoftwareHouseID { get; set; }

        // Costruttore
        public Videogame(string name, string overview, string releaseDate, DateTime createdAt, DateTime updatedAt, int softwareHouseId)
        {
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SoftwareHouseID = softwareHouseId;
        }
    }

    public class VideogameMenager
    {
        public const string STRINGA_DI_CONNESSIONE = "Data Source=localhost; Initial Catalog=master; Integrated Security=True;";
        public const string NOME_DATABASE = "videogames";

        // FUNCTION PER INSERIRE UN NUOVO VIDEOGAME
        public static void InsertVideogame(string name, string overview, string releaseDate, DateTime createdAt, DateTime updatedAt, int softwareHouseId)
        {
            Videogame NewVideogame = new Videogame(name, overview, releaseDate, createdAt, updatedAt, softwareHouseId);

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();

                string query = @$"INSERT INTO {NOME_DATABASE} (name, overview, release_date, created_at, updated_at, software_house_id) 
                             VALUES (@name, @overview, @releaseDate, @creation, @update, @softwareHouseID)";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@overview", overview));
                cmd.Parameters.Add(new SqlParameter("@releaseDate", releaseDate));
                cmd.Parameters.Add(new SqlParameter("@creation", createdAt));
                cmd.Parameters.Add(new SqlParameter("@update", updatedAt));
                cmd.Parameters.Add(new SqlParameter("@softwareHouseID", softwareHouseId));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                connessioneSql.Close();
            }
        }

        // FUNCTION PER TROVARE UN VIDEOGAME TRAMITE L'ID
        public Videogame GetVideogameById(int id)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            string query = @$"SELECT * FROM {NOME_DATABASE} 
                              WHERE id = @Id";
            SqlCommand command = new SqlCommand(query, connessioneSql);
            command.Parameters.AddWithValue("@Id", id);

            connessioneSql.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Videogame
                (
                    name: reader["name"].ToString(),
                    overview: reader["overview"].ToString(),
                    releaseDate: reader["release_date"].ToString(),
                    createdAt: (DateTime)reader["created_at"],
                    updatedAt: (DateTime)reader["updated_at"],
                    softwareHouseId: Convert.ToInt32(reader["software_house_id"])
                );
            }
            else
            {
                return null;
            }
        }

        // FUNCTION PER TROVARE UN VIDEOGAME IN BASE AL SUO NOME O PARTE DI ESSO
        public List<Videogame> GetVideogamesByName(string searchGame)
        {
            List<Videogame> videogames = new List<Videogame>();

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);
                
            string query = @$"SELECT * FROM {NOME_DATABASE} 
                                  WHERE name LIKE @SearchGame";
            SqlCommand command = new SqlCommand(query, connessioneSql);
            command.Parameters.AddWithValue("@SearchGame", "%" + searchGame + "%");

            connessioneSql.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                videogames.Add(new Videogame
                (
                    name: reader["name"].ToString(),
                    overview: reader["overview"].ToString(),
                    releaseDate: reader["release_date"].ToString(),
                    createdAt: (DateTime)reader["created_at"],
                    updatedAt: (DateTime)reader["updated_at"],
                    softwareHouseId: Convert.ToInt32(reader["software_house_id"])
                ));
            }

            return videogames;
        }

        // FUNCTION PER CANCELLARE UN VIDEOGIOCO
        public void DeleteVideogame(int id)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            string query = @$"DELETE FROM {NOME_DATABASE} 
                                WHERE id = @Id";
            SqlCommand command = new SqlCommand(query, connessioneSql);
            command.Parameters.AddWithValue("@Id", id);

            connessioneSql.Open();
            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 0)
            {
                throw new Exception("Il videogioco specificato non esiste.");
            }
        }
    }
}
