using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Videogame(string name, string overview, string release_date, DateTime createdAt, DateTime updatedAt, int software_hause_id)
        {
            Name = name;
            Overview = overview;
            ReleaseDate = release_date;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SoftwareHouseID = software_hause_id;
        }
    }

    public static class VideogameMenager
    {
        public const string STRINGA_DI_CONNESSIONE = "Data Source=localhost; Initial Catalog=master; Integrated Security=True;";

        static void InsertVideogame(string name, string overview, string release_date, DateTime createdAt, DateTime updatedAt, int software_hause_id)
        {
            Videogame NewVideogame = new Videogame(name, overview, release_date, createdAt, updatedAt, software_hause_id);

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();

                string query = @"INSERT INTO videogames (name, overview, release_date, created_at, updete_at, software_house_id) 
                             VALUES (@name, @overview, @releaseDate, @creation, @update, @softwareHouseID)";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@overview", overview));
                cmd.Parameters.Add(new SqlParameter("@releaseDate", release_date));
                cmd.Parameters.Add(new SqlParameter("@creation", createdAt));
                cmd.Parameters.Add(new SqlParameter("@update", updatedAt));
                cmd.Parameters.Add(new SqlParameter("@softwareHouseID", software_hause_id));

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

    }
}
