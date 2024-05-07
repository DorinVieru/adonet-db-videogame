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

    
}
